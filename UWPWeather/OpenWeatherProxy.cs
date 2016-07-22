using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace UWPWeather
{
    class OpenWeatherProxy
    {
        public async static Task<RootObject> GetData(string pair)
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://btc-e.com/api/2/"+pair+"/ticker");
            var result = await response.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var data = (RootObject)serializer.ReadObject(memoryStream);

            return data;
        }
    }

    [DataContract]
    public class Ticker
    {
        [DataMember]
        public double high { get; set; }
        [DataMember]
        public double low { get; set; }
        [DataMember]
        public double avg { get; set; }
        [DataMember]
        public double vol { get; set; }
        [DataMember]
        public double vol_cur { get; set; }
        [DataMember]
        public double last { get; set; }
        [DataMember]
        public double buy { get; set; }
        [DataMember]
        public double sell { get; set; }
        [DataMember]
        public long updated { get; set; }
        [DataMember]
        public long server_time { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Ticker ticker { get; set; }
    }
}
