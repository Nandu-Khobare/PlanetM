using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using AbstractLayer;
using System.Drawing;
using PlanetM_Utility;

namespace PlanetM_Ops
{
    /*******************************************************************************
    * Free ASP.net IMDb Scraper API for the new IMDb Template.
    * Author: Abhinay Rathore
    * Website: http://www.AbhinayRathore.com
    * Blog: http://web3o.blogspot.com
    * More Info: http://web3o.blogspot.com/2010/11/aspnetc-imdb-scraping-api.html
    * Last Updated: Jan 29, 2013
    *******************************************************************************/

    public class IMDBOperations
    {
        public bool status { get; set; }
        public string errorMsg { get; set; }
        public MovieIMDB iMov = new MovieIMDB();
        /// <summary>
        /// Movie details will be retrieved from Internet based on search keywords.
        /// First movie link will be taken from Google/Bing results and then retrieved from IMDB Site.
        /// </summary>
        /// <param name="MovieName"></param>
        public IMDBOperations(string MovieName)
        {
            LogWrapper.LogInfo(string.Format("Searching Movie with name : {0}", MovieName));
            string imdbUrl = getIMDbUrl(MovieName);
            LogWrapper.LogInfo(string.Format("URL for Movie : {0} is found as : {1}", MovieName, imdbUrl));
            status = false;
            if (!string.IsNullOrEmpty(imdbUrl))
            {
                string html = getUrlData(imdbUrl + "combined");
                parseIMDbPage(html, false);
                string error;
                if (!string.IsNullOrWhiteSpace(iMov.ImdbID))
                    SaveHtmlInBackup(html, iMov, out error);
            }
        }
        public IMDBOperations(string MovieName, bool GetExtraInfo = false)
        {
            LogWrapper.LogInfo(string.Format("Searching Movie with name : {0}", MovieName));
            string imdbUrl = getIMDbUrl(MovieName);
            LogWrapper.LogInfo(string.Format("URL for Movie : {0} is found as : {1}", MovieName, imdbUrl));
            status = false;
            if (!string.IsNullOrEmpty(imdbUrl))
            {
                string html = getUrlData(imdbUrl + "combined");
                parseIMDbPage(html, GetExtraInfo);
                string error;
                if (!string.IsNullOrWhiteSpace(iMov.ImdbID))
                    SaveHtmlInBackup(html, iMov, out error);
            }
        }
        /// <summary>
        /// Movie details will be retrieved from Internet based on URL.
        /// This will be used in Sync Operation in MiniIMDB
        /// </summary>
        /// <param name="movie"></param>
        public IMDBOperations(MovieIMDB movie)
        {
            LogWrapper.LogInfo(string.Format("Searching Movie with URL : {0}", movie.ImdbURL));
            iMov = movie;
            status = false;
            if (!string.IsNullOrEmpty(movie.ImdbURL))
            {
                string html = getUrlData(movie.ImdbURL + "combined");
                parseIMDbPage(html, false);
                string error;
                if (!string.IsNullOrWhiteSpace(iMov.ImdbID))
                    SaveHtmlInBackup(html, iMov, out error);
            }
        }
        /// <summary>
        /// Get IMDb URL from search results
        /// </summary>
        /// <param name="MovieName"></param>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        private string getIMDbUrl(string MovieName, string searchEngine = "google")
        {
            LogWrapper.LogInfo(string.Format("Search using {0} for Movie : {1} ", searchEngine, MovieName));
            string url = "http://www.google.com/search?q=imdb+" + System.Uri.EscapeUriString(MovieName); //default to Google search
            if (searchEngine.ToLower().Equals("bing")) url = "http://www.bing.com/search?q=imdb+" + System.Uri.EscapeUriString(MovieName);
            if (searchEngine.ToLower().Equals("ask")) url = "http://www.ask.com/web?q=imdb+" + System.Uri.EscapeUriString(MovieName);
            LogWrapper.LogInfo(string.Format("URL for search is : {0}", url));
            string html = getUrlData(url);
            ArrayList imdbUrls = matchAll(@"<a href=""(http://www.imdb.com/title/tt\d{7}/)"".*?>.*?</a>", html);
            LogWrapper.LogInfo(string.Format("Matches found on {0} : {1}", searchEngine, imdbUrls.Count));
            if (imdbUrls.Count > 0)
                return (string)imdbUrls[0]; //return first IMDb result
            else if (searchEngine.ToLower().Equals("google")) //if Google search fails
                return getIMDbUrl(MovieName, "bing"); //search using Bing
            else if (searchEngine.ToLower().Equals("bing")) //if Bing search fails
                return getIMDbUrl(MovieName, "ask"); //search using Ask
            else
                return string.Empty;
        }

