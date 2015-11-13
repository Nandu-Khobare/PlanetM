using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using System.IO;

namespace PlanetM_Utility
{
    /// <summary>
    /// This class has members to read the data from application configuration file,XML file and userStoreForAssembly
    /// </summary>
    public static class Configuration
    {
        static XDocument document;
        # region [Function ReadConfig]
        /// <FunctionName> ReadConfig</FunctionName>
        /// <param />
        /// <returns>String </returns>
        /// <summary>This function read the config value for the given Key</summary>
        /// <exception cref=" "/>
        /// <history>
        ///
        /// </history>
        #endregion
        public static string ReadConfig(string key)
        {
            if (key == "" || key.Trim() == null)
            {
                LogWrapper.LogError("Invalid Key!");
                throw new Exception("ReadAppConfig Error - Invalid Key!");
            }
            try
            {
                return ConfigurationManager.AppSettings[key].ToString().Trim();
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
                return null;
            }
        }
        # region [Function GetConfigurationValues]
        /// <FunctionName> GetConfigurationValues</FunctionName>
        /// <param> Element name will be input [string ]</param>
        ///
        /// <returns>list of string values[array] </returns>
        /// <summary>This function gets the config value from XML file for the given node</summary>
        /// <exception cref=" "/>
        /// <history>
        ///
        /// </history>
        #endregion
        public static string[] GetConfigurationValues(string element)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @ReadConfig("ConfigFile"));
            return LoadDocument(path, element);
        }
        # region [Function LoadDocument]
        /// <FunctionName> LoadDocument</FunctionName>
        /// <param> XML path will be input [string ]</param>
        /// <param> Element name will be input [string ]</param>
        ///
        /// <returns>list of string values[array] </returns>
        /// <summary>This function gets the config value from XML file for the given node</summary>
        /// <exception cref=" "/>
        /// <history>
        ///
        /// </history>
        #endregion
        private static string[] LoadDocument(string path, string element)
        {
            try
            {
                if (document == null)
                    document = XDocument.Load(path);
                var elements = from mainElement in document.Descendants(element)
                               select mainElement.Elements("key");
                string[] elementvalues = new string[0];
                foreach (IEnumerable<XElement> ele in elements)
                {
                    elementvalues = new string[ele.Count()];
                    for (int loopnumber = 0; loopnumber <= ele.Count() - 1; loopnumber++)
                        elementvalues[loopnumber] = ele.ElementAt(loopnumber).Value;
                }
                if (elementvalues.Length > 0)
                    return elementvalues;
                return null;
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
                throw new Exception("Method:load document " + ex.ToString());
            }
        }
    }
}
