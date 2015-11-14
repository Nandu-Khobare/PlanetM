--Size mis-match
SELECT MI.Name,MI.language,MI.Size,ML.Size,ML.Location
FROM MovieInfo MI INNER JOIN MovieLocation ML ON ML.Name=MI.Name and ML.Language=MI.Language
WHERE MI.Size!=ML.Size


--Duplicates

SELECT   ML1.* FROM MovieLocation ML1
INNER JOIN MovieLocation ML2 ON ML1.Name=ML2.Name and ML1.Language=ML2.Language --and ML1.Size=ML2.Size
GROUP BY ML1.Name,ML1.Language,ML1.Location,ML1.Checksum,ML1.Size
HAVING  COUNT(*) > 1
ORDER BY ML1.Name,ML1.Language



SELECT   ML1.* FROM MovieLocation ML1
INNER JOIN MovieLocation ML2 ON ML1.Name=ML2.Name and ML1.Language=ML2.Language and ML1.Size=ML2.Size
GROUP BY ML1.Name,ML1.Language,ML1.Location,ML1.Checksum,ML1.Size
HAVING  COUNT(*) > 1
ORDER BY ML1.Name,ML1.Language


--Movies with diff prints
SELECT   ML1.* FROM MovieLocation ML1
INNER JOIN MovieLocation ML2 ON ML1.Name=ML2.Name and ML1.Language=ML2.Language --and ML1.Size=ML2.Size
where ML1.name not in (SELECT   ML1.name FROM MovieLocation ML1
INNER JOIN MovieLocation ML2 ON ML1.Name=ML2.Name and ML1.Language=ML2.Language and ML1.Size=ML2.Size
GROUP BY ML1.Name,ML1.Language,ML1.Location,ML1.Checksum,ML1.Size
HAVING  COUNT(*) > 1)
GROUP BY ML1.Name,ML1.Language,ML1.Location,ML1.Checksum,ML1.Size HAVING  COUNT(*) > 1
ORDER BY ML1.Name,ML1.Language


select * from movieinfoimdb where runtime = 0
select * from movieinfoimdb where Rating < 1 AND ReleaseDate < GetDate()
select year(releasedate),year,year(releasedate)-year ,*from movieinfoimdb where ABS(year(releasedate)-year) >1 order by ABS(year(releasedate)-year) desc
select Top250,count(*) Count from movieinfoimdb where Top250>0 group by Top250 having count(*) >1
select DataModificationDate,DATEDIFF(DAY,DataModificationDate,GetDate()),* from movieinfoimdb MII order by MII.DataModificationDate asc


select MII.IMDBID,Title,director from movieinfoimdb MII left outer join moviedirectors MD on MD.IMDBID=MII.IMDBID where director is null
select MII.IMDBID,Title,star from movieinfoimdb MII left outer join moviestars MD on MD.IMDBID=MII.IMDBID where star is null
select MII.IMDBID,Title,cast from movieinfoimdb MII left outer join moviecast MD on MD.IMDBID=MII.IMDBID where cast is null
select MII.IMDBID,Title,writer from movieinfoimdb MII left outer join moviewriters MD on MD.IMDBID=MII.IMDBID where writer is null

select MII.IMDBID,Title,count(*) from movieinfoimdb MII left outer join moviedirectors MD on MD.IMDBID=MII.IMDBID group by MII.IMDBID,Title having count(*)<10
select MII.IMDBID,Title,count(*) from movieinfoimdb MII left outer join moviestars MD on MD.IMDBID=MII.IMDBID group by MII.IMDBID,Title having count(*)<3
select MII.IMDBID,Title,count(*) from movieinfoimdb MII left outer join moviecast MD on MD.IMDBID=MII.IMDBID group by MII.IMDBID,Title having count(*)<10
select MII.IMDBID,Title,count(*) from movieinfoimdb MII left outer join moviewriters MD on MD.IMDBID=MII.IMDBID group by MII.IMDBID,Title having count(*)<2
