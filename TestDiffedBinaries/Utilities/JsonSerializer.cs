using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestDiffedBinaries.Api.Models;

namespace TestDiffedBinaries.Api.Tests.Utilities
{
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
