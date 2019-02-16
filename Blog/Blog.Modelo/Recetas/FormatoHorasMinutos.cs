using System;
namespace Blog.Modelo.Recetas
{
   public  static class ExtensionesTimeSpan 
    {
        public static string FormatoHorasMinutos(this TimeSpan tiempo)
        {
            if (tiempo.Minutes == 0 && tiempo.Hours == 0 && tiempo.Days == 0)
                return "-";

            if (tiempo.Minutes == 0)
                return $"{tiempo.Hours + tiempo.Days * 24} h";

            if (tiempo.Hours == 0 &&  tiempo.Days == 0)
                return $"{tiempo.Minutes} '";

            return $"{tiempo.Hours + tiempo.Days * 24} h {tiempo.Minutes} '";
         }
}
}
