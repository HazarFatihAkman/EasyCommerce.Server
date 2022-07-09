using AutoMapper;
using EasyCommerce.Server.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommerce.Server.Shared;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assemblies.projectAssemblies);
    }

    private void ApplyMappingsFromAssembly(IEnumerable<Assembly> assemblies)
    {
        var types = assemblies
            .SelectMany(i => i.GetTypes())
            .Where(x => x.GetInterfaces().Any(i => i.IsGenericType &&
                    (i.GetGenericTypeDefinition() == typeof(IMap<>)))).ToList();

        foreach (var type in types)
        {
            try
            {
                var instance = Activator.CreateInstance(type);

                var methods = type.GetInterfaces().Where(i => i.Name == "IMap`1").Select(i => i.GetMethod("Map"))
                    .ToList();

                methods.ForEach(x => x.Invoke(instance, new object[] { this }));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"[Failed] Mapper : Apply '{type.FullName}'", ex);
            }
        }
    }
}
public interface IMap<T>
{
    void Map(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
        profile.CreateMap(GetType(), typeof(T));
    }
}
