using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.TokenManager;
using EasyCommerce.Server.Shared.Options;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text;

namespace EasyCommerce.Server;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var migrationAssembly = typeof(ApplicationDbContext).Assembly.GetName().Name;
        var connectionString = Configuration.GetConnectionString(nameof(ApplicationDbContext));
        var projectAssemblies = Assemblies.projectAssemblies;

        services.AddTransient<ApplicationDbContext>();
        services.AddMediatR(projectAssemblies.ToArray());
        services.AddAutoMapper(Assemblies.projectAssemblies);

        services.Configure<UserOptions>(Configuration.GetSection(UserOptions.Position));

        UserOptions userOptions = (UserOptions)Activator.CreateInstance(typeof(UserOptions));
        Configuration.GetSection(UserOptions.Position).Bind(userOptions);

        services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(userOptions.ApiKey, userOptions.AddMonths));

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(userOptions.ApiKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.IgnoreObsoleteActions();
            c.IgnoreObsoleteProperties();
            c.CustomSchemaIds(type => type.FullName);
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyCommerce.Server", Version = "v1" });
        });
        services
            .AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>())
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationAssembly))
        );
        services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );
        services.AddLogging();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();
        app.UseCors(options =>
        {
            options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyCommerce.Server v1"));
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
