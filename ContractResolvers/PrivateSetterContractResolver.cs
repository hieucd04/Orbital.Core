using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Orbital.Core
{
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            if (jsonProperty.Writable)
            {
                return jsonProperty;
            }

            var propertyInfo = member as PropertyInfo;
            var hasPrivateSetter = propertyInfo?.GetSetMethod(true) != null;
            jsonProperty.Writable = hasPrivateSetter;

            return jsonProperty;
        }
    }
}