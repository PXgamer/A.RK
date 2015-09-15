using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Amaze.RK_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Set Strings
            string title = "";
            string year = "";
            string imdb = "";
            string aURL = "";
            string q720p = "";
            string q1080p = "";
            string XMLURL = "";
            string XMLURL2 = "";

            //Define Strings
            if (textBox1.Text != "") {
                title = textBox1.Text;
                aURL = "http://omdbapi.com/?t=" + title + "&type=movie&tomatoes=true";
                XMLURL = "https://yts.to/rss/" + title + "/1080p/0";
                XMLURL2 = "https://yts.to/rss/" + title + "/720p/0";

                if (textBox2.Text != "")
                {
                    year = textBox2.Text;
                    aURL = "http://omdbapi.com/?t=" + title + "&y=" + year + "&type=movie&tomatoes=true";

                    if (textBox3.Text != "")
                    {
                        imdb = textBox3.Text;
                    }
                }
            }
            else if (textBox3.Text != "")
            {
                imdb = textBox3.Text;
                aURL = "http://omdbapi.com/?i=" + imdb + "&type=movie&tomatoes=true";

                if (textBox2.Text != "")
                {
                    year = textBox2.Text;
                    aURL = "http://omdbapi.com/?i=" + imdb + "&y=" + year + "&type=movie&tomatoes=true";
                }
            }
            else
            {
                MessageBox.Show("Please enter something...");
            }
            
            WebClient c = new WebClient();
            var data = c.DownloadString(aURL);
            //Console.WriteLine(data);
            JObject o = JObject.Parse(data);

            Genre.Text = o["Genre"].ToString();
            Rating.Text = o["Rated"].ToString();
            string aTitle = o["Title"].ToString();
            Title.Text = Regex.Replace(o["Title"].ToString(), @"[Â]", "") + " (" + o["Year"].ToString() + ")";
            IMDB.Text = o["imdbID"].ToString();
            Plot.Text = o["Plot"].ToString();
            Director.Text = o["Director"].ToString();
            Language.Text = o["Language"].ToString();
            Country.Text = o["Country"].ToString();
            Metascore.Text = o["Metascore"].ToString() + "/100";
            IMDBRating.Text = o["imdbRating"].ToString() + "/10";
            TomatoMeter.Text = o["tomatoMeter"].ToString() + "/100";
            TomatoRating.Text = o["tomatoRating"].ToString() + "/10";
            DVDRelease.Text = o["DVD"].ToString();


            /*
             
             
            //YTS XML to JSON
            WebClient d = new WebClient();
            var xml = d.DownloadString(XMLURL);
            var xml2 = d.DownloadString(XMLURL2);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(xml2);
            string jsonText2 = JsonConvert.SerializeXmlNode(doc2);

            //Use JSON Data 1080p
            JObject l = JObject.Parse(jsonText);
            MessageBox.Show(l.ToString());
            if (l.SelectToken("rss.channel.item.title").ToString() == aTitle + " (" + year + ") 1080p")
            {
                q1080p = l.SelectToken("rss.channel.item.enclosure.@url").ToString();
                MessageBox.Show(q1080p);
            }
            else
            {
                MessageBox.Show("Cannot find this movie in 1080p");
            }
             
             
            */


            //Start poster loading
            p.Load(o["Poster"].ToString());

            //create a new Bitmap with the proper dimensions

            Bitmap finalImg = new Bitmap(p.Image, p.Width, p.Height);

            //center the new image
            p.SizeMode = PictureBoxSizeMode.CenterImage;

            //set the new image
            p.Image = finalImg;

            p.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Width == 1049)
            {
                this.Width = 662;
                button2.Text = ">";
            }
            else
            {
                this.Width = 1049;
                button2.Text = "<";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://Amaze.RK/");
        }

    }
}
