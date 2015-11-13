using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Collections;
using AbstractLayer;

namespace PlanetM_Utility
{
    class MoviePathSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            Hashtable pathCollection = new Hashtable();
            MoviePathInfo moviePathInfo;
            LogWrapper.LogInfo("Retrieving movie folders from configuration for scanning.");
            try
            {
                XmlNode xNode;
                XmlElement rootElement = (XmlElement)section;
                XmlNodeList xmlNodes = rootElement.SelectNodes("//Path");
                foreach (XmlNode nodeLog in xmlNodes)
                {
                    moviePathInfo = new MoviePathInfo();
                    xNode = nodeLog.SelectSingleNode("MoviePath");
                    moviePathInfo.MoviePath = xNode.InnerText.ToString().Trim();
                    xNode = nodeLog.SelectSingleNode("MovieLanguage");
                    moviePathInfo.MovieLanguage = xNode.InnerText.ToString().Trim();
                    xNode = nodeLog.SelectSingleNode("Order");
                    moviePathInfo.Order = Convert.ToInt32(xNode.InnerText);
                    pathCollection.Add(moviePathInfo.Order, moviePathInfo);
                    LogWrapper.LogInfo(string.Format("Added path to ScanList. Details are:: Order : {0} Path : '{1}' Language : {2}", moviePathInfo.Order, moviePathInfo.MoviePath, moviePathInfo.MovieLanguage));
                }
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            return pathCollection;
        }

        #endregion
    }

    class StopWordSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            Hashtable wordCollection = new Hashtable();
            LogWrapper.LogInfo("Retrieving stopwords from configuration.");
            try
            {
                string stopword;
                XmlElement rootElement = (XmlElement)section;
                XmlNodeList xmlNodes = rootElement.SelectNodes("//Word");
                foreach (XmlNode xNode in xmlNodes)
                {
                    stopword = xNode.InnerText.ToString().Trim();
                    wordCollection.Add(stopword, stopword);
                    LogWrapper.LogInfo(string.Format("Added stopword : {0}", stopword));
                }
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            return wordCollection;
        }

        #endregion
    }

}
