using EasyCommerce.Server.Shared.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace EasyCommerce;

public static class OptionsExtensions
{
    public static T AddOptions<T>(this IServiceCollection services, IConfiguration configuration) where T : class, IOption
    {
        services.AddOptions();
        T options = (T)Activator.CreateInstance(typeof(T));
        configuration.GetSection(options.Position).Bind(options);
        services.TryAddSingleton(options);
        return options;
    }
}
