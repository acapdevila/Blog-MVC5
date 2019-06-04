using System;
using Ac.Modelo.Tags;
using Omu.ValueInjecter;

namespace Ac.ViewModels.Post.Conversores
{
    public static class ConversorPost
    {
        public static void ActualizaPost(this Ac.Modelo.Post post, 
            EditorPost editorPost, 
            AsignadorTags asignadorTags)
        {
            post.ModificarTitulo(editorPost.Titulo);
            post.Subtitulo = editorPost.Subtitulo;
            post.Descripcion = editorPost.Descripcion;
            post.Autor = editorPost.Autor;
            post.ContenidoHtml = editorPost.ContenidoHtml;
            post.PalabrasClave = editorPost.PalabrasClave;
            post.UrlImagenPrincipal = editorPost.UrlImagenPrincipal;
            
            asignadorTags.AsignarTags(post, editorPost.ListaTags);
            post.FechaModificacion = DateTime.Now;

        }



        public static void ActualizaBorrador(this Modelo.Post post,
            EditorBorrador editorBorrador,
            AsignadorTags asignadorTags)
        {
            post.InjectFrom(editorBorrador);

            post.ModificarTitulo(editorBorrador.Titulo);

            asignadorTags.AsignarTags(post, editorBorrador.ListaTags);
            post.FechaModificacion = DateTime.Now;
        }

    }
}
