using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EasyCommerce.Server.Shared.Common;

public static class Assemblies
{
    private static List<Assembly> _assemblies;

    public static List<Assembly> projectAssemblies => _assemblies ??= GetOrLoadAssemblies();

    private static List<Assembly> GetOrLoadAssemblies()
    {
        var loadedAssemblies = new HashSet<string>();
        var assembliesToCheck = new Queue<Assembly>();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(i => i.FullName.StartsWith("EasyCommerce")).ToList();

        assemblies.ForEach(i =>
        {
            assembliesToCheck.Enqueue(i);
            Debug.WriteLine($"[Assemblies] Add : '{i.GetName().Name}'");
        });

        while (assembliesToCheck.Any())
        {
            var assemblyToCheck = assembliesToCheck.Dequeue();

            if (loadedAssemblies.Contains(assemblyToCheck.FullName))
            {
                Debug.WriteLine($"[Assemblies] Loaded : '{assemblyToCheck.GetName().Name}'");
            }

            loadedAssemblies.Add(assemblyToCheck.FullName);

            assemblyToCheck.GetReferencedAssemblies().Where(i => i.FullName.StartsWith("Ecommerce")).ToList().ForEach(i =>
            {
                if (!loadedAssemblies.Contains(i.FullName) && !assembliesToCheck.Any(i => i.FullName == i.FullName))
                {
                    Debug.WriteLine($"[Assemblies] Not loaded :'{i.Name}' added to queue again");
                    var assembly = Assembly.Load(i);
                    assembliesToCheck.Enqueue(assembly);
                    loadedAssemblies.Add(assembly.FullName);
                }
                else
                {
                    Debug.WriteLine($"[Assemblies] Loaded or in queue : '{i.Name}'");
                }
            });
        }

        return assemblies.OrderBy(i => i.FullName).Distinct().ToList();
    }
}
