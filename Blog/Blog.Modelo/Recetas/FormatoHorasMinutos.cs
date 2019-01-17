using System;
namespace Blog.Modelo.Recetas
{
   public  static class ExtensionesTimeSpan 
    {
        public static string FormatoHorasMinutos(this TimeSpan tiempo)
        {
            if (0 < tiempo.Hours)
                return $"{tiempo.Hours} horas y {tiempo.Minutes} minutos";

            return $"{tiempo.Minutes} minutos";
        }
}
}
