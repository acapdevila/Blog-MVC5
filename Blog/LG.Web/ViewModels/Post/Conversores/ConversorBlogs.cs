using Blog.Modelo.Posts;
using Omu.ValueInjecter;

namespace LG.Web.ViewModels.Post.Conversores
{
    public static class ConversorBlog
    {
        public static void CopiaValores(this EditorBlog editorBlog, BlogEntidad blog)
        {
            editorBlog.InjectFrom(blog);
          }

        public static void CopiaValores(this BlogEntidad blog, EditorBlog editorBlog)
        {
            blog.InjectFrom(editorBlog);
        }

    }
}
