using System.Linq;
using System.Text.RegularExpressions;
using Blog.Modelo;
using Omu.ValueInjecter;

namespace Blog.Web.ViewModels.Post.Conversores
{
    public static class ConversorPost
    {
        public static void CopiaValores(this EditorPost editorPost, Modelo.Post post)
        {
            editorPost.InjectFrom(post);

            editorPost.Tags = post.TagsSeparadosPorComma;
        }

        public static void CopiaValores(this Modelo.Post post, EditorPost editorPost)
        {
            post.InjectFrom(editorPost);
            post.EstablecerTags(editorPost.ListaTags);
        }
    }
}
