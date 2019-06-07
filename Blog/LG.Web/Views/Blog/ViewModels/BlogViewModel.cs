using Blog.Modelo.Posts;
using PagedList;

namespace LG.Web.Views.Blog.ViewModels
{
    public class BlogViewModel
    {

        public IPagedList<LineaResumenPost> Posts { get; set; }

    }
}