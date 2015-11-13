using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using AbstractLayer;
using PlanetM_Utility;

namespace PlanetM_Ops
{
    public class FolderOperations
    {
        #region Initialization
        static FolderOperations _instance;
        static List<MoviePathInfo> moviePaths = new List<MoviePathInfo>();
        List<Movie> movies = new List<Movie>();
        List<string> stopwords = new List<string>();
        public FolderOperations()
        {
            getMoviePaths();
            getStopWords();
        }
        public static FolderOperations GetInstance()
        {
            if (_instance == null)
            { _instance = new FolderOperations(); return _instance; }
            else
            { return _instance; }
        }
        private bool getMoviePaths()
        {
            Hashtable configCollection = (Hashtable)(System.Configuration.ConfigurationManager.GetSection("MoviePaths"));
            IDictionaryEnumerator index = configCollection.GetEnumerator();
            MoviePathInfo moviePathInfo = new MoviePathInfo();
            // List<MoviePathInfo> moviePaths = new List<MoviePathInfo>();
            try
            {
                while (index.MoveNext())
                {
                    moviePaths.Add((MoviePathInfo)index.Value);
                    //string strMoviePath = moviePathInfo.MoviePath;
                    //string strMovieLanguage = moviePathInfo.MovieLanguage;
                    //int order = moviePathInfo.Order;
                }
            }
            catch (Exception ex)
            {
                //if (log.IsErrorEnabled)
                //{
                //    log.Error("Page Load failed : " + ex.Message);
                //}
            }
            moviePaths.Reverse();
            return true;
        }
        private bool getStopWords()
        {
            try
            {
                Hashtable configCollection = (Hashtable)(System.Configuration.ConfigurationManager.GetSection("StopWords"));
                IDictionaryEnumerator index = configCollection.GetEnumerator();
                while (index.MoveNext())
                {
                    stopwords.Add((string)index.Value);
                }
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            return true;
        }
        #endregion
        public List<Movie> scanForMovies()
        {
            movies.Clear();
            foreach (MoviePathInfo path in moviePaths)
            {
                getMoviesForPath(path);
            }
            /*
            FileStream fs = new FileStream(@"C:\Logs\PlanetM\Movies.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            foreach (Movie m in movies)
            {
                sw.WriteLine(m.Name+" "+m.Language);
            }
            sw.Close();
            fs.Close();
            */
            return movies;
        }
        public bool getMoviesForPath(MoviePathInfo moviePath)
        {
            bool success = false;
            try
            {
                if (Directory.Exists(moviePath.MoviePath))
                {
                    //string[] moviesInCurrentDirArray = Directory.GetDirectories(moviePath.MoviePath);
                    //DateTime[] modificationTimes = new DateTime[moviesInCurrentDirArray.Length];
                    //for (int i = 0; i < moviesInCurrentDirArray.Length; i++)
                    //    modificationTimes[i] = new DirectoryInfo(moviesInCurrentDirArray[i]).LastWriteTime;
                    //Array.Sort(modificationTimes, moviesInCurrentDirArray);
                    //List<string> moviesInCurrentDir = moviesInCurrentDirArray.Reverse().ToList();

                    List<string> moviesInCurrentDir = (from dir in
                                                           from e in Directory.GetDirectories(moviePath.MoviePath)
                                                           select new DirectoryInfo(e)
                                                       orderby dir.LastWriteTime descending
                                                       select dir.FullName).ToList();

                    moviesInCurrentDir.RemoveAll(ContainsSubDir);
                    foreach (string movieDir in moviesInCurrentDir)
                    {
                        try
                        {
                            Movie mov = new Movie();
                            mov.Name = removeStopwordsFromMovName(movieDir);
                            mov.Language = moviePath.MovieLanguage;
                            mov.Location = movieDir;
                            //mov.Checksum = GetMD5HashFromFile(movieDir);
                            mov.Size = (float)GetSizeForMovie(movieDir) / (1024 * 1024);
                            mov.Year = GetYearForMovie(mov.Name);
                            movies.Add(mov);
                            string[] CollectionWords = Configuration.GetConfigurationValues("CollectionGroupWords");
                            foreach (string collectionWord in CollectionWords)
                            {
                                if (mov.Name.Contains(collectionWord))
                                {
                                    Random rand = new Random();
                                    MoviePathInfo CollectionPath = new MoviePathInfo();
                                    CollectionPath.MoviePath = movieDir;
                                    CollectionPath.MovieLanguage = moviePath.MovieLanguage;
                                    CollectionPath.Order = rand.Next(1000, 2000);
                                    getMoviesForPath(CollectionPath);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogWrapper.LogError(ex);
                        }
                    }
                    success = true;
                }
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            return success;
        }
        public List<Movie> getMoviesForFolder(string folderPath, string language)
        {
            movies.Clear();
            MoviePathInfo mPathInfo = new MoviePathInfo();
            mPathInfo.MoviePath = folderPath;
            mPathInfo.MovieLanguage = language;
            getMoviesForPath(mPathInfo);
            List<string> CDRoms = new List<string>();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.CDRom)
                    CDRoms.Add(drive.Name);
            }
            if (CDRoms.Contains(Path.GetPathRoot(folderPath)))
            {
                IEnumerable<FileInfo> movieFilesOnCDRom = from fileinf in
                                                              from file in Directory.GetFiles(folderPath)
                                                              select new FileInfo(file)
                                                          orderby fileinf.LastWriteTime descending
                                                          select fileinf;

                foreach (FileInfo moviefile in movieFilesOnCDRom)
                {
                    Movie mov = new Movie();
                    mov.Name = removeStopwordsFromMovName(Regex.Replace(moviefile.Name, moviefile.Extension, ""));
                    mov.Language = language;
                    mov.Location = moviefile.FullName;
                    mov.Size = (float)moviefile.Length / (1024 * 1024);
                    mov.Year = GetYearForMovie(mov.Name);
                    if (mov.Size >= 100)
                        movies.Add(mov);
                }
            }
            return movies;
        }
        private long GetSizeForMovie(string movieDir)
        {
            long size = 0;
            DirectoryInfo dir = new DirectoryInfo(movieDir);
            try
            {
                FileInfo[] files = dir.GetFiles();
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (FileInfo file in files)
                {
                    size += file.Length;
                }
                foreach (DirectoryInfo d in dirs)
                {
                    size += GetSizeForMovie(d.FullName);
                }
            }
            catch (Exception ex)
            {
                MessageHandler.ShowError(ex);
            }
            return size;
        }
        private int GetYearForMovie(string movieName)
        {
            int Year = 0;
            Regex regex = new Regex(@"(19|20)\d{2}", RegexOptions.Compiled | RegexOptions.Singleline);
            Match match = regex.Match(movieName);
            if (match.Success)
            {
                Year = Convert.ToInt32(match.Value);
            }
            return Year;
        }
        // Search predicate returns true if a string matches with the subpaths.
        private static bool ContainsSubDir(String s)
        {
            bool ans = false;
            foreach (MoviePathInfo path in moviePaths)
            {
                if (path.MoviePath == s)
                    return true;
            }
            return ans;
        }

        private string removeStopwordsFromMovName(string movName)
        {
            movName = movName.Remove(0, movName.LastIndexOf('\\') + 1);
            Regex regEx = new Regex(@"(\.|\[|\]|-|\(|\)|{|}|\s{2,})");
            movName = regEx.Replace(movName, " ");
            foreach (string stopword in stopwords)
            {
                regEx = new Regex(stopword, RegexOptions.IgnoreCase);
                movName = regEx.Replace(movName, "").Trim();
            }
            regEx = new Regex(@"(\.|\[|\]|-|\(|\)|{|}|\s{2,})");
            movName = regEx.Replace(movName, " ");
            return movName;
        }

        public string GetMD5HashFromFile(string movDir)
        {
            DirectoryInfo di = new DirectoryInfo(movDir);
            IEnumerable<System.IO.FileInfo> fileList = di.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            string fileName = (from file in fileList
                               let len = file.Length
                               where len > 0
                               orderby len descending
                               select file.FullName).First();

            FileStream file1 = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file1);
            file1.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
