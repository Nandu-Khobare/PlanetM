using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AbstractLayer;
using DBAccessLayer;
using PlanetM_Utility;

namespace PlanetM_Ops
{
    public class DBOperations : Movie
    {
        IDataAccess DBAObj;
        public DBOperations()
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings["PlanetM"] != null)
                DBAObj = new SQLDataAccess(System.Configuration.ConfigurationManager.ConnectionStrings["PlanetM"].ConnectionString);
            else
                DBAObj = new SQLDataAccess(PlanetM_Utility.Configuration.ReadConfig("ConnectionString"));
            LogWrapper.LogInfo("DB Access Object created successfully.");
        }
        public bool AddNewMovie(Movie movie)
        {
            LogWrapper.LogInfo("Adding new movie in PlanetM.");
            return DBAObj.AddNewMovie(movie);
        }
        public bool AddNewMovieFromIMDB(string ImdbID, ref string strErrorMsg)
        {
            LogWrapper.LogInfo("Adding new movie from MiniIMDB to PlanetM.");
            return DBAObj.AddNewMovieFromIMDB(ImdbID, ref strErrorMsg);
        }
        public bool DeleteMovie(Movie movie)
        {
            LogWrapper.LogInfo("Deleting movie from PlanetM.");
            return DBAObj.DeleteMovie(movie);
        }
        public bool UpdateMovie(string MovieName, string Language, Movie movie)
        {
            LogWrapper.LogInfo("Updating movie details in PlanetM.");
            return DBAObj.UpdateMovie(MovieName, Language, movie);
        }
        public DataSet GetAllMovies()
        {
            LogWrapper.LogInfo("Retrieving all movies for PlanetM.");
            return DBAObj.GetAllMovies();
        }
        public DataSet GetAllMoviesForPDFExport()
        {
            LogWrapper.LogInfo("Retrieving all movies in PlanetM for PDF Export.");
            return DBAObj.GetAllMoviesForPDFExport();
        }
        public DataSet GetAllMoviesBySearchCriteria(AbstractLayer.MovieSearchCriteria criteria)
        {
            LogWrapper.LogInfo("Retrieving movies from PlanetM for given search criteria.");
            return DBAObj.GetAllMoviesBySearchCriteria(criteria);
        }
        public DataSet GetMovieDetails(string Name, string Language)
        {
            LogWrapper.LogInfo("Retrieving movie details from PlanetM by name and language.");
            return DBAObj.GetMovieDetails(Name, Language);
        }
        public DataSet GetMovieDetails(string ImdbID)
        {
            LogWrapper.LogInfo("Retrieving movie details from PlanetM by ImdbId.");
            return DBAObj.GetMovieDetails(ImdbID);
        }
        public bool UpdateMovieCover(Movie mov, byte[] pic)
        {
            LogWrapper.LogInfo("Updating movie cover in PlanetM.");
            return DBAObj.UpdateCover(mov, pic);
        }
        public bool UpdateMovieChecksum(Movie movie)
        {
            LogWrapper.LogInfo("Updating movie checksum in PlanetM.");
            return DBAObj.UpdateMovieChecksum(movie);
        }
        public bool UpdateMovieAsSeenUnseen(string MovieName, string Language, bool IsWatched, ref string strErrorMsg)
        {
            LogWrapper.LogInfo("Updating movie as Seen\\Unseen in PlanetM.");
            return DBAObj.UpdateMovieAsSeenUnseen(MovieName, Language, IsWatched, ref strErrorMsg);
        }
        public byte[] GetMovieCover(Movie mov)
        {
            LogWrapper.LogInfo("Retrieving movie cover in PlanetM.");
            return DBAObj.GetCover(mov);
        }
        public DataSet GetAllMovieCovers()
        {
            LogWrapper.LogInfo("Retrieving all movie covers from PlanetM and MiniIMDB.");
            return DBAObj.GetAllMovieCovers();
        }
        public DataSet GetAllMovieDuplicates()
        {
            LogWrapper.LogInfo("Retrieving all movie duplicates from PlanetM.");
            return DBAObj.GetAllMovieDuplicates();
        }
        public DataSet GetRecentMovies()
        {
            LogWrapper.LogInfo("Retrieving recently added movies in PlanetM.");
            return DBAObj.GetRecentMovies();
        }
        public DataSet GetWatchlistMovies()
        {
            LogWrapper.LogInfo("Retrieving movies yet to see from PlanetM.");
            return DBAObj.GetWatchlistMovies();
        }
        public DataSet GetDiscrepancyReport(DiscrepancyReport report)
        {
            LogWrapper.LogInfo("Retrieving movies for Discrepancy Report.");
            return DBAObj.GetDiscrepancyReport(report);
        }
        public bool DeleteMovieLocation(string Location)
        {
            LogWrapper.LogInfo("Deleting movie location from PlanetM.");
            return DBAObj.DeleteMovieLocation(Location);
        }
        public bool MergeMovieWithExistingMovie(string MovieName, string Language, string mergeMovieName, ref string strErrorMsg)
        {
            LogWrapper.LogInfo("Merging movie with another movie in PlanetM.");
            return DBAObj.MergeMovieWithExistingMovie(MovieName, Language, mergeMovieName, ref strErrorMsg);
        }

        public bool AddNewMovieIMDB(MovieIMDB movie, byte[] poster)
        {
            LogWrapper.LogInfo("Adding new movie in MiniIMDB.");
            return DBAObj.AddNewMovieIMDB(movie, poster);
        }
        public bool UpdateMovieFromIMDB(string MovieName, string Language, Movie movie, byte[] poster)
        {
            LogWrapper.LogInfo("Updating movie details in MiniIMDB.");
            return DBAObj.UpdateMovieFromIMDB(MovieName, Language, movie, poster);
        }
        public bool SynchronizeMovieWithIMDB(string MovieName, string Language, string IMDBID, ref string strErrorMsg)
        {
            LogWrapper.LogInfo("Synchronizing PlanetM movie with MiniIMDB movie.");
            return DBAObj.SynchronizeMovieWithIMDB(MovieName, Language, IMDBID, ref strErrorMsg);
        }
        public bool UpdateMovieImdbAsWishlist(string ImdbID, bool IsWishlist, ref string strErrorMsg)
        {
            LogWrapper.LogInfo("Updating movie as Wishlist\\Non-Wishlist in MiniIMDB.");
            return DBAObj.UpdateMovieImdbAsWishlist(ImdbID, IsWishlist, ref strErrorMsg);
        }
        public DataSet GetMovieDetailsFromIMDB(string ImdbID)
        {
            LogWrapper.LogInfo("Retrieving movie details from MiniIMDB.");
            return DBAObj.GetMovieDetailsFromIMDB(ImdbID);
        }
        public DataSet GetAllMoviesFromIMDB()
        {
            LogWrapper.LogInfo("Retrieving all movies for MiniIMDB.");
            return DBAObj.GetAllMoviesFromIMDB();
        }
        public DataSet GetAllMoviesFromIMDBForPDFExport()
        {
            LogWrapper.LogInfo("Retrieving all movies in MiniIMDB for PDF Export.");
            return DBAObj.GetAllMoviesFromIMDBForPDFExport(); ;
        }
        public DataSet GetMoviesForWishlistFromIMDB()
        {
            LogWrapper.LogInfo("Retrieving all movies for wishlist in MiniIMDB.");
            return DBAObj.GetMoviesForWishlistFromIMDB();
        }
        public DataSet GetAllMoviesFromIMDBBySearchCriteria(AbstractLayer.MovieSearchIMDBCriteria criteria)
        {
            LogWrapper.LogInfo("Retrieving movies from MiniIMDB for given search criteria.");
            return DBAObj.GetAllMoviesFromIMDBBySearchCriteria(criteria);
        }
        public bool DeleteMovieFromIMDB(string ImdbID)
        {
            LogWrapper.LogInfo("Deleting movie from MiniIMDB.");
            return DBAObj.DeleteMovieFromIMDB(ImdbID);
        }

        public bool ImportMyRatingsOnImdb(DataTable dt, ref string strErrorMsg)
        {
            LogWrapper.LogInfo("Importing MyRatings from IMDB Site to PlanetM and MiniIMDB.");
            return DBAObj.ImportMyRatingsOnImdb(dt, ref strErrorMsg);
        }
        public bool ImportMoviesInMiniIMDB(DataTable dt, ref string strErrorMsg)
        {
            LogWrapper.LogInfo("Importing movies from IMDB Site to MiniIMDB.");
            return DBAObj.ImportMoviesInMiniIMDB(dt, ref strErrorMsg);
        }
    }
}
