using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Utensilios;
using Infra;

namespace LG.Web.Views.Utensilios.ViewModels
{
    public class UtensiliosViewModel
    {
        public UtensiliosViewModel()
        {
            Categorias = new List<CategoriaUtensilioViewModel>();
        }

        public UtensiliosViewModel(List<UtensilioCategoria> categorias):this()
        {
            foreach (var utensilioCategoria in categorias.OrderBy(m=>m.Posicion))
            {
                Categorias.Add(new CategoriaUtensilioViewModel(utensilioCategoria));
            }
        }

        public List<CategoriaUtensilioViewModel> Categorias { get; set; }
    }

    public class CategoriaUtensilioViewModel    
    {
        public CategoriaUtensilioViewModel()
        {
            Utensilios= new List<ItemUtensilioViewModel>();
        }

        public CategoriaUtensilioViewModel(UtensilioCategoria categoria) :this()
        {
            Nombre = categoria.Nombre;
            foreach (var utensilio in categoria.Utensilios.OrderBy(m=>m.Nombre))
            {
                Utensilios.Add(new ItemUtensilioViewModel(utensilio));
            }
        }

        public string Nombre { get; set; }
        public List<ItemUtensilioViewModel> Utensilios { get; set; }
    }

    public class ItemUtensilioViewModel
    {
        public ItemUtensilioViewModel()
        {
            
        }

        public ItemUtensilioViewModel(Utensilio utensilio)
        {
            Id = utensilio.Id;
            Nombre = utensilio.Nombre;
            Imagen = utensilio.Imagen;
            Link = utensilio.Link;
        }

        public int Id { get; set; }

        public string Nombre { get; private set; }

        public Imagen Imagen { get; set; }

        public string Link { get; private set; }
    }
}