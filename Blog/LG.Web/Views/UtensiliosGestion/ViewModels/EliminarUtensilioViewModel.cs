using Blog.Modelo.Utensilios;

namespace LG.Web.Views.UtensiliosGestion.ViewModels
{
    public  class EliminarUtensilioViewModel
    {
        public EliminarUtensilioViewModel()
        {
            
        }

        public EliminarUtensilioViewModel(Utensilio utensilio) : this()
        {
            Id = utensilio.Id;
            Nombre = utensilio.Nombre;
            Url = utensilio.Link;
        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Url { get; set; }

    }
}
