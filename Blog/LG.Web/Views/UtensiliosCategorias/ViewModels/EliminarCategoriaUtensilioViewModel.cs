using Blog.Modelo.Utensilios;

namespace Blog.Smoothies.Views.UtensiliosCategorias.ViewModels
{
    public  class EliminarCategoriaUtensilioViewModel
    {
        public EliminarCategoriaUtensilioViewModel()
        {
            
        }

        public EliminarCategoriaUtensilioViewModel(UtensilioCategoria categoria) : this()
        {
            Id = categoria.Id;
            Nombre = categoria.Nombre;
            Url = categoria.UrlSlug;
        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Url { get; set; }

    }
}
