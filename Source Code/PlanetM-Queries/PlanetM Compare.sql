USE PLANETM_DB

SELECT --Top 1000 
IMDBID,
Title,Language,Year,Rating,MyRating,
Genre,MPAARating,ReleaseDate,Runtime,
--Tagline,convert(varchar(25),Plot),convert(varchar(50),Storyline),PosterURL,
Awards,Nominations,Top250,Oscars
FROM MovieInfoIMDB
--WHERE MyRating>0
ORDER BY Title
		

SELECT --TOP 1000
M.Name,M.Language,MI.IMDBID,
MI.Year,M.IsWatched Seen,
MI.Size,MI.IMDBRating,M.MyRating,M.Genre
FROM Movies M INNER JOIN MovieInfo MI 
ON M.Name=MI.Name AND M.Language=MI.Language
