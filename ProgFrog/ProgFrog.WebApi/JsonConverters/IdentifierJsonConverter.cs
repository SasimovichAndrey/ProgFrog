using Newtonsoft.Json;
using ProgFrog.Interface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgFrog.WebApi.JsonConverters
{
    public class IdentifierJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IIdentifier) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var str = serializer.Deserialize<string>(reader);
            var psn = new GuidIdentifier(Guid.Parse(str));

            return psn;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var str = ((IIdentifier)value).StringPresentation;
            serializer.Serialize(writer, str);
        }
    }
}