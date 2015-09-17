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
        //Set Strings
        string title = "";
        string year = "";
        string imdb = "";
        string aURL = "";
        string tvID = "";
        // string q720p = "";
        // string q1080p = "";
        string XMLURL = "";
        string XMLURL2 = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (radioButton1.Checked == true)
            {
                movie();
            }
            else if (radioButton2.Checked == true)
            {
                tv();
            }
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label1.Text = "Movie Name:";
                label2.Text = "Movie Year:";
                label3.Text = "IMDB ID:";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button2.Enabled = true;
                button1.Text = "Search for Movie";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label1.Text = "TV Show Name:";
                label2.Text = "TV Show Year:";
                label3.Text = "TVDB ID:";
                label5.Text = "TVDB ID:";
                label6.Text = "TVRage ID:";
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                textBox3.Enabled = true;
                button2.Enabled = false;
                this.Width = 662;
                button2.Text = ">";
                button1.Text = "Search for TV Show";
            }
        }


        public void movie()
        {
            //Define Strings
            if (textBox1.Text != "")
            {
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

        public void tv()
        {
            if (textBox1.Text != "")
            {
                title = textBox1.Text;
                aURL = "http://api.tvmaze.com/singlesearch/shows?q=" + title;

                WebClient c = new WebClient();
                var data = c.DownloadString(aURL);

                JObject l = JObject.Parse(data);

                try
                {
                    Title.Text = l["name"].ToString();
                    Genre.Text = l["genres"][0].ToString();

                    Console.WriteLine("Status:        " + l["status"].ToString());

                    Plot.Text = Regex.Replace(l["summary"].ToString(), "<.*?>", string.Empty);

                    if (l.SelectToken("externals.tvrage") != null)
                    {
                        IMDB.Text = l.SelectToken("externals.tvrage").ToString();
                    }
                    else { }
                    if (l.SelectToken("rating.average") != null)
                    {
                        Rating.Text = l.SelectToken("externals.thetvdb").ToString();
                    }
                    else { }
                    string poster = l.SelectToken("image.original").ToString();
                    //Start poster loading
                    p.Load(poster);

                    //create a new Bitmap with the proper dimensions

                    Bitmap finalImg = new Bitmap(p.Image, p.Width, p.Height);

                    //center the new image
                    p.SizeMode = PictureBoxSizeMode.CenterImage;

                    //set the new image
                    p.Image = finalImg;

                    p.Show();
                }
                catch
                {
                    Console.WriteLine("Sorry. This TV show could not be found!");
                }
            }
            else if (textBox3.Text != "")
            {
                tvID = textBox3.Text;
                aURL = "http://api.tvmaze.com/lookup/shows?thetvdb=" + tvID;
            }
            
        }

    }
}
