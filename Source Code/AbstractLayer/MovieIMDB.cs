using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;

namespace AbstractLayer
{
    public class MovieIMDB
    {
        public string ImdbID { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Language { get; set; }
        public string Year { get; set; }
        public string Rating { get; set; }
        public string MyRating { get; set; }
        public ArrayList Genres { get; set; }
        public ArrayList Directors { get; set; }
        public ArrayList Writers { get; set; }
        public ArrayList Stars { get; set; }
        public ArrayList Cast { get; set; }
        public ArrayList Producers { get; set; }
        public ArrayList Musicians { get; set; }
        public ArrayList Cinematographers { get; set; }
        public ArrayList Editors { get; set; }
        public string MpaaRating { get; set; }
        public string ReleaseDate { get; set; }
        public string Plot { get; set; }
        public ArrayList PlotKeywords { get; set; }
        public string PosterURL { get; set; }
        public string PosterLargeFullURL { get; set; }
        public string PosterLargeURL { get; set; }
        public string PosterSmallURL { get; set; }
        public Image Poster { get; set; }
        public string Runtime { get; set; }
        public string Top250 { get; set; }
        public string Oscars { get; set; }
        public string Awards { get; set; }
        public string Nominations { get; set; }
        public string Storyline { get; set; }
        public string Tagline { get; set; }
        public string Votes { get; set; }
        public ArrayList ReleaseDates { get; set; }
        public ArrayList MediaImages { get; set; }
        public string ImdbURL { get; set; }
    }
}
