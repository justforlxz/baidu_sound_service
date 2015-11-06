using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace WindowsFormsApplication2
{
    class AccessKey
    {
        private string grant_type = "client_credentials";
        private string client_id = "NIrSkGHyhxrNGoqIXWiB5GpD";
        private string client_secret="a25731e4055e3399635f3a5b29ecb2e9";

        public string JsonBuild()
        {
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName("grant_type");
            writer.WriteValue(grant_type);
            writer.WritePropertyName("client_id");
            writer.WriteValue(client_id);
            writer.WritePropertyName("client_secret");
            writer.WriteValue(client_secret);
            writer.WriteEndObject();
            writer.Flush();
            string result = sw.GetStringBuilder().ToString();
            return result;
        }
        private bool CheckValidationResult(object sender,
        X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;// Always accept
        }

        public string JsonPost()//string Json)
        {
            string fulldata = "grant_type=" + grant_type + "&client_id=" + client_id + "&client_secret=" + client_secret;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(fulldata);
            HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create("https://openapi.baidu.com/oauth/2.0/token/");
            ServicePointManager.ServerCertificateValidationCallback =new RemoteCertificateValidationCallback(CheckValidationResult);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
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
            Accesskey ak = JsonConvert.DeserializeObject<Accesskey>(json);
            return ak.ToString();
        }
    }

    public class Accesskey
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string session_key { get; set; }
        public string session_secret { get; set; }
        public override string ToString()
        {
            return access_token;
        }
    }
}
