using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Utilities.Helpers.Serialization
{
    public class IgnoreNegativeValuesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsClass;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Bu dönüştürücü sadece serileştirme için kullanılmalıdır.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken token = JToken.FromObject(value);

            if (token.Type == JTokenType.Object)
            {
                JObject obj = (JObject)token;
                obj = ProcessObject(obj);
                obj.WriteTo(writer);
            }
            else
            {
                token.WriteTo(writer);
            }
        }

        private JObject ProcessObject(JObject obj)
        {
            JObject result = new JObject();
            foreach (var property in obj.Properties())
            {
                JToken value = property.Value;

                if (value.Type == JTokenType.Integer && (int)value < 0)
                {
                    continue;
                }

                if (value.Type == JTokenType.Object)
                {
                    value = ProcessObject((JObject)value);
                }

                if (value.Type == JTokenType.Array)
                {
                    value = ProcessArray((JArray)value);
                }

                result.Add(property.Name, value);
            }
            return result;
        }

        private JArray ProcessArray(JArray array)
        {
            JArray result = new JArray();

            for (int i = 0; i < array.Count; i++)
            {
                JToken token = array[i];

                if (token.Type == JTokenType.Integer && (int)token < 0)
                {
                    continue;
                }

                if (token.Type == JTokenType.Object)
                {
                    token = ProcessObject((JObject)token);
                }

                if (token.Type == JTokenType.Array)
                {
                    token = ProcessArray((JArray)token);
                }

                result.Add(token);
            }

            return result;
        }
    }
}
