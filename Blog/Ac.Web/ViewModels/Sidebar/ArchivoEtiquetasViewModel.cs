﻿using System;
using System.Collections.Generic;
using Ac.Dominio.Dtos;

namespace Ac.Web.ViewModels.Sidebar
{
    public class ArchivoEtiquetasViewModel
    {
        public List<ArchivoItemViewModel> ListaArchivo { get; }
        
        public ArchivoEtiquetasViewModel(List<ArchivoItemViewModel> listaArchivo)
        {
            ListaArchivo = listaArchivo;
        }
    }

    public class ArchivoItemViewModel
    {
        public ArchivoItemViewModel(ArchivoItemDto itemDto)
        {
            Anyo = itemDto.Anyo;
            Mes = itemDto.Mes;
            NumPosts = itemDto.NumPosts;
        }

        public ArchivoItemViewModel(LineaArchivoDto itemDto)
        {
            Anyo = itemDto.Año;
            Mes = itemDto.Mes;
            //NumPosts = itemDto.;
        }

        public int Anyo { get; set; }

        public int Mes { get; set; }

        public string NombreMes {
            get
            {
                return new DateTime(Anyo, Mes, 1).ToString("MMMM");
            }
        }
        

        public int NumPosts { get; set; }
    }
}
