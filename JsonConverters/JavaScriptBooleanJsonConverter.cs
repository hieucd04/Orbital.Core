using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Orbital.Core
{
    public class JavaScriptBooleanJsonConverter : JsonConverter<bool>
    {
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return JToken.Load(reader).Value<bool?>() ?? false;
        }

        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer) { new JValue(value).WriteTo(writer); }
    }
}