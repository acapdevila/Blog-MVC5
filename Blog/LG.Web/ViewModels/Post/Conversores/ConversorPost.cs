using System;
using Blog.Modelo.Categorias;
using Blog.Modelo.Tags;
using Omu.ValueInjecter;

namespace LG.Web.ViewModels.Post.Conversores
{
    public static class ConversorPost
    {
        public static void ActualizaPost(this global::Blog.Modelo.Posts.Post post, 
            EditorPost editorPost, 
            AsignadorTags asignadorTags,
            AsignadorCategorias asignadorCategorias)
        {
            post.ModificarTitulo(editorPost.Titulo);
            post.Subtitulo = editorPost.Subtitulo;
            post.Descripcion = editorPost.Descripcion;
            post.Autor = editorPost.Autor;
            post.ContenidoHtml = editorPost.ContenidoHtml;
            post.PalabrasClave = editorPost.PalabrasClave;
            post.UrlImagenPrincipal = editorPost.UrlImagenPrincipal;
            
            asignadorTags.AsignarTags(post, editorPost.ListaTags);
            asignadorCategorias.AsignarCategorias(post, editorPost.ListaCategorias);
            post.FechaModificacion = DateTime.Now;

        }



        public static void ActualizaBorrador(this global::Blog.Modelo.Posts.Post post,
            EditorBorrador editorBorrador,
            AsignadorTags asignadorTags,
            AsignadorCategorias asignadorCategorias)
        {
            post.InjectFrom(editorBorrador);

            post.ModificarTitulo(editorBorrador.Titulo);

            asignadorTags.AsignarTags(post, editorBorrador.ListaTags);
            asignadorCategorias.AsignarCategorias(post, editorBorrador.ListaCategorias);
            post.FechaModificacion = DateTime.Now;
        }

    }
}
