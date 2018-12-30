using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;

namespace Blog.Modelo.Categorias
{
    public class AsignadorCategorias
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private Post _post;

        public AsignadorCategorias(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }
        
        public void AsignarCategorias(Post entidad, List<string> listaCategorias)
        {
            _post = entidad;

            var categoriasPorEliminarDeEntidad = DetecatarCategoriasPorEliminarDeEntidad(listaCategorias);

            var categoriasPorAñadirAEntidad = DetectarNuevosCategoriasPorAñadirAEntidad(listaCategorias);

           EliminarCategorias(categoriasPorEliminarDeEntidad);

           AñadirCategorias(categoriasPorAñadirAEntidad);
        }
        
        private void AñadirCategorias(IEnumerable<string> categoriasPorAñadir)
        {
            foreach (var categoriaPorAñadir in categoriasPorAñadir)
            {
                var categoria = CrearORecuperarCategoria(categoriaPorAñadir);
                _post.Categorias.Add(categoria);
            }
        }

        private Categoria CrearORecuperarCategoria(string categoriaPorAñadir)
        {
            categoriaPorAñadir = categoriaPorAñadir.Trim();
            var categoria =  _categoriaRepositorio.RecuperarCategoriaPorNombre(categoriaPorAñadir);

            if (categoria == null)
            {
                categoria = new Categoria
                {
                    UrlSlug = GeneradorUrlSlug.GenerateSlug(categoriaPorAñadir)
                };
                categoria.CambiarNombre(categoriaPorAñadir);
            }

            return categoria;
        }

        private void EliminarCategorias(List<Categoria> categoriasPorEliminar)
        {
            while (categoriasPorEliminar.Any())
            {
                var categoria = categoriasPorEliminar.First();

                _post.Categorias.Remove(categoria);
                categoriasPorEliminar.Remove(categoria);
            }
        }

        private IEnumerable<string> DetectarNuevosCategoriasPorAñadirAEntidad(List<string> listaCategorias)
        {
            return listaCategorias.Except(_post.Categorias.Select(t => t.Nombre));
        }

        private List<Categoria> DetecatarCategoriasPorEliminarDeEntidad(List<string> listaCategorias)
        {
            return _post.Categorias.Where(m => !listaCategorias.Contains(m.Nombre)).ToList();
        }
    }
}
