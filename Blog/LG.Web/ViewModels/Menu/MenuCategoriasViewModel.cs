using System.Collections.Generic;
using Blog.Modelo.Dtos;

namespace LG.Web.ViewModels.Menu
{
    public class MenuCategoriasViewModel
    {
        public MenuCategoriasViewModel(List<global::Blog.Modelo.Categorias.Categoria> categorias)
        {
            Categorias = new List<CategoriaDto>();
            foreach (var categoria in categorias)
            {
                Categorias.Add(new CategoriaDto(categoria));
            }
        }

        public List<CategoriaDto> Categorias { get; }
    }
}
