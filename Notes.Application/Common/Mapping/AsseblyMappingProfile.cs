
using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Notes.Application.Common.Mapping
{
    // генерация маппера на основе классов с сборке, реализовавших интерфейс IMapWith
    public class AsseblyMappingProfile : Profile
    {
        public AsseblyMappingProfile(Assembly assembly) =>
            ApplyMappingFromAssembly(assembly);

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(Type => Type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>))).ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo.Invoke(instance, new object[] { this });
            }
        }
    }
}
