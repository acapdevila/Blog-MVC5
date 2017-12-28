namespace Blog.Modelo.Categorias
{
    public interface ICategoriaRepositorio
    {
        Categoria RecuperarCategoriaPorNombre(string nombreCategoria);
    }
}
