using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using LG.Web.Helpers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LG.Web.Servicios
{
    public class SubirArchivoImagenServicio
    {
        public string SubirImagen(WebImage imagen, int dimensionMaxima)
        {
            if (imagen == null || !imagen.FileName.TerminaConUnaExtensionDeImagenValida())
            {
                return string.Empty;
            }

            WebImage imagenRedimensionada = RedimensionarImagen(imagen, dimensionMaxima);

            string nombreUnico = GenerarUnNombreUnico(Path.GetFileName(imagen.FileName));

            GuardarImagenEnAzure(imagenRedimensionada, nombreUnico);

            return nombreUnico;
        }

        
        private WebImage RedimensionarImagen(WebImage imagen, int dimensionMaxima)
        {
            var isWide = imagen.Width > imagen.Height;
            var bigestDimension = isWide ? imagen.Width : imagen.Height;

            if (bigestDimension > dimensionMaxima)
            {
                if (isWide)
                    imagen.Resize(dimensionMaxima, ((dimensionMaxima * imagen.Height) / imagen.Width));
                else
                    imagen.Resize(((dimensionMaxima * imagen.Width) / imagen.Height), dimensionMaxima);
            }

            return imagen;
        }

        private string GenerarUnNombreUnico(string filename)
        {
            Match matchGuid = Regex.Match(filename, @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12}[_])");

            if (matchGuid.Success)
            {
                filename = filename.Replace(matchGuid.Value, string.Empty);
            }

            return $"{Guid.NewGuid()}_{filename}".ToLower();
        }


        private void GuardarImagenEnAzure(WebImage imagen, string nombreArchivo)
        {
            CloudBlobContainer storageContainer = AzureStorageService.ObtenerBlobContainer();
            storageContainer.SubirImagen(nombreArchivo, imagen);
        }

        private void GuardarImagenEnServidor(WebImage imagen, string rutaDirectorio,string nombreArchivo)
        {
             imagen.Save(Path.Combine("~" + rutaDirectorio, nombreArchivo));
             imagen = null;
        }
        

      
    }
}
