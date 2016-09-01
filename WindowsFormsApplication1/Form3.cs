using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        string[] profile = new string[7];
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            new Form2().Show();
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!Settings1.Default.auth)
            {
                Thread.Sleep(500);
            }

            WebRequest request1 = WebRequest.Create("https://api.vk.com/method/users.get?user_id=" + Settings1.Default.id + "&fields=photo_max_orig,country,bdate,sex,online&access_token=" + Settings1.Default.token);
            WebResponse resp = request1.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader read = new StreamReader(stream);
            string sr = read.ReadToEnd();
            JObject o = JObject.Parse(sr);
            JArray Response = (JArray)o["response"];
            profile[0] = (string)Response[0]["first_name"];
            profile[1] = (string)Response[0]["last_name"];
            if ((int)Response[0]["sex"] == 1) profile[2] = "Female"; else profile[2] = "Male";
            profile[3] = (string)Response[0]["photo_max_orig"];
            profile[4] = (string)Response[0]["bdate"];
            if ((int)Response[0]["online"] == 1) profile[5] = "online"; else profile[5] = "offline";
            int id = (int)Response[0]["country"];
            read.Close();
            resp.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = profile[3];
            label1.Text = profile[0];
            label2.Text = profile[1];
            label3.Text = profile[2];
            label4.Text = profile[4];
            label5.Text = profile[5];
        }

        private void loadVKPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
