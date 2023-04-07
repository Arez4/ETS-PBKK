using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Net;

namespace WindowsFormsApplication43
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string city;
        private void button1_Click(object sender, EventArgs e)
        {
            

            city = txtcity.Text;

            string uri = string.Format("http://api.weatherapi.com/v1/forecast.xml?key=ccecc38af17842a897155559230404&q={0}&days=1&aqi=no&alerts=no", city);

            XDocument doc = XDocument.Load(uri);

            string iconUri = (string)doc.Descendants("icon").FirstOrDefault();

            WebClient client = new WebClient();

            byte[] image = client.DownloadData("http:" + iconUri);

            MemoryStream stream = new MemoryStream(image);



            Bitmap newBitMap = new Bitmap(stream);
            string avgtemp = (string)doc.Descendants("avgtemp_c").FirstOrDefault();
            string precipation = (string)doc.Descendants("totalprecip_mm").FirstOrDefault();
            string maxwindk= (string)doc.Descendants("maxwind_kph").FirstOrDefault();
            string Condition = (string)doc.Descendants("text").FirstOrDefault();
            string humidity = (string)doc.Descendants("humidity").FirstOrDefault();


            string country = (string)doc.Descendants("country").FirstOrDefault();

            string cloud = (string)doc.Descendants("text").FirstOrDefault();

            Bitmap icon = newBitMap;


            txtmaxtemp.Text = avgtemp;
            txtmintemp.Text = precipation;

            txtwindm.Text = maxwindk;
            txtwindk.Text = Condition;

            txthumidity.Text = humidity;

            label7.Text = cloud;
            txtcountry.Text = country;
            pictureBox1.Image = icon;








        }


        private void getweather(string city)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Max Temp", typeof(string));
            dt.Columns.Add("Min Temp", typeof(string));
            dt.Columns.Add("Maxwindmph", typeof(string));
            dt.Columns.Add("Maxwindkph", typeof(string));
            dt.Columns.Add("Humidity", typeof(string));
            dt.Columns.Add("Cloud", typeof(string));
            dt.Columns.Add("Icon", typeof(Bitmap));

            city = txtcity.Text;

            string uri = string.Format("http://api.weatherapi.com/v1/forecast.xml?key=ccecc38af17842a897155559230404&q={0}&days=1&aqi=no&alerts=no", city);

            XDocument doc = XDocument.Load(uri);


            foreach(var npc in doc.Descendants("forecastday"))
            {
                string iconUri = (string)npc.Descendants("icon").FirstOrDefault();
                WebClient client = new WebClient();
                byte[] image = client.DownloadData("http:" + iconUri);
                MemoryStream stream = new MemoryStream(image);


                Bitmap newBitmap = new Bitmap(stream);

                
                dt.Rows.Add(new object[]
                {
                     (string)doc.Descendants("country").FirstOrDefault(),
                      (string)npc.Descendants("date").FirstOrDefault(),

                   (string)npc.Descendants("maxtemp_c").FirstOrDefault(),
                   (string)npc.Descendants("mintemp_c").FirstOrDefault(),
                    (string)npc.Descendants("maxwind_mph").FirstOrDefault(),
                   (string)npc.Descendants("maxwind_kph").FirstOrDefault(),
                   (string)npc.Descendants("avghumidity").FirstOrDefault(),              
                     (string)npc.Descendants("text").FirstOrDefault(),
                     newBitmap


                });
            }
            dataGridView1.DataSource = dt;






        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
