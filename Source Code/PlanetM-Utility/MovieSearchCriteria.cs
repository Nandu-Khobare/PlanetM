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
    public sealed class MovieSearchCriteria
    {
        #region Initialization
        //User Name to whom criteria is
        private string UserName;
        //Folder name under which all criterias shall be stored in isolated memory
        private static string folderName = "PlanetM";
        string _MovieName = string.Empty;
        string _MovieLanguage = "ALL";
        string _MovieYear = "ALL";
        string _MovieSize = "ALL";
        string _MovieMyRating = "ALL";
        string _MovieIMDBRating = "ALL";
        string _MovieIsSeen = "ALL";
        public string MovieName
        {
            get
            {
                return _MovieName;
            }
            set
            {
                _MovieName = value;
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
        public string MovieSize
        {
            get
            {
                return _MovieSize;
            }
            set
            {
                _MovieSize = value;
            }
        }
        public string MovieMyRating
        {
            get
            {
                return _MovieMyRating;
            }
            set
            {
                _MovieMyRating = value;
            }
        }
        public string MovieIMDBRating
        {
            get
            {
                return _MovieIMDBRating;
            }
            set
            {
                _MovieIMDBRating = value;
            }
        }
        public string MovieIsSeen
        {
            get
            {
                return _MovieIsSeen;
            }
            set
            {
                _MovieIsSeen = value;
            }
        }
        /// <summary>
        /// Default constructor required for serialization
        /// </summary>
        public MovieSearchCriteria()
            : this(string.Empty)
        {
        }
        public MovieSearchCriteria(string UserName)
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
            LogWrapper.LogInfo("Saving Search criteria for User : " + UserName);
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
                XmlSerializer xs = new XmlSerializer(typeof(MovieSearchCriteria));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(streamWrite, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, this);
                LogWrapper.LogInfo("Saving Search criteria completed.");
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
        public static MovieSearchCriteria GetLastCriteria(string forTheUser)
        {
            LogWrapper.LogInfo("Retrieving Search criteria for User : " + forTheUser);
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
                    return new MovieSearchCriteria();
                }
                //Deserialize the criteria from isolated memory
                IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, assemblyStorage);
                XmlSerializer xs = new XmlSerializer(typeof(MovieSearchCriteria));
                MovieSearchCriteria searchCriteria = (MovieSearchCriteria)xs.Deserialize(stream);
                return searchCriteria;
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
                MessageHandler.ShowError(ex);
                throw new Exception("Not able to read the last Search Criteria " + ex.ToString());
            }
            finally
            {
                assemblyStorage.Close();
            }
        }
    }
}
