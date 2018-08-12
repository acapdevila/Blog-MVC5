using System;

namespace Blog.Servicios.Cache
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
            public const string Key = "PaginaPrincipal";
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromHours(1);
        }
    }
}