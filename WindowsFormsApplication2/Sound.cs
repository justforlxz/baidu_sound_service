using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WindowsFormsApplication2
{
    class Sound
    {
        public string JsonBuild(string token,string data,int length)
        {
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName("format");
            writer.WriteValue("wav");
            writer.WritePropertyName("rate");
            writer.WriteValue(8000);
            writer.WritePropertyName("channel");
            writer.WriteValue(1);
            writer.WritePropertyName("cuid");
            writer.WriteValue("8638110117331105");
            writer.WritePropertyName("token");
            writer.WriteValue(token);
            writer.WritePropertyName("speech");
            writer.WriteValue(data);
            writer.WritePropertyName("len");
            writer.WriteValue(length);
            writer.WriteEndObject();
            writer.Flush();
            string result = sw.GetStringBuilder().ToString();
            return result;
        }

        public string JsonPost(string Json)
        {
            //string fulldata = "grant_type=" + grant_type + "&client_id=" + client_id + "&client_secret=" + client_secret;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(Json);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://vop.baidu.com/server_api");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            myRequest.ContentLength = data.Length;
            myRequest.Proxy = null;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            return result;
        }
        public string jsonReader(string json)
        {
            soundResult ak = JsonConvert.DeserializeObject<soundResult>(json);
            return ak.ReultStrings();
        }

    }

    public class soundResult
    {
        public string err_no { get; set; }
        public string err_msg { get; set; }
        public string sn { get; set; }
        public string result { get; set; }
        public  string ReultStrings()
        {
            return result;
        }
    }
}
