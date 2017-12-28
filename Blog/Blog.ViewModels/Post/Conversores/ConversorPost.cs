using Blog.Modelo.Categorias;
using Blog.Modelo.Tags;
using Omu.ValueInjecter;

namespace Blog.ViewModels.Post.Conversores
{
    public static class ConversorPost
    {
        public static void CopiaValores(this EditorPost editorPost, Modelo.Posts.Post post)
        {
            editorPost.InjectFrom(post);
            editorPost.Tags = post.Tags.TagsSeparadosPorComma();
            editorPost.Categorias = post.Categorias.CategoriasSeparadasPorComma();
        }

        public static void CopiaValores(this Modelo.Posts.Post post, 
            EditorPost editorPost, 
            AsignadorTags asignadorTags,
            AsignadorCategorias asignadorCategorias)
        {
            post.InjectFrom(editorPost);
            asignadorTags.AsignarTags(post, editorPost.ListaTags);
            asignadorCategorias.AsignarCategorias(post, editorPost.ListaCategorias);

        }

    }
}
