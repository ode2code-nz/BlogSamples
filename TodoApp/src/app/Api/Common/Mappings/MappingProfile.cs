using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Todo.Api.Common.Mappings
{
    public class MappingProfile : Profile
    {
        private readonly Assembly _assembly;
        private const string MapFrom = "MapFrom";
        private const string MapToAndFrom = "MapToAndFrom";

        public MappingProfile()
        {
            _assembly = Assembly.GetExecutingAssembly();

            ApplyMapFromMappings(typeof(IMapFrom<>), MapFrom);
            ApplyMapFromMappings(typeof(IMapToAndFrom<>), MapToAndFrom);
        }

        private void ApplyMapFromMappings(Type interfaceType, string mappingMethodName)
        {
            var types = _assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName) 
                    ?? type.GetInterface(interfaceType.Name)?.GetMethod(mappingMethodName);
                
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}