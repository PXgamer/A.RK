using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
		string url = "";
		string title = "";
		string year = "";
			for(int i = 0; i < args.Length; i++)
			   {
				   if( args[i].ToLower() == "-movie" ){
						title = args[1];
						year = args[2];
						url = "http://omdbapi.com/?t=" + title + "&y=" + year + "&type=movie";
						
						WebClient c = new WebClient();
						var data = c.DownloadString(url);
							
						JObject o = JObject.Parse(data);
							try {
								Console.WriteLine("Title:         ") + o["Title"].ToString();
								Console.WriteLine("Year:          ") + o["Year"].ToString();
								Console.WriteLine("---------------");
								Console.WriteLine("Runtime:       ") + o["Runtime"].ToString();
								Console.WriteLine("Genre:         ") + o["Genre"].ToString();
								Console.WriteLine("Age Rating:    ") + o["Rated"].ToString();
								Console.WriteLine("---------------");
								Console.WriteLine("Director:      ") + o["Director"].ToString();
								Console.WriteLine("Plot:          ") + o["Plot"].ToString();
								Console.WriteLine("---------------");
								Console.WriteLine("IMDB ID:       ") + o["imdbID"].ToString();
								Console.WriteLine("Metascore:     ") + o["Metascore"].ToString();
								Console.WriteLine("IMDB Rating:   ") + o["imdbRating"].ToString();
								
								//End Film
								Console.WriteLine("---------------");
								Console.WriteLine("");
								Console.WriteLine("");
							}
							catch {
								Console.WriteLine("Sorry. This film could not be found!";
							}
				   }
				   else if( args[i].ToLower() == "-tv" ){
						title = args[1];
						url = "http://api.tvmaze.com/singlesearch/shows?q=" + title;
						
						WebClient c = new WebClient();
						var data = c.DownloadString(url);
							
						JObject o = JObject.Parse(data);
						
						try {
							Console.WriteLine("Title:         ") + o["name"].ToString();
							Console.WriteLine("Premiere Date: ") + o["premiered"].ToString();
							Console.WriteLine("---------------");
							Console.WriteLine("Genre:         ") + o["genres"].ToString();
							Console.WriteLine("Status:        ") + o["status"].ToString();
							Console.WriteLine("Plot:          ") + o["summary"].ToString();
							Console.WriteLine("---------------");
							if (o.SelectToken(rating.average) != null) {
							Console.WriteLine("Rating:        ") + o.SelectToken(rating.average).ToString();
							}
							else {}
							if (o.SelectToken(rating.average) != null) {
							Console.WriteLine("TVRage ID:     ") + o.SelectToken(externals.tvrage).ToString();
							}
							else {}
							if (o.SelectToken(rating.average) != null) {
							Console.WriteLine("TVDB ID:       ") + o.SelectToken(externals.thetvdb).ToString();
							}
							else {}
							
							//End TV Show
							Console.WriteLine("---------------");
							Console.WriteLine("");
							Console.WriteLine("");
						}
						catch {
							Console.WriteLine("Sorry. This TV show could not be found!";
						}
				   }
			   }
		
		

        }
    }
}