        /// <summary>
        /// Parse IMDb page data
        /// </summary>
        /// <param name="html"></param>
        /// <param name="GetExtraInfo"></param>
        private void parseIMDbPage(string html, bool GetExtraInfo)
        {
            try
            {
                LogWrapper.LogInfo(string.Format("Parsing movie data from HTML started."));
                iMov.ImdbID = match(@"<link rel=""canonical"" href=""http://www.imdb.com/title/(tt\d{7})/"" />", html);
                LogWrapper.LogInfo(string.Format("ImdbID : {0}", iMov.ImdbID));
                if (!string.IsNullOrEmpty(iMov.ImdbID))
                {
                    status = true;
                    //iMov.Title = System.Net.WebUtility.HtmlDecode(match(@"<title>(.*?) \(.*?</title>", html)).Replace("IMDb - ", "");
                    iMov.Title = System.Net.WebUtility.HtmlDecode(match(@"<title>(IMDb \- )*(.*?) \(.*?</title>", html, 2));
                    LogWrapper.LogInfo(string.Format("Title : {0}", iMov.Title));
                    iMov.OriginalTitle = match(@"title-extra"">(.*?)<", html);
                    LogWrapper.LogInfo(string.Format("OriginalTitle : {0}", iMov.OriginalTitle));
                    iMov.Year = match(@"<title>.*?\(.*?(\d{4}).*?\).*?</title>", html);
                    LogWrapper.LogInfo(string.Format("Year : {0}", iMov.Year));
                    //iMov.Rating = match(@">(\d.\d)</b><span class=""mellow"">/10", html);
                    //iMov.Rating = match(@">(\d.\d)</span></b><span class=""mellow"">", html);
                    //iMov.Rating = match(@">(\d.\d)</span></strong><span class=""mellow"">", html);
                    //iMov.Rating = match(@"ratingValue"">(\d.\d)<", html);
                    iMov.Rating = match(@"<b>(\d.\d)/10</b>", html);
                    LogWrapper.LogInfo(string.Format("Rating : {0}", iMov.Rating));

                    iMov.Genres = new ArrayList();
                    LogWrapper.LogInfo(string.Format("Finding Genres in html"));
                    //iMov.Genres = matchAll(@"<a.*?>(.*?)</a>", match(@"Genres:</h4>(.*?)</div>", html));
                    iMov.Genres = matchAll(@"<a.*?>(.*?)</a>", match(@"Genre.?:(.*?)(</div>|See more)", html));
                    iMov.Directors = new ArrayList();
                    LogWrapper.LogInfo(string.Format("Finding Directors in html"));
                    //iMov.Directors = matchAll(@"<a.*?>(.*?)</a>", match(@"Directors?:[\n\r\s]*</h4>(.*?)(</div>|>.?and )", html));
                    iMov.Directors = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Directed by</a></h5>(.*?)</table>", html));
                    LogWrapper.LogInfo(string.Format("Finding Writers in html"));
                    //iMov.Writers = matchAll(@"<a.*?>(.*?)</a>", match(@"Writers?:[\n\r\s]*</h4>(.*?)(</div>|>.?and )", html));
                    iMov.Writers = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Writing credits</a></h5>(.*?)</table>", html));
                    LogWrapper.LogInfo(string.Format("Finding Stars in html"));
                    //iMov.Stars = matchAll(@"<a.*?>(.*?)</a>", match(@"Stars?:(.*?)</div>", html));
                    //iMov.Stars = matchAll(@"<a.*?actors.*?>(.*?)</a>", match(@"Stars?:(.*?)</div>", html));
                    iMov.Stars = matchAll(@"<a.*?>(.*?)</a>", match(@"Stars?:(.*?)(</div>|<a href=""fullcredits)", html));
                    LogWrapper.LogInfo(string.Format("Finding Cast in html"));
                    //iMov.Cast = matchAll(@"class=""name"">[\n\r\s]*<a.*?>(.*?)</a>", html);
                    //iMov.Cast = matchAll(@"itemprop='name'>(.*?)</a>", html);
                    iMov.Cast = matchAll(@"<td class=""nm""><a.*?href=""/name/.*?/"".*?>(.*?)</a>", match(@"<h3>Cast</h3>(.*?)</table>", html));
                    LogWrapper.LogInfo(string.Format("Finding Producers in html"));
                    iMov.Producers = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Produced by</a></h5>(.*?)</table>", html));
                    LogWrapper.LogInfo(string.Format("Finding Musicians in html"));
                    iMov.Musicians = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Original Music by</a></h5>(.*?)</table>", html));
                    LogWrapper.LogInfo(string.Format("Finding Cinematographers in html"));
                    iMov.Cinematographers = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Cinematography by</a></h5>(.*?)</table>", html));
                    LogWrapper.LogInfo(string.Format("Finding Editors in html"));
                    iMov.Editors = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Film Editing by</a></h5>(.*?)</table>", html));

                    iMov.Plot = System.Net.WebUtility.HtmlDecode(match(@"<p itemprop=""description"">(.*?)<a href=""plotsummary"">See full summary</a>", html));
                    if (iMov.Plot == "")
                        iMov.Plot = System.Net.WebUtility.HtmlDecode(match(@"<p itemprop=""description"">(.*?)</p>", html));
                    LogWrapper.LogInfo(string.Format("Plot : {0}", iMov.Plot));
                    LogWrapper.LogInfo(string.Format("Finding PlotKeywords in html"));
                    iMov.PlotKeywords = matchAll(@"<a.*?>(.*?)</a>", match(@"Plot Keywords:</h5>.*?<div class=""info-content"">(.*?)</div", html));

                    iMov.ReleaseDate = match(@"Release Date:</h4>.*?(\d{1,2} (January|February|March|April|May|June|July|August|September|October|November|December) (19|20)\d{2}).*(\(|<span)", html);
                    LogWrapper.LogInfo(string.Format("ReleaseDate : {0}", iMov.ReleaseDate));
                    iMov.Runtime = match(@"Runtime:</h4>[\s]*.*?(\d{1,4}) min[\s]*.*?\<\/div\>", html);
                    //if (String.IsNullOrEmpty(iMov.Runtime)) iMov.Runtime = match(@"infobar.*?([0-9]+) min.*?</div>", html);
                    if (String.IsNullOrEmpty(iMov.Runtime)) iMov.Runtime = match(@"<time itemprop=""duration"".*?>.*?(\d+) min.*?</time>", html);
                    LogWrapper.LogInfo(string.Format("Runtime : {0}", iMov.Runtime));
                    iMov.Top250 = match(@"Top 250 #(\d{1,3})<", html);
                    LogWrapper.LogInfo(string.Format("Top250 : {0}", iMov.Top250));
                    iMov.Oscars = match(@"<b>Won (\d{1,2}) Oscars?\.</b>", html);
                    //iMov.Oscars = match(@"Won (\d{1,2}) Oscars\.", html);
                    //if (iMov.Oscars == "")
                    //{
                    //    iMov.Oscars = match(@"(Won Oscar\.)", html);
                    //    if (iMov.Oscars == "Won Oscar.")
                    //        iMov.Oscars = "1";
                    //}
                    LogWrapper.LogInfo(string.Format("Oscars : {0}", iMov.Oscars));
                    iMov.Awards = match(@"(\d{1,4}) wins", html);
                    LogWrapper.LogInfo(string.Format("Awards : {0}", iMov.Awards));
                    iMov.Nominations = match(@"(\d{1,4}) nominations", html);
                    LogWrapper.LogInfo(string.Format("Nominations : {0}", iMov.Nominations));
                    //iMov.Storyline = System.Net.WebUtility.HtmlDecode(match(@"Storyline</h2>[\s]*<p>(.*?)[\s]*(<em|</p>)", html));
                    iMov.Storyline = System.Net.WebUtility.HtmlDecode(match(@"Storyline</h2>.*?<p>(.*?)[\s]*(<em|</p>)", html));
                    LogWrapper.LogInfo(string.Format("Storyline : {0}", iMov.Storyline));
                    iMov.Tagline = System.Net.WebUtility.HtmlDecode(match(@"Taglines?:</h4>(.*?)(<span|</div)", html));
                    LogWrapper.LogInfo(string.Format("Tagline : {0}", iMov.Tagline));
                    //iMov.MpaaRating = match(@"infobar"">.*?<img.*?alt=""(.*?)"" src="".*?certificates.*?"".*?>", html);
                    iMov.MpaaRating = match(@"<div class=""infobar"">[\n\r\s]*?<span title=""Ratings certificate for .*?"".*?class=""us_(.*?)", html);
                    LogWrapper.LogInfo(string.Format("MpaaRating : {0}", iMov.MpaaRating));
                    //iMov.Votes = match(@"href=""ratings"".*?>(\d+,?\d*)</span> votes</a>", html);
                    iMov.Votes = match(@"ratingCount"">(\d+,?\d*)</span>", html);
                    LogWrapper.LogInfo(string.Format("Votes : {0}", iMov.Votes));
                    //iMov.PosterURL = match(@"img_primary"">[\n\r\s]*?<a.*?><img src=""(.*?)"".*?</td>", html);
                    iMov.PosterURL = match(@"<div class=""image"">.*?<img.*?src=""(.*?)"".*?</div>", html);
                    //if (!string.IsNullOrEmpty(iMov.PosterURL) && iMov.PosterURL.IndexOf("nopicture") < 0)
                    if (!string.IsNullOrEmpty(iMov.PosterURL) && iMov.PosterURL.IndexOf("media-imdb.com") > 0)
                    {
                        if (iMov.PosterURL.IndexOf("_V1_") != -1)
                        {
                            //iMov.PosterSmallURL = iMov.PosterURL.Substring(0, iMov.PosterURL.IndexOf("_V1.")) + "_V1._SY150.jpg";
                            //iMov.PosterLargeURL = iMov.PosterURL.Substring(0, iMov.PosterURL.IndexOf("_V1.")) + "_V1._SY500.jpg";
                            //iMov.PosterLargeFullURL = iMov.PosterURL.Substring(0, iMov.PosterURL.IndexOf("_V1.")) + "_V1._SY0.jpg";
                            //iMov.PosterSmallURL = Regex.Replace(iMov.PosterURL, @"_V1_.*?.jpg", "_V1._SY150.jpg");
                            //iMov.PosterLargeURL = Regex.Replace(iMov.PosterURL, @"_V1_.*?.jpg", "_V1._SY500.jpg");
                            //iMov.PosterLargeFullURL = Regex.Replace(iMov.PosterURL, @"_V1_.*?.jpg", "_V1._SY0.jpg");
                            //iMov.Poster = Image.FromStream(new WebClient().OpenRead(iMov.PosterLargeFullURL));
                            iMov.Poster = Image.FromStream(new WebClient().OpenRead(iMov.PosterURL));
                        }
                        else
                            iMov.Poster = Image.FromStream(new WebClient().OpenRead(iMov.PosterURL));
                    }
                    else
                    {
                        iMov.PosterURL = string.Empty;
                        iMov.PosterSmallURL = string.Empty;
                        iMov.PosterLargeURL = string.Empty;
                        iMov.Poster = null;
                    }
                    LogWrapper.LogInfo(string.Format("PosterURL : {0}", iMov.PosterURL));
                    iMov.ImdbURL = "http://www.imdb.com/title/" + iMov.ImdbID + "/";
                    if (GetExtraInfo)
                    {
                        iMov.ReleaseDates = getReleaseDates();
                        iMov.MediaImages = getMediaImages();
                    }
                    LogWrapper.LogInfo(string.Format("Parsing movie data from HTML finished."));
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogWrapper.LogError(ex);
                MessageHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Get all release dates
        /// </summary>
        /// <returns></returns>
        private ArrayList getReleaseDates()
        {
            LogWrapper.LogInfo(string.Format("Retrieving release dates"));
            ArrayList list = new ArrayList();
            string releasehtml = getUrlData("http://www.imdb.com/title/" + iMov.ImdbID + "/releaseinfo");
            string releaseDate = string.Empty;
            foreach (string r in matchAll(@"<tr>(.*?)</tr>", match(@"Date</th></tr>\n*?(.*?)</table>", releasehtml)))
            {
                Match rd = new Regex(@"<td>(.*?)</td>\n*?.*?<td align=""right"">(.*?)</td>", RegexOptions.Multiline).Match(r);
                releaseDate = StripHTML(rd.Groups[1].Value.Trim()) + " = " + StripHTML(rd.Groups[2].Value.Trim());
                LogWrapper.LogInfo(string.Format("Release date : {0}", releaseDate));
                list.Add(releaseDate);
            }
            return list;
        }

        /// <summary>
        /// Get all media images
        /// </summary>
        /// <returns></returns>
        private ArrayList getMediaImages()
        {
            LogWrapper.LogInfo(string.Format("Retrieving media images"));
            ArrayList list = new ArrayList();
            string mediaurl = "http://www.imdb.com/title/" + iMov.ImdbID + "/mediaindex";
            string mediahtml = getUrlData(mediaurl);
            int pagecount = matchAll(@"<a href=""\?page=(.*?)"">", match(@"<span style=""padding: 0 1em;"">(.*?)</span>", mediahtml)).Count;
            for (int p = 1; p <= pagecount + 1; p++)
            {
                mediahtml = getUrlData(mediaurl + "?page=" + p);
                foreach (Match m in new Regex(@"src=""(.*?)""", RegexOptions.Multiline).Matches(match(@"<div class=""thumb_list"" style=""font-size: 0px;"">(.*?)</div>", mediahtml)))
                {
                    String image = m.Groups[1].Value;
                    image = image.Substring(0, image.IndexOf("_V1.")) + "_V1._SY500.jpg";
                    LogWrapper.LogInfo(string.Format("Media image : {0}", image));
                    list.Add(image);
                }
            }
            return list;
        }

        /// <summary>
        /// Match single instance
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="html"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private string match(string regex, string html, int i = 1)
        {
            return new Regex(regex, RegexOptions.Multiline).Match(html).Groups[i].Value.Trim();
        }

        /// <summary>
        /// Match all instances and return as ArrayList
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="html"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private ArrayList matchAll(string regex, string html, int i = 1)
        {
            ArrayList list = new ArrayList();
            int cnt = 0;
            foreach (Match m in new Regex(regex, RegexOptions.Multiline).Matches(html))
            {
                cnt++;
                LogWrapper.LogInfo(string.Format("Match #{0} found is : {1}", cnt, m.Groups[i].Value.Trim()));
                list.Add(System.Net.WebUtility.HtmlDecode(m.Groups[i].Value).Trim());
            }
            return list;
        }

        /// <summary>
        /// Strip HTML Tags
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        static string StripHTML(string inputString)
        {
            return Regex.Replace(inputString, @"<.*?>", string.Empty);
        }

        /// <summary>
        /// Get URL Data
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string getUrlData(string url)
        {
            LogWrapper.LogInfo(string.Format("Retrieving data for URL : {0}", url));
            WebClient client = new WebClient();
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            //Random IP Address
            client.Headers["X-Forwarded-For"] = r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255);
            //Random User-Agent
            client.Headers["User-Agent"] = "Mozilla/" + r.Next(3, 5) + ".0 (Windows NT " + r.Next(4, 6) + "." + r.Next(0, 2) + "; rv:2.0.1) Gecko/20100101 Firefox/" + r.Next(3, 5) + "." + r.Next(0, 5) + "." + r.Next(0, 5);
            LogWrapper.LogInfo(string.Format("Generated IP Address : {0} and User-Agent : {1}", client.Headers["X-Forwarded-For"], client.Headers["User-Agent"]));
            try
            {
                Stream datastream = client.OpenRead(url);
                StreamReader reader = new StreamReader(datastream);

                while (!reader.EndOfStream)
                    sb.Append(reader.ReadLine());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogWrapper.LogError(ex);
                MessageHandler.ShowError(ex);
            }
            return sb.ToString();
        }

        private bool SaveHtmlInBackup(string html, MovieIMDB movie, out string error)
        {
            bool result = false;
            error = string.Empty;
            try
            {
                LogWrapper.LogInfo(string.Format("Creating IMDB HTML backup for Movie : {0}", movie.ImdbID));
                string backupDirectory = Configuration.ReadConfig("IMDB HTML Backup");
                if (!Directory.Exists(backupDirectory))
                    Directory.CreateDirectory(backupDirectory);
                string backupFileName = string.Format("{0} - {1} ({2}) - {3}.html", movie.ImdbID, movie.Title, movie.Year, DateTime.Now.ToString("yyyyMMdd"));
                Regex regex = new Regex(string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))), RegexOptions.Compiled);
                backupFileName = regex.Replace(backupFileName, "");
                LogWrapper.LogInfo(string.Format("IMDB HTML backup file name is : {0}", backupFileName));
                backupFileName = Path.Combine(backupDirectory, backupFileName);
                if (File.Exists(backupFileName))
                    File.Delete(backupFileName);
                File.WriteAllText(backupFileName, html);
                LogWrapper.LogInfo(string.Format("Created IMDB HTML backup file successfully : {0}", backupFileName));
                result = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                LogWrapper.LogError(ex);
            }
            return result;
        }
    }
}
