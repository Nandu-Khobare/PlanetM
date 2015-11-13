using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractLayer
{
    public class MovieSearchCriteria
    {
        string _Name;
        string _Language;

        string _IsWatched;
        string _Size;
        string _MyRating;
        string _IMDBRating;
        string _Year;
        string _Genre;

        public string Name
        {
            set
            {
                _Name = value;
            }
            get
            {
                return _Name;
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
        public string IsWatched
        {
            set
            { _IsWatched = value; }
            get
            { return _IsWatched; }
        }
        public string Size
        {
            set
            { _Size = value; }
            get
            { return _Size; }
        }
        public string MyRating
        {
            set
            { _MyRating = value; }
            get
            { return _MyRating; }
        }
        public string IMDBRating
        {
            set
            { _IMDBRating = value; }
            get
            { return _IMDBRating; }
        }
        public string Year
        {
            set
            { _Year = value; }
            get
            { return _Year; }
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
