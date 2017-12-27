using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Blog.Modelo.Posts
{
    public class CriteriosBusqueda : ValueObject<CriteriosBusqueda>
    {
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


        public IReadOnlyList<string> PalabrasBuscadas { get; }

        public string BuscarPor { get;  }
        
        private CriteriosBusqueda(string buscarPor)
        {
            BuscarPor = buscarPor;
            PalabrasBuscadas = buscarPor.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
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
    }
}