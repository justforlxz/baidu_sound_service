using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile=new OpenFileDialog();
            openFile.Filter = "mp3音频|*.mp3|wav音频|*.wav|3gpp音频|*.3gpp";
            Base64 be=new Base64();
            byte[] bytes;
            string resultConvert;
            FileInfo fileInfo = null;
            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                bytes = be.loadWavSound(openFile.FileName);
                resultConvert = be.mp3Convert2Binary(bytes);
                fileInfo = new FileInfo(openFile.FileName);
                AccessKey access = new AccessKey();
                Sound sound = new Sound();
                string json = access.JsonBuild();
                string jsonTemp = access.JsonPost();
                string accessKey = access.jsonReader(jsonTemp);
                string jsonTemp1 = sound.JsonBuild(accessKey, resultConvert, Convert.ToInt32(fileInfo.Length));
                string jsonTemp2 = sound.JsonPost(jsonTemp1);
                byte[] utf81 = utf8.GetBytes(jsonTemp2);
                byte[] gb2312b = Encoding.Convert(utf8, gb2312, utf81);
               // string jsonFinal = sound.jsonReader(jsonTemp2);
                textBox1.Text += gb2312.GetString(gb2312b);
            }


            //textBox1.Text=jsonTemp+"\r\n"+access.jsonReader(jsonTemp);
        }
    }
}
