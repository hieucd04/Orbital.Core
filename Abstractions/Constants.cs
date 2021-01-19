using JetBrains.Annotations;

namespace Orbital.Core
{
    public static class Constants
    {
        public static class Http
        {
            [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
            public static class ContentTypes
            {
                public const string ApplicationJson = "application/json";
            }

            [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
            public static class Headers
            {
                public const string ContentEncoding = "Content-Encoding";
                public const string ContentType = "Content-Type";
                public const string ETag = "ETag";
                public const string XRequestedWith = "X-Requested-With";
            }

            [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
            public static class Methods
            {
                public const string Delete = "DELETE";
                public const string Get = "GET";
                public const string Head = "HEAD";
                public const string Options = "OPTIONS";
                public const string Patch = "PATCH";
                public const string Post = "POST";
                public const string Put = "PUT";
            }

            [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
            public static class RequestTypes
            {
                public const string Ajax = "XMLHttpRequest";
            }
        }
    }
}