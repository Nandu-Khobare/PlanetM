using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbstractLayer;
using System.Data;

namespace DBAccessLayer
{
    /// <summary>
    /// Summary description for IdataAccess.
    /// </summary>
    public interface IDataAccess
    {
        //all the method signature goes here		
        bool AddNewMovie(Movie movie);
        bool AddNewMovieFromIMDB(string ImdbID, ref string strErrorMsg);
        bool DeleteMovie(Movie movie);
        bool UpdateMovie(string MovieName,string Language, Movie movie);
        DataSet GetAllMovies();
        DataSet GetAllMoviesForPDFExport();
        DataSet GetAllMoviesBySearchCriteria(MovieSearchCriteria criteria);
        bool UpdateCover(Movie mov,byte[] pic);
        bool UpdateMovieChecksum(Movie mov);
        bool UpdateMovieAsSeenUnseen(string MovieName, string Language, bool IsWatched, ref string strErrorMsg);
        byte[] GetCover(Movie mov);
        DataSet GetAllMovieCovers();
        DataSet GetAllMovieDuplicates();
        DataSet GetRecentMovies();
        DataSet GetWatchlistMovies();
        DataSet GetDiscrepancyReport(DiscrepancyReport report);
        DataSet GetMovieDetails(string Name,string Language);
        DataSet GetMovieDetails(string ImdbID);
        bool DeleteMovieLocation(string Location);
        bool MergeMovieWithExistingMovie(string MovieName, string Language, string mergeMovieName, ref string strErrorMsg);

        bool AddNewMovieIMDB(MovieIMDB movie,byte[] poster);
        bool UpdateMovieFromIMDB(string MovieName, string Language, Movie movie, byte[] poster);
        bool SynchronizeMovieWithIMDB(string MovieName, string Language, string IMDBID, ref string strErrorMsg);
        bool UpdateMovieImdbAsWishlist(string ImdbID, bool IsWishlist, ref string strErrorMsg);
        DataSet GetMovieDetailsFromIMDB(string ImdbID);
        DataSet GetAllMoviesFromIMDB();
        DataSet GetAllMoviesFromIMDBForPDFExport();
        DataSet GetMoviesForWishlistFromIMDB();
        DataSet GetAllMoviesFromIMDBBySearchCriteria(MovieSearchIMDBCriteria criteria);
        bool DeleteMovieFromIMDB(string ImdbID);

        bool ImportMyRatingsOnImdb(DataTable dt,ref string strErrorMsg);
        bool ImportMoviesInMiniIMDB(DataTable dt, ref string strErrorMsg);
    }
}
