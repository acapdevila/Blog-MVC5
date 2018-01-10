using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Tags;
using CSharpFunctionalExtensions;

namespace Blog.Modelo.Posts
{
    public class CriteriosBusqueda : ValueObject<CriteriosBusqueda>
    {
        private List<Tag> _tags;
        private List<string> _palabras;

        public static CriteriosBusqueda Vacio()
        {
            return new CriteriosBusqueda(string.Empty);
        }

        public static Result<CriteriosBusqueda> Crear(string buscarPor)
        {
            if (string.IsNullOrEmpty(buscarPor))
                return Result.Ok(Vacio());

            if (150 < buscarPor.Length)
                buscarPor = buscarPor.Substring(0, 150);
            
            return Result.Ok(new CriteriosBusqueda(buscarPor));
        }

        public IReadOnlyCollection<Tag> Tags
        {
            get { return _tags; }
        }

        public IReadOnlyList<string> PalabrasBuscadas
        {
            get { return _palabras; }
        }

        public string BuscarPor { get;  }
        
        private CriteriosBusqueda(string buscarPor)
        {
            BuscarPor = buscarPor.Trim().Trim(',');
            _palabras = BuscarPor.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
            _tags = new List<Tag>();
        }


        protected override bool EqualsCore(CriteriosBusqueda other)
        {
            return BuscarPor.Equals(other.BuscarPor, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return BuscarPor.GetHashCode()*312;
        }

        public static implicit operator string(CriteriosBusqueda criteriosBusqueda)
        {
            return criteriosBusqueda.BuscarPor;
        }

        public static explicit operator CriteriosBusqueda(string buscarPor)
        {
            return Crear(buscarPor).Value;
        }

        public void AñadirTags(List<Tag> tags)
        {
            foreach (var tag in tags)
            {
                AñadirTag(tag);
            }
        }

        private void AñadirTag(Tag tag)
        {
            _tags.Add(tag);
            _palabras.Remove(tag.Nombre);
        }
    }
}