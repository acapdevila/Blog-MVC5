using Blog.Modelo.Posts;
using PagedList;

namespace Blog.Smoothies.Views.Blog.ViewModels
{
    public class BlogViewModel
    {

        public IPagedList<LineaResumenPost> Posts { get; set; }

    }
}