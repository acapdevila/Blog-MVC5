using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Blog.Modelo
{
    public class Post
    {
        public static Post CrearNuevoPorDefecto()
        {
            return new Post 
            {
                Autor = "Albert Capdevila",
                EsBorrador = true,
                FechaPost = DateTime.Now,
                FechaPublicacion = DateTime.Now.AddDays(5)
            };
        }

        public Post()
        {
            Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public string Subtitulo { get; set; }
        public string Titulo { get; set; }
        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public string ContenidoHtml { get; set; }
        public bool EsBorrador { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Autor { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public string TagsSeparadosPorComma => Tags.Any() ? string.Join(",", Tags.Select(m=>m.Nombre)) : string.Empty;

        public void EstablecerTags(List<string> listaTags)
        {
            var tagsParaEliminar = Tags.Where(m => !listaTags.Contains(m.Nombre)).ToList();
            var tagsParaAñadir = listaTags.Except(Tags.Select(t => t.Nombre));

            while (tagsParaEliminar.Any())
            {
                var tag = tagsParaEliminar.First();
                Tags.Remove(tag);
                tagsParaEliminar.Remove(tag);
            }

            foreach (var tag in tagsParaAñadir)
            {
                Tags.Add(new Tag
                {
                    Nombre = tag,
                    UrlSlug = GenerateSlug(tag)
                });
            }
        }

        //http://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
        private static string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private static string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
