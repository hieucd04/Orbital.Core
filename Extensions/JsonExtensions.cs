using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Orbital.Core
{
    public static class Json
    {
        public static JsonSerializerSettings DefaultJsonSerializerSettings
        {
            get
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    DateParseHandling = DateParseHandling.None,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Populate,
                    ContractResolver = new DefaultContractResolver()
                };

                jsonSerializerSettings.Converters.Add(
                    new StringEnumConverter(
                        new CamelCaseNamingStrategy()
                    )
                );

                return jsonSerializerSettings;
            }
        }

        static JsonSerializerSettings CamelCaseJsonSerializerSettings
        {
            get
            {
                var jsonSerializerSettings = DefaultJsonSerializerSettings;
                jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                return jsonSerializerSettings;
            }
        }

        public static void ConfigureSerializer() { JsonConvert.DefaultSettings = () => CamelCaseJsonSerializerSettings; }
    }
}