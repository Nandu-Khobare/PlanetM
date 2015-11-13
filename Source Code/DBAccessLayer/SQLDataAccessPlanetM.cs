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
    public partial class SQLDataAccess : IDataAccess
    {
        readonly string m_Connection_String;
        public SQLDataAccess(string _ConnectionString)
        {
            if (_ConnectionString != null && _ConnectionString.Length > 50)
                m_Connection_String = _ConnectionString;
            else
            {
                string dbSource = PlanetM_Utility.Configuration.ReadConfig("DBSource");
                string dbFilePath = Path.Combine(Environment.CurrentDirectory, @"Data\" + PlanetM_Utility.Configuration.ReadConfig("DBFileName"));
                if (File.Exists(dbFilePath))
                    m_Connection_String = string.Format(@"Data Source={0};AttachDbFilename={1};Integrated Security=True;Connect Timeout=30;User Instance=True", dbSource, dbFilePath);
                else
                    m_Connection_String = string.Format(@"Data Source={0};AttachDbFilename={1}\{2};Integrated Security=True;Connect Timeout=30;User Instance=True", dbSource, PlanetM_Utility.Configuration.ReadConfig("DBFilePath"), PlanetM_Utility.Configuration.ReadConfig("DBFileName"));
            }
        }

        public bool AddNewMovie(Movie movie)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@name", movie.Name);
                arrParam[1] = new SqlParameter("@language", movie.Language);
                arrParam[2] = new SqlParameter("@location", movie.Location);
                arrParam[3] = new SqlParameter("@size", movie.Size);
                arrParam[4] = new SqlParameter("@year", movie.Year);

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.AddNewMovieInPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.AddNewMovieInPlanetM, arrParam);
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

        public bool AddNewMovieFromIMDB(string ImdbID, ref string strErrorMsg)
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
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.AddNewMovieInPlanetMFromIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.AddNewMovieInPlanetMFromIMDB, arrParam);
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

        public bool UpdateMovie(string MovieName, string Language, Movie movie)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[10];

                arrParam[0] = new SqlParameter("@name", MovieName);
                arrParam[1] = new SqlParameter("@language", Language);
                arrParam[2] = new SqlParameter("@newName", movie.Name);
                arrParam[3] = new SqlParameter("@newLanguage", movie.Language);
                arrParam[4] = new SqlParameter("@isWatched", movie.IsWatched);
                arrParam[5] = new SqlParameter("@myRating", movie.MyRating);
                arrParam[6] = new SqlParameter("@year", movie.Year);
                arrParam[7] = new SqlParameter("@seendate", movie.WhenSeen);
                arrParam[8] = new SqlParameter("@quality", movie.Quality);
                StringBuilder Genre = new StringBuilder();
                foreach (string gen in movie.Genre)
                {
                    Genre.Append(gen + ",");
                }
                if (movie.Genre.Count != 0)
                    Genre.Remove(Genre.Length - 1, 1);
                arrParam[9] = new SqlParameter("@genre", Genre.ToString());

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.UpdateMovieInPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.UpdateMovieInPlanetM, arrParam);
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

        public DataSet GetAllMovies()
        {
            DataSet dtStMovies = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllMoviesFromPlanetM));
                dtStMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMoviesFromPlanetM);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dtStMovies;
        }

        public DataSet GetAllMoviesForPDFExport()
        {
            DataSet dtStMovies = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllMoviesForPDFExportFromPlanetM));
                dtStMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMoviesForPDFExportFromPlanetM);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dtStMovies;
        }

        public bool UpdateCover(Movie movie, byte[] pic)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@name", movie.Name);
                arrParam[1] = new SqlParameter("@language", movie.Language);
                arrParam[2] = new SqlParameter("@cover", pic);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.UpdateMovieCoverInPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.UpdateMovieCoverInPlanetM, arrParam);
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

        public bool UpdateMovieAsSeenUnseen(string MovieName, string Language, bool IsWatched, ref string strErrorMsg)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@name", MovieName);
                arrParam[1] = new SqlParameter("@language", Language);
                arrParam[2] = new SqlParameter("@isWatched", IsWatched);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.UpdateMovieAsSeenUnseenInPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.UpdateMovieAsSeenUnseenInPlanetM, arrParam);
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

        public byte[] GetCover(Movie movie)
        {
            DataSet ds;
            DataRow myRow;
            SqlParameter[] arrParam = new SqlParameter[2];
            byte[] imageData = new byte[0];
            try
            {
                arrParam[0] = new SqlParameter("@name", movie.Name);
                arrParam[1] = new SqlParameter("@language", movie.Language);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.GetMovieCoverFromPlanetM, sbSPParams.ToString()));
                ds = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetMovieCoverFromPlanetM, arrParam);
                myRow = ds.Tables[0].Rows[0];
                if (!myRow.IsNull(0))
                    imageData = (byte[])myRow["Cover"];
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return imageData;
        }

        public DataSet GetAllMovieCovers()
        {
            DataSet dsCovers = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllMovieCoversFromPlanetM));
                dsCovers = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMovieCoversFromPlanetM);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dsCovers;
        }

        public DataSet GetAllMovieDuplicates()
        {
            DataSet dsDuplicates = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllMovieDuplicatesFromPlanetM));
                dsDuplicates = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMovieDuplicatesFromPlanetM);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dsDuplicates;
        }

        public DataSet GetRecentMovies()
        {
            DataSet dsRecentMovies = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllRecentMoviesFromPlanetM));
                dsRecentMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllRecentMoviesFromPlanetM);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dsRecentMovies;
        }

        public DataSet GetWatchlistMovies()
        {
            DataSet dsWishlist = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", StoredProcedure.GetAllWatchlistMoviesFromPlanetM));
                dsWishlist = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllWatchlistMoviesFromPlanetM);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dsWishlist;
        }

        public DataSet GetDiscrepancyReport(DiscrepancyReport report)
        {
            DataSet dsDiscrepancy = null;
            try
            {
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameters None.", report.StoredProcedureForReport));
                dsDiscrepancy = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, report.StoredProcedureForReport);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return dsDiscrepancy;
        }

        public bool UpdateMovieChecksum(Movie movie)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@name", movie.Name);
                arrParam[1] = new SqlParameter("@language", movie.Language);
                arrParam[2] = new SqlParameter("@location", movie.Location);
                arrParam[3] = new SqlParameter("@checksum", movie.Checksum);

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.UpdateMovieChecksumInPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.UpdateMovieChecksumInPlanetM, arrParam);
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

        public DataSet GetMovieDetails(string Name, string Language)
        {
            DataSet ds = new DataSet();
            SqlParameter[] arrParam = new SqlParameter[2];
            try
            {
                arrParam[0] = new SqlParameter("@name", Name);
                arrParam[1] = new SqlParameter("@language", Language);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.GetMovieDetailsByNameLanguageFromPlanetM, sbSPParams.ToString()));
                ds = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetMovieDetailsByNameLanguageFromPlanetM, arrParam);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return ds;
        }

        public DataSet GetMovieDetails(string ImdbID)
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
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.GetMovieDetailsByImdbIdFromPlanetM, sbSPParams.ToString()));
                ds = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetMovieDetailsByImdbIdFromPlanetM, arrParam);
            }
            catch (Exception Ex)
            {
                PlanetM_Utility.LogWrapper.LogError(Ex);
            }
            return ds;
        }

        public bool DeleteMovie(Movie movie)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@name", movie.Name);
                arrParam[1] = new SqlParameter("@language", movie.Language);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.DeleteMovieFromPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.DeleteMovieFromPlanetM, arrParam);
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

        public bool DeleteMovieLocation(string Location)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@location", Location);
                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.DeleteMovieLocationFromPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.DeleteMovieLocationFromPlanetM, arrParam);
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

        public bool MergeMovieWithExistingMovie(string MovieName, string Language, string mergeMovieName, ref string strErrorMsg)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@name", MovieName);
                arrParam[1] = new SqlParameter("@language", Language);
                arrParam[2] = new SqlParameter("@mergeMovieName", mergeMovieName);

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.MergeMovieInPlanetM, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.MergeMovieInPlanetM, arrParam);
            }
            catch (SqlException Ex)
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

        public DataSet GetAllMoviesBySearchCriteria(MovieSearchCriteria criteria)
        {
            DataSet dtStMovies;
            SqlParameter[] arrParam = new SqlParameter[8];
            arrParam[0] = new SqlParameter("@name", criteria.Name);
            arrParam[1] = new SqlParameter("@language", criteria.Language);
            arrParam[2] = new SqlParameter("@myRating", criteria.MyRating);
            arrParam[3] = new SqlParameter("@year", criteria.Year);
            arrParam[4] = new SqlParameter("@genre", criteria.Genre);
            arrParam[5] = new SqlParameter("@seen", criteria.IsWatched);
            arrParam[6] = new SqlParameter("@imdbRating", criteria.IMDBRating);
            arrParam[7] = new SqlParameter("@size", criteria.Size);

            StringBuilder sbSPParams = new StringBuilder();
            foreach (var param in arrParam)
            {
                sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
            }
            sbSPParams.Remove(sbSPParams.Length - 3, 3);
            PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.GetAllMoviesBySearchCriteriaFromPlanetM, sbSPParams.ToString()));
            dtStMovies = SqlHelper.ExecuteDataset(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.GetAllMoviesBySearchCriteriaFromPlanetM, arrParam);
            return dtStMovies;
        }

        public bool UpdateMovieFromIMDB(string MovieName, string Language, Movie movie, byte[] poster)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[9];

                arrParam[0] = new SqlParameter("@name", MovieName);
                arrParam[1] = new SqlParameter("@language", Language);
                arrParam[2] = new SqlParameter("@newName", movie.Name);
                arrParam[3] = new SqlParameter("@newLanguage", movie.Language);
                arrParam[4] = new SqlParameter("@imdbID", movie.IMDBID);
                arrParam[5] = new SqlParameter("@imdbRating", movie.IMDBRating);
                arrParam[6] = new SqlParameter("@year", movie.Year);
                arrParam[7] = new SqlParameter("@cover", poster);
                StringBuilder Genre = new StringBuilder();
                foreach (string gen in movie.Genre)
                {
                    Genre.Append(gen + ",");
                }
                if (movie.Genre.Count != 0)
                    Genre.Remove(Genre.Length - 1, 1);
                arrParam[8] = new SqlParameter("@genre", Genre.ToString());

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.UpdateMovieInPlanetMFromMiniIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.UpdateMovieInPlanetMFromMiniIMDB, arrParam);
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

        public bool SynchronizeMovieWithIMDB(string MovieName, string Language, string IMDBID, ref string strErrorMsg)
        {
            SqlTransaction objTrans = null;
            SqlConnection myConnection = new SqlConnection(m_Connection_String);
            try
            {
                myConnection.Open();
                objTrans = myConnection.BeginTransaction();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@name", MovieName);
                arrParam[1] = new SqlParameter("@language", Language);
                arrParam[2] = new SqlParameter("@imdbID", IMDBID);

                StringBuilder sbSPParams = new StringBuilder();
                foreach (var param in arrParam)
                {
                    sbSPParams.Append(param.ParameterName + ":" + param.Value + " & ");
                }
                sbSPParams.Remove(sbSPParams.Length - 3, 3);
                PlanetM_Utility.LogWrapper.LogInfo(string.Format("Executing SP:{0} Parameter Details>> {1}", StoredProcedure.SynchronizeMovieInPlanetMFromMiniIMDB, sbSPParams.ToString()));
                SqlHelper.ExecuteNonQuery(m_Connection_String, CommandType.StoredProcedure, StoredProcedure.SynchronizeMovieInPlanetMFromMiniIMDB, arrParam);
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