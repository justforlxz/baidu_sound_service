using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
/*            OpenFileDialog openFile=new OpenFileDialog();
            openFile.Filter = "mp3音频|*.mp3|wma音频|*.wma|3gpp音频|*.3gpp";
            Base64 be=new Base64();
            byte[] bytes;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                bytes = be.loadWavSound(openFile.FileName);
                string result = be.mp3Convert2Binary(bytes);
                textBox1.Text = result;

            }*/
            AccessKey access=new AccessKey();
            string json = access.JsonBuild();
            string jsonTemp = access.JsonPost();
            textBox1.Text=jsonTemp+"\r\n"+access.jsonReader(jsonTemp);
        }
    }
}
