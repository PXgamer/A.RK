using System;
using System.Net;
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
								Console.WriteLine("Title:         " + Regex.Replace(o["Title"].ToString(), @"[Â]", ""));
                                Console.WriteLine("Year:          " + o["Year"].ToString());
								Console.WriteLine("---------------");
								Console.WriteLine("Runtime:       " + o["Runtime"].ToString());
								Console.WriteLine("Genre:         " + o["Genre"].ToString());
								Console.WriteLine("Age Rating:    " + o["Rated"].ToString());
								Console.WriteLine("---------------");
								Console.WriteLine("Director:      " + o["Director"].ToString());
								Console.WriteLine("Plot:          " + o["Plot"].ToString());
								Console.WriteLine("---------------");
								Console.WriteLine("IMDB ID:       " + o["imdbID"].ToString());
								Console.WriteLine("Metascore:     " + o["Metascore"].ToString());
								Console.WriteLine("IMDB Rating:   " + o["imdbRating"].ToString());
								
								//End Film
								Console.WriteLine("---------------");
								Console.WriteLine("");
								Console.WriteLine("");
							}
							catch {
								Console.WriteLine("Sorry. This film could not be found!");
							}
				   }
				   else if( args[i].ToLower() == "-tv" ){
						title = args[1];
						url = "http://api.tvmaze.com/singlesearch/shows?q=" + title;
						
						WebClient c = new WebClient();
						var data = c.DownloadString(url);
							
						JObject l = JObject.Parse(data);
						
						try {
							Console.WriteLine("Title:         " + l["name"].ToString());
							Console.WriteLine("Premiere Date: " + l["premiered"].ToString());
							Console.WriteLine("---------------");
							Console.WriteLine("Genre:         " + l["genres"][0].ToString());
							Console.WriteLine("Status:        " + l["status"].ToString());
                            string plot = Regex.Replace(l["summary"].ToString(), "<.*?>", string.Empty);
                            Console.WriteLine("Plot:          " + plot);
							Console.WriteLine("---------------");
							if (l.SelectToken("rating.average") != null) {
							Console.WriteLine("Rating:        " + l.SelectToken("rating.average").ToString());
							}
							else {}
							if (l.SelectToken("rating.average") != null) {
							Console.WriteLine("TVRage ID:     " + l.SelectToken("externals.tvrage").ToString());
							}
							else {}
							if (l.SelectToken("rating.average") != null) {
							Console.WriteLine("TVDB ID:       " + l.SelectToken("externals.thetvdb").ToString());
							}
							else {}
							
							//End TV Show
							Console.WriteLine("---------------");
							Console.WriteLine("");
							Console.WriteLine("");
						}
						catch {
							Console.WriteLine("Sorry. This TV show could not be found!");
						}
				   }
			   }
		
		

        }
    }
}
