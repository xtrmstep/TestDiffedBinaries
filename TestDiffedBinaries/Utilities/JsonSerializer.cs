using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TestDiffedBinaries.Api.Utilities
{
    /// <summary>
    /// Wrapper for JSON.NET
    /// </summary>
    public static class JsonSerializer
    {
        public static string ToJson<T>(this T data)
        {
            if (data == null) return null;

            var jsonSer = new Newtonsoft.Json.JsonSerializer();
            
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                jsonSer.Serialize(writer, data);
            }
            return sb.ToString();
        }

        public static T FromJson<T>(this string json)
        {
            if (json == null) return default(T);

            var jsonSer = new Newtonsoft.Json.JsonSerializer();

            using (StringReader sr = new StringReader(json))
            using (JsonReader jsonReader = new JsonTextReader(sr))
            {
                return jsonSer.Deserialize<T>(jsonReader);
            }
        }
    }
}
