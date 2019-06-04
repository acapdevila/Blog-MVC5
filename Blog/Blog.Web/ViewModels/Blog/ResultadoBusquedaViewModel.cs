﻿using Ac.Modelo.Posts;
using PagedList;

namespace Ac.ViewModels.Blog
{
    public class ResultadoBusquedaViewModel
    {

        public IPagedList<LineaPostCompleto> ListaPosts { get; set; }
        public string BuscarPor { get; set; }
    }
}
