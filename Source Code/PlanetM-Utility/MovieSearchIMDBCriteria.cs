using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace PlanetM_Utility
{
    /// <summary>
    /// Serializable class for searching movies from Search form
    /// </summary>
    [Serializable]
    public sealed class MovieSearchIMDBCriteria
    {
        #region Initialization
        //User Name to whom criteria is
        private string UserName;
        //Folder name under which all criterias shall be stored in isolated memory
        private static string folderName = "MiniIMDB";
        string _SearchType = "Title";
        string _SearchKeywords = string.Empty;
        string _MovieLanguage = "ALL";
        string _MovieYear = "ALL";
        string _MovieRating = "ALL";
        string _MovieMpaaRating = "ALL";
        string _MovieTop250 = "ALL";
        string _MovieOscars = "ALL";
        public string SearchType
        {
            get
            {
                return _SearchType;
            }
            set
            {
                _SearchType = value;
            }
        }
        public string SearchKeywords
        {
            get
            {
                return _SearchKeywords;
            }
            set
            {
                _SearchKeywords = value;
            }
        }
        public string MovieLanguage
        {
            get
            {
                return _MovieLanguage;
            }
            set
            {
                _MovieLanguage = value;
            }
        }
        public string MovieYear
        {
            get
            {
                return _MovieYear;
            }
            set
            {
                _MovieYear = value;
            }
        }
        public string MovieRating
        {
            get
            {
                return _MovieRating;
            }
            set
            {
                _MovieRating = value;
            }
        }
        public string MovieMpaaRating
        {
            get
            {
                return _MovieMpaaRating;
            }
            set
            {
                _MovieMpaaRating = value;
            }
        }
        public string MovieTop250
        {
            get
            {
                return _MovieTop250;
            }
            set
            {
                _MovieTop250 = value;
            }
        }
        public string MovieOscars
        {
            get
            {
                return _MovieOscars;
            }
            set
            {
                _MovieOscars = value;
            }
        }
        /// <summary>
        /// Default constructor required for serialization
        /// </summary>
        public MovieSearchIMDBCriteria()
            : this(string.Empty)
        {
        }
        public MovieSearchIMDBCriteria(string UserName)
        {
            this.UserName = UserName.Trim();
        }
        #endregion

        static private string GetFileName(string forTheUser)
        {
            //returns complete file name
            return string.Format("{0}\\{1}.xml", folderName, forTheUser);
        }
        /// <summary>
        /// Serializes criteria object under isolated memory
        /// </summary>
        public void Save()
        {
            LogWrapper.LogInfo("Saving iSearch criteria for User : " + UserName);
            IsolatedStorageFile assemblyStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            //True when saving first time
            if (assemblyStorage.GetDirectoryNames(folderName).Length == 0)
            {
                assemblyStorage.CreateDirectory(folderName);
                LogWrapper.LogInfo("Created folder : " + folderName);
            }
            IsolatedStorageFileStream streamWrite;
            try
            {
                streamWrite = new IsolatedStorageFileStream(GetFileName(UserName), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, assemblyStorage);
                XmlSerializer xs = new XmlSerializer(typeof(MovieSearchIMDBCriteria));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(streamWrite, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, this);
                LogWrapper.LogInfo("Saving iSearch criteria completed.");
            }
            finally
            {
                assemblyStorage.Close();
            }
        }
        /// <summary>
        /// Static function. Gets criteria from isolated memory for the requested user
        /// </summary>
        /// <param name="forTheUser">To whom criteria requested for</param>
        /// <returns></returns>
        public static MovieSearchIMDBCriteria GetLastCriteria(string forTheUser)
        {
            LogWrapper.LogInfo("Retrieving iSearch criteria for User : " + forTheUser);
            IsolatedStorageFile assemblyStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            try
            {
                if (assemblyStorage.GetDirectoryNames(folderName).Length == 0)
                {
                    assemblyStorage.CreateDirectory(folderName);
                    LogWrapper.LogInfo("Created folder : " + folderName);
                }
                string fileName = GetFileName(forTheUser);
                //If no files found return empty criteria.
                if (assemblyStorage.GetFileNames(fileName).Length == 0)
                {
                    LogWrapper.LogInfo("No files found with name : " + fileName);
                    return new MovieSearchIMDBCriteria();
                }
                //Deserialize the criteria from isolated memory
                IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, assemblyStorage);
                XmlSerializer xs = new XmlSerializer(typeof(MovieSearchIMDBCriteria));
                MovieSearchIMDBCriteria searchCriteria = (MovieSearchIMDBCriteria)xs.Deserialize(stream);
                return searchCriteria;
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
                MessageHandler.ShowError(ex);
                throw new Exception("Not able to read the last iSearch Criteria " + ex.ToString());
            }
            finally
            {
                assemblyStorage.Close();
            }
        }
    }
}
