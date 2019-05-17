using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Categorias;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;

namespace Blog.Smoothies.Views.Blog.ViewModels
{
    public class DisplayPostViewModel
    {
        public DisplayPostViewModel()
        {
            
        }

        public DisplayPostViewModel(Post post)
        {
            Id = post.Id;
            Subtitulo = post.Subtitulo;
            Titulo = post.Titulo;
            Descripcion = post.Descripcion;
            PalabrasClave = post.PalabrasClave;
            UrlImagenPrincipal = post.UrlImagenPrincipal;
            PalabrasClave = post.PalabrasClave;
            UrlSlug = post.UrlSlug;
            FechaPost = post.FechaPost;
            ContenidoHtml = post.ContenidoHtml;
            PostContenidoHtml = post.PostContenidoHtml;
            Autor = post.Autor;
            Tags = post.Tags;
            Categorias = post.Categorias;
            Utensilios = post.Utensilios.Select(m => new PostUtensilioViewModel(m)).ToList();
        }

        public int Id { get; set; }

        public string Subtitulo { get; set; }
        public string Titulo { get; private set; }

        public string Descripcion { get; set; }

        public string PalabrasClave { get; set; }

        public string UrlImagenPrincipal { get; set; }

        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public string ContenidoHtml { get; set; }

        public string PostContenidoHtml { get; set; }

        public int? RecetaId { get; set; }

        public string Autor { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Categoria> Categorias { get; set; }

        public ICollection<PostUtensilioViewModel> Utensilios { get; set; }

        public bool EsReceta => RecetaId.HasValue;


    }
}