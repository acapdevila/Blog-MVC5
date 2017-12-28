using System.Collections.Generic;
using Blog.Modelo.Dtos;

namespace Blog.ViewModels.Menu
{
    public class MenuCategoriasViewModel
    {
        public MenuCategoriasViewModel(List<Modelo.Categorias.Categoria> categorias)
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
