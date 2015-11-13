using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractLayer
{
    public struct StoredProcedure
    {
        public const string AddNewMovieInPlanetM = "uspAddMovie";
        public const string AddNewMovieInPlanetMFromIMDB = "uspAddMovieFromIMDB";

        public const string UpdateMovieInPlanetM = "uspUpdateMovie";
        public const string UpdateMovieCoverInPlanetM = "uspUpdateMovieCover";
        public const string UpdateMovieChecksumInPlanetM = "uspUpdateMovieChecksum";
        public const string UpdateMovieAsSeenUnseenInPlanetM = "uspUpdateMovieAsSeenUnseen";
        public const string UpdateMovieInPlanetMFromMiniIMDB = "uspUpdateMovieFromIMDB";

        public const string GetMovieCoverFromPlanetM = "uspGetMovieCover";
        public const string GetMovieDetailsByNameLanguageFromPlanetM = "uspGetMovieDetailsByNameLanguage";
        public const string GetMovieDetailsByImdbIdFromPlanetM = "uspGetMovieDetailsByIMDBID";
        public const string GetAllMoviesBySearchCriteriaFromPlanetM = "uspGetAllMoviesBySearchCriteria";
        public const string GetAllMoviesFromPlanetM = "uspGetAllMovies";
        public const string GetAllMoviesForPDFExportFromPlanetM = "uspGetAllMoviesForPDFExport";
        public const string GetAllMovieCoversFromPlanetM = "uspGetAllMovieCovers";
        public const string GetAllMovieDuplicatesFromPlanetM = "uspGetAllMovieDuplicates";
        public const string GetAllRecentMoviesFromPlanetM = "uspGetRecentMovies";
        public const string GetAllWatchlistMoviesFromPlanetM = "uspGetWatchlistMovies";

        public const string DeleteMovieFromPlanetM = "uspDeleteMovie";
        public const string DeleteMovieLocationFromPlanetM = "uspDeleteMovieLocation";

        public const string MergeMovieInPlanetM = "uspMergeMovieWithExisting";
        public const string SynchronizeMovieInPlanetMFromMiniIMDB = "uspSynchronizeMovieWithIMDB";

        public const string AddNewMovieInMiniIMDB = "uspAddMovieInImdb";
        public const string UpdateMovieAsWishlistInMiniIMDB = "uspUpdateMovieImdbAsWishlist";
        public const string GetMovieDetailsByImdbIdFromMiniIMDB = "uspGetMovieDetailsFromImdbByID";
        public const string GetAllMoviesBySearchCriteriaFromMiniIMDB = "uspGetAllMoviesFromIMDBBySearchCriteria";
        public const string GetAllMoviesFromMiniIMDB = "uspGetAllMoviesFromIMDB";
        public const string GetAllWishlistMoviesFromMiniIMDB = "uspGetWishlistMoviesFromIMDB";
        public const string GetAllMoviesForPDFExportFromMiniIMDB = "uspGetAllMoviesFromIMDBForPDFExport";
        public const string DeleteMovieFromMiniIMDB = "uspDeleteMovieFromIMDB";

        public const string ImportMyRatingsFromImdbSiteInPlanetM_MiniIMDB = "uspImportMyRatingsOnImdb";
        public const string ImportMoviesFromImdbSiteInMiniIMDB = "uspImportMoviesInMiniIMDB";

    }
}
