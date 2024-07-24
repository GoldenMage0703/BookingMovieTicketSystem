using DTO.Models;
using Newtonsoft.Json;

namespace Web.SessionExtensions
{
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                // other settings if needed
            };
           
            session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }
    }
}
