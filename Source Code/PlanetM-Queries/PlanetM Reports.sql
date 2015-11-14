--Recently Watched Movies
SELECT --TOP 1000
M.Name,M.Language,MI.SeenDate,MI.Year,
MI.IMDBID,MI.IMDBRating,M.MyRating,M.Genre,MII.Oscars,MII.Top250
FROM Movies M 
INNER JOIN MovieInfo MI 
ON M.Name=MI.Name AND M.Language=MI.Language
LEFT JOIN MovieInfoIMDB MII
ON MI.IMDBID = MII.IMDBID
WHERE M.IsWatched=1 AND SeenDate > GetDate()-30
ORDER BY SeenDate DESC

SELECT DISTINCT 
	MD.Director,Count(*) NoOfMovies
FROM MovieDirectors MD
INNER JOIN MovieInfoIMDB MII
	ON MD.IMDBID = MII.IMDBID
--WHERE MII.Top250 > 0
GROUP BY MD.Director
HAVING Count(*)>4
ORDER BY NoOfMovies DESC



SELECT --TOP 1000
M.Name,M.Language,MI.IMDBID,
MI.Year,MII.Oscars,MI.IMDBRating,M.MyRating,M.Genre,Location
FROM Movies M 
INNER JOIN MovieInfo MI 
ON M.Name=MI.Name AND M.Language=MI.Language
LEFT JOIN MovieLocation ML
ON M.Name=ML.Name AND M.Language=ML.Language
INNER JOIN MovieInfoIMDB MII
ON MI.IMDBID = MII.IMDBID
WHERE (MII.Oscars>0 OR Rating>8 OR M.MyRating>8) AND (Location like '%unseen%' AND Location not like '%OSCARS%' AND Location not like 'G:\%' AND M.Language!='Hindi')
ORDER BY Name


SELECT --TOP 1000
M.Name,M.Language,MI.IMDBID,
MI.Year,MII.Oscars,MI.IMDBRating,M.MyRating,M.Genre,Location
FROM Movies M 
INNER JOIN MovieInfo MI 
ON M.Name=MI.Name AND M.Language=MI.Language
LEFT JOIN MovieLocation ML
ON M.Name=ML.Name AND M.Language=ML.Language
INNER JOIN MovieInfoIMDB MII
ON MI.IMDBID = MII.IMDBID
WHERE (Location like '%OSCARS%') AND (Rating<8 OR Top250=0) --AND M.MyRating<8 AND M.MyRating!=0) -- AND Location not like 'G:\%' AND M.Language!='Hindi')
ORDER BY Name