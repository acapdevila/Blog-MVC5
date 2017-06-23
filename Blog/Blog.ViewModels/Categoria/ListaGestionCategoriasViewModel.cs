using System.Collections.Generic;
using Blog.Modelo.Dtos;

namespace Blog.ViewModels.Categoria
{
    public class ListaGestionCategoriasViewModel
    {
        public ListaGestionCategoriasViewModel(List<LineaCategoriaDto> listaCategorias)
        {
            ListaCategorias = listaCategorias;
        }

        public List<LineaCategoriaDto> ListaCategorias { get; set; }
    }

 
}
