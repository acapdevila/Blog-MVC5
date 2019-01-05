namespace Blog.Servicios.Recetas.Comandos.Imagenes
{
    public class ComandoAsignarImagen
    {
        public ComandoAsignarImagen(string url, string alt)
        {
            Url = url;
            Alt = alt;
        }
        public string Url { get;  }
        public string Alt { get; }
    }
}
