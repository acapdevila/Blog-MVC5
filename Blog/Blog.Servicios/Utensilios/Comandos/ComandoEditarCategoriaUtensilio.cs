namespace Blog.Servicios.Utensilios.Comandos
{
    public class ComandoEditarCategoriaUtensilio    
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string UrlSlug { get; set; }

        public int Posicion { get; set; }
    }
}