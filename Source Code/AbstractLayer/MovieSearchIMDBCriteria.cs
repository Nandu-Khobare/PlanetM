using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractLayer
{
    public class MovieSearchIMDBCriteria
    {
        string _searchType;
        string _keywords;
        string _Language;
        string _Year;
        string _Rating;
        string _MpaaRating;
        string _IsTop250;
        string _Oscars;
        string _Genre;

        public string SearchType
        {
            set
            {
                if (value.Length == 0)
                {
                    throw new Exception("Search Type is a compulsory field");
                }
                _searchType = value;
            }
            get
            {
                return _searchType;
            }
        }
        public string Keywords
        {
            set
            {
                _keywords = value;
            }
            get
            {
                return _keywords;
            }
        }
        public string Language
        {
            set
            {
                if (value.Length == 0)
                {
                    throw new Exception("Language is a compulsory field");
                }
                _Language = value;
            }
            get
            {
                return _Language;
            }
        }
        public string Year
        {
            set
            { _Year = value; }
            get
            { return _Year; }
        }
        public string Rating
        {
            set
            { _Rating = value; }
            get
            { return _Rating; }
        }
        public string MpaaRating
        {
            set
            { _MpaaRating = value; }
            get
            { return _MpaaRating; }
        }
        public string IsTop250
        {
            set
            { _IsTop250 = value; }
            get
            { return _IsTop250; }
        }
        public string Oscars
        {
            set
            { _Oscars = value; }
            get
            { return _Oscars; }
        }
        public string Genre
        {
            set
            { _Genre = value; }
            get
            { return _Genre; }
        }
    }
}
