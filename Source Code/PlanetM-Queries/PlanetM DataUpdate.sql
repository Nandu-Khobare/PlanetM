select * from dbo.MovieInfoIMDB where language='hindi'title like '%arya%'

select * from dbo.MovieInfoIMDB where title = 'Hyoch Navra Pahije'

select * from dbo.MovieInfoIMDB a left outer join moviestars b on a.imdbid=b.imdbid 
where star = 'Dada Kondke'

select * from dbo.MovieInfoIMDB a left outer join moviewriters b on a.imdbid=b.imdbid 
where writer = 'Dada Kondke'

--update dbo.MovieInfoIMDB set genre='Drama,Romance,Musical' where title = 'Mann'

select * from movieinfo where name='Agneepath 2'
update movieinfo set imdbid='' where name='Agneepath 2'