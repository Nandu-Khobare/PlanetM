using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using AbstractLayer;
using System.IO;

namespace DBAccessLayer
{
    partial class SQLDataAccess : IDataAccess
    {
        public bool AddNewMovieIMDB(MovieIMDB movie, byte[] poster)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[23];

                arrParam[0] = new SqlParameter("@imdbID", movie.ImdbID);
                arrParam[1] = new SqlParameter("@title", movie.Title);
                arrParam[2] = new SqlParameter("@language", movie.Language);
                arrParam[3] = new SqlParameter("@year", movie.Year);
                arrParam[4] = new SqlParameter("@rating", movie.Rating);
                StringBuilder sb = new StringBuilder();
                foreach (string gen in movie.Genres)
                {
                    sb.Append(gen + ",");
                }
                if (movie.Genres.Count != 0)
                    sb.Remove(sb.Length - 1, 1);
                arrParam[5] = new SqlParameter("@genre", sb.ToString());
                arrParam[6] = new SqlParameter("@mpaaRating", movie.MpaaRating);
                arrParam[7] = new SqlParameter("@releaseDate", movie.ReleaseDate);
                arrParam[8] = new SqlParameter("@runtime", movie.Runtime);

                arrParam[9] = new SqlParameter("@tagline", movie.Tagline);
                arrParam[10] = new SqlParameter("@plot", movie.Plot);
                arrParam[11] = new SqlParameter("@storyline", movie.Storyline);

                arrParam[12] = new SqlParameter("@poster", poster);
                arrParam[13] = new SqlParameter("@posterURL", movie.PosterURL);
                arrParam[14] = new SqlParameter("@awards", movie.Awards);
                arrParam[15] = new SqlParameter("@nominations", movie.Nominations);
                arrParam[16] = new SqlParameter("@top250", movie.Top250);
                arrParam[17] = new SqlParameter("@oscars", movie.Oscars);
                arrParam[18] = new SqlParameter("@imdbURL", movie.ImdbURL);

