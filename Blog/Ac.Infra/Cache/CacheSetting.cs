using System;

namespace Ac.Infra.Cache
{
    public static class CacheSetting
    {
        public static class SitemapNodes
        {
            public const string Key = "SitemapNodes";
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
        }


        public static class RutasPosts
        {
            public const string Key = "RutasPosts";
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
        }

        public static class RutasCategorias
        {
            public const string Key = "RutasCategorias";
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
        }

        public static class RutasEtiquetas
        {
            public const string Key = "RutasEtiquetas";
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
        }

        public static class PaginaPrincipal
        {
            public const string Posts = "PaginaPrincipalPosts";
            public const string Etiquetas = "PaginaPrincipalEtiquetas";
            public const string Categorias = "PaginaPrincipalCategorias";
            public const string Archivo = "PaginaPrincipalArchivo";

            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromHours(1);
        }
    }
}