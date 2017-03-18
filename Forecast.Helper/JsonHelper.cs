using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace Forecast.Helper
{
    public static class JsonHelper<T> where T : class
    {
        public static T JsonDeserialize(string message)
        {
            //using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            //{
                //ms.Position = 0;
                var ss = new JsonSerializerSettings();
                ss.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                ss.DateFormatString = "yyyy-MM-dd hh:mm:ss";
                ss.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                return JsonConvert.DeserializeObject<T>(message, ss);
                //var s = new DataContractJsonSerializer(typeof(T));
                //return (T)s.ReadObject(ms);
            //}
        }

        public static string JsonSerialize(T message)
        {
            //JsonConvert.SerializeObject()
            using (var ms = new MemoryStream())
            {
                var s = new DataContractJsonSerializer(typeof(T));
                s.WriteObject(ms, message);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
