CREATE TYPE MyRatingsOnImdb AS TABLE
(
IMDBID varchar(25), MyRating int
);

CREATE TYPE MoviesIMDB AS TABLE
(
ID int, IMDBID varchar(25), Title varchar(100), Rating float
);

select * from temp
truncate table temp

select M.name,mr.imdbid,M.iswatched,mi.seendate
FROM temp MR
left outer JOIN MovieInfo MI 
ON MI.IMDBID=MR.IMDBID
left outer join Movies M
ON M.Name = MI.Name AND M.Language = MI.Language


select * from tempmovies
truncate table tempmovies

select t.*,MII.Title,MII.Language,MII.Rating
FROM tempmovies t
left outer JOIN MovieInfoIMDB MII 
ON MII.IMDBID=t.IMDBID


--BEGIN TRAN
--MERGE INTO MovieInfoIMDB AS MII
--		USING tempmovies as M
--		ON MII.IMDBID = M.IMDBID
--WHEN NOT MATCHED BY TARGET THEN
--	INSERT(IMDBID,Title,Language,Rating,IMDBURL)
--	VALUES(IMDBID,Title,'English',Rating,'http://www.imdb.com/title/' + IMDBID + '/')
--WHEN MATCHED THEN
--	UPDATE SET Rating = M.Rating;
--COMMIT TRAN
--ROLLBACK TRAN