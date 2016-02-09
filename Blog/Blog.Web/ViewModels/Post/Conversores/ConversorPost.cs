using System.Linq;
using System.Text.RegularExpressions;
using Blog.Modelo;
using Blog.Modelo.Tags;
using Omu.ValueInjecter;

namespace Blog.Web.ViewModels.Post.Conversores
{
    public static class ConversorPost
    {
        public static void CopiaValores(this EditorPost editorPost, Modelo.Posts.Post post)
        {
            editorPost.InjectFrom(post);
            editorPost.Tags = post.Tags.TagsSeparadosPorComma();
        }

        public static void CopiaValores(this Modelo.Posts.Post post, EditorPost editorPost, AsignadorTags asignadorTags)
        {
            post.InjectFrom(editorPost);
            asignadorTags.AsignarTags(post, editorPost.ListaTags);
        }

    }
}
