''
--DBCC SHRINKDATABASE ('D:\STUDY\MYDBS\PLANETM_DB.MDF')

--DBCC SHRINKDATABASE ('PLANETM_DB')
--DBCC SHRINKDATABASE ('MDDEGIGT')

--DBCC LOG ('PLANETM_DB',0)
--DBCC LOG ('PLANETM_DB',1)
DBCC LOG ('PLANETM_DB',2)
--DBCC LOG ('PLANETM_DB',3)
--DBCC LOG ('PLANETM_DB',4)

-- Sync MovieInfo and MovieInfoIMDB for Ratings
--		UPDATE MI
--			SET MI.IMDBRating = MII.Rating
--		FROM MovieInfoIMDB AS MII
--		INNER JOIN MovieInfo MI 
--			ON MII.IMDBID = MI.IMDBID
--		WHERE MII.Rating != MI.IMDBRating


Name 200-83 Language 50-16 Genre 100-50
select len(Name),name,Language from movies order by LEN(Name) desc
select len(Language),name,Language from movies order by LEN(Language) desc
select len(Genre),name,Language,Genre from movies order by LEN(Genre) desc

Location 500-122 Checksum 50-25
select len(location),name,location from movielocation order by LEN(location) desc
select len(checksum),name,checksum from movielocation order by LEN(checksum) desc

Title 200-83 Genre 100-50 MPAARating 25-7 Tagline 500-400 Plot 500-368 Storyline 2000-997 PosterURL 200-100
select len(Title),imdbid,title from movieinfoimdb order by LEN(Title) desc
select len(Genre),imdbid,title,Genre from movieinfoimdb order by LEN(Genre) desc
select len(mpaarating),imdbid,title,mpaarating from movieinfoimdb order by LEN(mpaarating) desc

select len(convert(varchar(max),tagline)),imdbid,title,tagline from movieinfoimdb order by LEN(convert(varchar(max),tagline)) desc
select len(convert(varchar(max),plot)),imdbid,title,plot from movieinfoimdb order by LEN(convert(varchar(max),plot)) desc
select len(convert(varchar(max),storyline)),imdbid,title,storyline from movieinfoimdb order by LEN(convert(varchar(max),storyline)) desc
select len(convert(varchar(max),posterurl)),imdbid,title,posterurl from movieinfoimdb order by LEN(convert(varchar(max),posterurl)) desc

Cast 50-33 Star 50-27 Director 50-32 Writer 50-32
select len(mc.cast),mii.imdbid,title,mc.cast from movieinfoimdb mii join moviecast mc on mii.imdbid=mc.imdbid order by LEN(mc.cast) desc
select len(ms.star),mii.imdbid,title,ms.star from movieinfoimdb mii join moviestars ms on mii.imdbid=ms.imdbid order by LEN(ms.star) desc
select len(md.director),mii.imdbid,title,md.director from movieinfoimdb mii join moviedirectors md on mii.imdbid=md.imdbid order by LEN(md.director) desc
select len(mw.writer),mii.imdbid,title,mw.writer from movieinfoimdb mii join moviewriters mw on mii.imdbid=mw.imdbid order by LEN(mw.writer) desc