using Newtonsoft.Json;
using ProgFrog.Interface.Data.Serialization;

namespace ProgFrog.Core.Data.Serialization
{
    public class JsonSerializer<T> : IModelSerializer<T>
    {
        public T Deserialize(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public string Serialize(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
