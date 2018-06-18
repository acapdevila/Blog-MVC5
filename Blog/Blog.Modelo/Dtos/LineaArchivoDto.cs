using System;

namespace Blog.Modelo.Dtos
{
    public class LineaArchivoDto
    {
        public string UrlSlug { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPost { get; set; }

        public int Año => FechaPost.Year;
        public int Mes => FechaPost.Month;
    }
}
