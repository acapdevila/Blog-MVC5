﻿--insert into acapdevila.AspNetUsers
--select * from AspNetUsers

--SET IDENTITY_INSERT acapdevila.Post ON;
--insert into acapdevila.Post ([Id]
--      ,[Subtitulo]
--      ,[Titulo]
--      ,[UrlSlug]
--      ,[FechaPost]
--      ,[ContenidoHtml]
--      ,[EsBorrador]
--      ,[FechaPublicacion]
--      ,[Autor]
--      ,[EsRssAtom]
--      ,[PostContenidoHtml]
--      ,[Descripcion]
--      ,[PalabrasClave]
--      ,[UrlImagenPrincipal]
--      ,[FechaModificacion]
--      ,[TituloSinAcentos])
--SELECT [Id]
--      ,[Subtitulo]
--      ,[Titulo]
--      ,[UrlSlug]
--      ,[FechaPost]
--      ,[ContenidoHtml]
--      ,[EsBorrador]
--      ,[FechaPublicacion]
--      ,[Autor]
--      ,[EsRssAtom]
--      ,[PostContenidoHtml]
--      ,[Descripcion]
--      ,[PalabrasClave]
--      ,[UrlImagenPrincipal]
--      ,[FechaModificacion]
--      ,[TituloSinAcentos]
--  FROM [dbo].[Post]
--  Where BlogId = 1
   --SET IDENTITY_INSERT acapdevila.Post OFF;
 
 --SET IDENTITY_INSERT acapdevila.Tag ON;
 --insert into acapdevila.Tag ([Id]
 --     ,[Nombre]
 --     ,[UrlSlug]
 --     ,[Descripcion]
 --     ,[PalabrasClave]
 --     ,[UrlImagenPrincipal]
 --     ,[ContenidoHtml]
 --     ,[FechaPublicacion]
 --     ,[EsPublico]
 --     ,[NombreSinAcentos])
 --SELECT distinct Tag.[Id]
 --     ,Tag.[Nombre]
 --     ,Tag.[UrlSlug]
 --     ,Tag.[Descripcion]
 --     ,Tag.[PalabrasClave]
 --     ,Tag.[UrlImagenPrincipal]
 --     ,Tag.[ContenidoHtml]
 --     ,Tag.[FechaPublicacion]
 --     ,Tag.[EsPublico]
 --     ,Tag.[NombreSinAcentos]
 -- FROM [dbo].[Tag]
 -- inner join TagPost on Tag.id = TagPost.IdTag
 -- inner join Post on TagPost.IdPost = Post.Id
 -- where Post.BlogId = 1
 --SET IDENTITY_INSERT acapdevila.Tag OFF;

 insert into acapdevila.TagPost (IdPost, IdTag)
 select TagPost.IdPost, TagPost.IdTag from TagPost
 inner join Post on TagPost.IdPost = Post.Id
 where Post.BlogId = 1