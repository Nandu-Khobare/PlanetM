select * from movieinfo order by LastModificationDate desc
select * from movieinfoimdb order by LastModificationDate desc
select * from movielocation order by addeddate desc

select * from dbo.MovieCast where [order] >0
select * from dbo.MovieDirectors where [order] >0
select * from dbo.MovieStars where [order] >0
select * from dbo.MovieWriters where [order] >0

--update movieinfo set lastmodificationdate=addeddate
--update movieinfoimdb set addeddate=lastmodificationdate

select * from movielocation ml join movieinfo mi on mi.name=ml.name and mi.language=ml.language
--update  ml set ml.addeddate=mi.addeddate from movielocation ml join movieinfo mi on mi.name=ml.name and mi.language=ml.language


SELECT ROUTINE_NAME, ROUTINE_DEFINITION
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_DEFINITION LIKE '%%' and (ROUTINE_DEFINITION LIKE '%update%' or ROUTINE_DEFINITION LIKE '%inserta%' or ROUTINE_DEFINITION LIKE '%delete%')
AND ROUTINE_TYPE='PROCEDURE'
order by ROUTINE_NAME
--LastModificationDate=GetDate()



declare @location varchar(500)
declare @size float
Declare @sizeOld float
declare @folderName varchar(500)
set @location='F:\Doc\A Picture of Britain'
set @size= 2237.3515625

set @folderName= Substring(@location, Len(@location) - Charindex('\',Reverse(@location)) + 2, Len(@location))
select @sizeOld=Size from movielocation where location like '%\' + @folderName

Declare @movieName varchar(200)
SELECT @movieName = Name FROM MovieLocation WHERE Location like '%\'+@folderName  and ABS(@size-Size)<2 
SELECT ABS(@size-Size),(@size-Size),* FROM MovieLocation WHERE Location like '%\'+@folderName  and ABS(@size-Size)<2 
select @movieName,@sizeOld,@size
