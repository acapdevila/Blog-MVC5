using System;
using Blog.Modelo.Categorias;
using Blog.Modelo.Tags;
using Omu.ValueInjecter;

namespace Blog.ViewModels.Post.Conversores
{
    public static class ConversorPost
    {
        public static void ActualizaBorrador(this EditorPost editorPost, Modelo.Posts.Post post)
        {
            editorPost.InjectFrom(post);
            editorPost.Tags = post.Tags.TagsSeparadosPorComma();
            editorPost.Categorias = post.Categorias.CategoriasSeparadasPorComma();
        }

        public static void ActualizaPost(this Modelo.Posts.Post post, 
            EditorPost editorPost, 
            AsignadorTags asignadorTags,
            AsignadorCategorias asignadorCategorias)
        {
            post.InjectFrom(editorPost);
            asignadorTags.AsignarTags(post, editorPost.ListaTags);
            asignadorCategorias.AsignarCategorias(post, editorPost.ListaCategorias);
            post.FechaModificacion = DateTime.Now;
        }



        public static void ActualizaBorrador(this Modelo.Posts.Post post,
            EditorBorrador editorBorrador,
            AsignadorTags asignadorTags,
            AsignadorCategorias asignadorCategorias)
        {
            post.InjectFrom(editorBorrador);
            asignadorTags.AsignarTags(post, editorBorrador.ListaTags);
            asignadorCategorias.AsignarCategorias(post, editorBorrador.ListaCategorias);
            post.FechaModificacion = DateTime.Now;
        }

    }
}
