using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApiSample.Api.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public static List<T> ToCollection<T>(this T item) where T : new()
        {
            return new List<T>{item};
        }
    }
}