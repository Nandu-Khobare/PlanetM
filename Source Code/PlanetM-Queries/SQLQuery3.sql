USE PLANETM_DB

SELECT --Top 1000 
IMDBID,
Title,Language,Year,Rating,MyRating,
Genre,MPAARating,ReleaseDate,Runtime,
--Tagline,convert(varchar(25),Plot),convert(varchar(50),Storyline),PosterURL,
Awards,Nominations,Top250,Oscars
FROM MovieInfoIMDB MII
WHERE iswishlist=0 and  exists(select null from movieinfo where imdbid=MII.imdbid)
ORDER BY Title

--update 	MovieInfoIMDB set myrating=0 WHERE imdbid='tt0079945'

SELECT --TOP 1000
M.Name,M.Language,MI.IMDBID,
MI.Year,M.IsWatched Seen,SeenDate,
MI.Size,MI.IMDBRating,M.MyRating,M.Genre
FROM Movies M INNER JOIN MovieInfo MI 
ON M.Name=MI.Name AND M.Language=MI.Language
where m.name like '%trek%'

--update Movies set iswatched=0,myrating=0 where name like '%motion%'
--update Movieinfo set imdbid=null where name like '%motion%'