                sb.Clear();
                foreach (string director in movie.Directors)
                {
                    sb.Append(director + "~");
                }
                if (movie.Directors.Count != 0)
                    sb.Remove(sb.Length - 1, 1);
                arrParam[19] = new SqlParameter("@directors", sb.ToString());
                sb.Clear();
                foreach (string star in movie.Stars)
                {
                    sb.Append(star + "~");
                }
                if (movie.Stars.Count != 0)
                    sb.Remove(sb.Length - 1, 1);
                arrParam[20] = new SqlParameter("@stars", sb.ToString());
                sb.Clear();
                foreach (string writer in movie.Writers)
                {
                    sb.Append(writer + "~");
                }
                if (movie.Writers.Count != 0)
                    sb.Remove(sb.Length - 1, 1);
                arrParam[21] = new SqlParameter("@writers", sb.ToString());
                sb.Clear();
                foreach (string cast in movie.Cast)
                {
                    sb.Append(cast + "~");
                }
                if (movie.Cast.Count != 0)
                    sb.Remove(sb.Length - 1, 1);
                arrParam[22] = new SqlParameter("@cast", sb.ToString());

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.AddNewMovieInMiniIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.AddNewMovieInMiniIMDB, arrParam);
            }
            catch (Exception Ex)
            {
                objTrans.Rollback();
                PlanetM_Utility.LogWrapper.LogError(Ex);
                return false;
            }
            finally
            {
                myConnection.Close();
            }
            return true;
        }
        
        public bool UpdateMovieImdbAsWishlist(string ImdbID, bool IsWishlist, ref string strErrorMsg)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@imdbID", ImdbID);
                arrParam[1] = new SqlParameter("@isWishlist", IsWishlist);

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.UpdateMovieAsWishlistInMiniIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.UpdateMovieAsWishlistInMiniIMDB, arrParam);
            }
            catch (Exception Ex)
            {
                objTrans.Rollback();
                PlanetM_Utility.LogWrapper.LogError(Ex);
                strErrorMsg = Ex.Message.ToString();
                return false;
            }
            finally
            {
                myConnection.Close();
            }
            return true;
        }
        
        public DataSet GetMovieDetailsFromIMDB(string ImdbID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] arrParam = new SqlParameter[1];
            try
            {
                arrParam[0] = new SqlParameter("@imdbID", ImdbID);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.GetMovieDetailsByImdbIdFromMiniIMDB, sbSPParams.ToString()));
                ds = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetMovieDetailsByImdbIdFromMiniIMDB, arrParam);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return ds;
        }
        
        public DataSet GetAllMoviesFromIMDB()
        {
            DataSet dtStMovies = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllMoviesFromMiniIMDB));
                dtStMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMoviesFromMiniIMDB);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dtStMovies;
        }

        public DataSet GetMoviesForWishlistFromIMDB()
        {
            DataSet dtStMovies=null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllWishlistMoviesFromMiniIMDB));
                dtStMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllWishlistMoviesFromMiniIMDB);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dtStMovies;
        }

        public DataSet GetAllMoviesFromIMDBForPDFExport()
        {
            DataSet dtStMovies = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllMoviesForPDFExportFromMiniIMDB));
                dtStMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMoviesForPDFExportFromMiniIMDB);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dtStMovies;
        }

        public DataSet GetAllMoviesFromIMDBBySearchCriteria(MovieSearchIMDBCriteria criteria)
        {
            DataSet dtStMovies;
            SqlParameter[] arrParam = new SqlParameter[9];
            arrParam[0] = new SqlParameter("@searchType", criteria.SearchType);
            arrParam[1] = new SqlParameter("@keywords", criteria.Keywords);
            arrParam[2] = new SqlParameter("@language", criteria.Language);
            arrParam[3] = new SqlParameter("@year", criteria.Year);
            arrParam[4] = new SqlParameter("@rating", criteria.Rating);
            arrParam[5] = new SqlParameter("@mpaaRating", criteria.MpaaRating);
            arrParam[6] = new SqlParameter("@isTop250", criteria.IsTop250);
            arrParam[7] = new SqlParameter("@oscars", criteria.Oscars);
            arrParam[8] = new SqlParameter("@genre", criteria.Genre);

            StringBuilder sbSPParams = new StringBuilder();
            foreach (var param in arrParam)
            {
                sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
            }
            sbSPParams.Remove(sbSPParams.Length - 3, 3);
            PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.GetAllMoviesBySearchCriteriaFromMiniIMDB, sbSPParams.ToString()));
            dtStMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMoviesBySearchCriteriaFromMiniIMDB, arrParam);
            return dtStMovies;
        }
        
        public bool DeleteMovieFromIMDB(string ImdbID)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@imdbID", ImdbID);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.DeleteMovieFromMiniIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.DeleteMovieFromMiniIMDB, arrParam);
            }
            catch (Exception Ex)
            {
                objTrans.Rollback();
                PlanetM_Utility.LogWrapper.LogError(Ex);
                return false;
            }
            finally
            {
                myConnection.Close();
            }
            return true;
        }

        public bool ImportMyRatingsOnImdb(DataTable dt, ref string strErrorMsg)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@myRatings", dt);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.ImportMyRatingsFromImdbSiteInPlanetM_MiniIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.ImportMyRatingsFromImdbSiteInPlanetM_MiniIMDB, arrParam);
            }
            catch (SqlException Ex)
            {
                objTrans.Rollback();
                PlanetM_Utility.LogWrapper.LogError(Ex);
                strErrorMsg = Ex.Message.ToString();
                return false;
            }
            finally
            {
                myConnection.Close();
            }
            return true;
        }

        public bool ImportMoviesInMiniIMDB(DataTable dt, ref string strErrorMsg)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@movies", dt);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.ImportMoviesFromImdbSiteInMiniIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.ImportMoviesFromImdbSiteInMiniIMDB, arrParam);
            }
            catch (SqlException Ex)
            {
                objTrans.Rollback();
                PlanetM_Utility.LogWrapper.LogError(Ex);
                strErrorMsg = Ex.Message.ToString();
                return false;
            }
            finally
            {
                myConnection.Close();
            }
            return true;
        }
    }
}
