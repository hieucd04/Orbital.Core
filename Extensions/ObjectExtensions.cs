using Newtonsoft.Json;

namespace Orbital.Core
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object @object, Formatting formatting = Formatting.None, JsonSerializerSettings settings = null)
        {
            return @object == null ? "{}" : JsonConvert.SerializeObject(@object, formatting, settings);
        }
    }
}