using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractLayer
{
    public class Movie
    {
        string _Name;
        string _Language;

        bool _IsWatched;
        DateTime _WhenSeen;
        float _Size;
        int _MyRating;
        float _IMDBRating;
        string _IMDBID;
        int _Year;
        string _Location;
        string _Checksum;
        string _Quality;

        public List<string> Genre = new List<string>();
        public List<string> Locations = new List<string>();
        public List<string> Checksums = new List<string>();
        public List<float> LocationSize = new List<float>();

        public string Name
        {
            set
            {
                if (value.Length == 0)
                {
                    throw new Exception("Name is a compulsory field");
                }
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
        public bool IsWatched
        {
            set
            { _IsWatched = value; }
            get
            { return _IsWatched; }
        }
        public DateTime WhenSeen
        {
            set
            { _WhenSeen = value; }
            get
            { return _WhenSeen; }
        }
        public float Size
        {
            set
            { _Size = value; }
            get
            { return _Size; }
        }
        public int MyRating
        {
            set
            { _MyRating = value; }
            get
            { return _MyRating; }
        }
        public int Year
        {
            set
            { _Year = value; }
            get
            { return _Year; }
        }
        public float IMDBRating
        {
            set
            { _IMDBRating = value; }
            get
            { return _IMDBRating; }
        }
        public string IMDBID
        {
            set
            { _IMDBID = value; }
            get
            { return _IMDBID; }
        }
        public string Location
        {
            set
            {
                _Location = value;
            }
            get
            {
                return _Location;
            }
        }
        public string Quality
        {
            set
            {
                _Quality = value;
            }
            get
            {
                return _Quality;
            }
        }
        public string Checksum
        { get { return _Checksum; } set { _Checksum = value; } }
    }
}
