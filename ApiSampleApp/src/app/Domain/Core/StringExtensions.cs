using System.IO;
using System.Xml.Serialization;

namespace ApiSample.Domain.Core
{
    public static class StringExtensions
    {
        public static T FromXmlStringToType<T>(this string xml)
        {
            T instance;
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var sr = new StringReader(xml))
            {
                instance = (T)xmlSerializer.Deserialize(sr);
            }
            return instance;
        }
    }
}
