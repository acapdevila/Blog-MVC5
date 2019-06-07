using System;
using System.Collections.Generic;
using System.Linq;
using  Infra;
using CSharpFunctionalExtensions;

namespace Ac.Modelo.Posts
{
    public class CriteriosBusqueda : ValueObject
    {
        private readonly List<string> _palabras;

        public static CriteriosBusqueda Vacio()
        {
            return new CriteriosBusqueda(string.Empty);
        }

        public static CriteriosBusqueda Crear(string buscarPor)
        {
            if (string.IsNullOrEmpty(buscarPor))
                return Vacio();

            if (150 < buscarPor.Length)
                buscarPor = buscarPor.Substring(0, 150);
            
            return new CriteriosBusqueda(buscarPor);
        }
        
        public IReadOnlyList<string> PalabrasBuscadas
        {
            get { return _palabras; }
        }

        public IReadOnlyList<string> PalabrasBuscadasSinAcento
        {
            get { return _palabras.Select(m=>m.RemoveDiacritics()).ToList(); }
        }

        public string BuscarPor { get;  }
        
        private CriteriosBusqueda(string buscarPor)
        {
            BuscarPor = buscarPor.Trim().Trim(',');
            _palabras = BuscarPor
                        .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                        .Select(m=>m.Trim())
                        .Distinct()
                        .ToList();
        }

        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new List<object> {BuscarPor};
        }

        public static implicit operator string(CriteriosBusqueda criteriosBusqueda)
        {
            return criteriosBusqueda.BuscarPor;
        }

        public static explicit operator CriteriosBusqueda(string buscarPor)
        {
            return Crear(buscarPor);
        }



      
    }
}