namespace Blog.Servicios.Utensilios.Comandos
{
    public class ComandoCrearUtensilio
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ImagenUrl { get; set; }
        public string ImagenAlt { get; set; }

        public string Link { get; set; }

        public string Categoria { get; set; }
    }
}