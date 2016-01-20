using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;

namespace Blog.Web.Servicios
{
    public class SubirArchivoImagenServicio
    {
        public string ObtenerImagenDePeticionYSubirImagen(int dimensionMaxima)
        {
            var imagen = ObtenerImagenDePeticion();

            if (imagen != null && EsUnArchivoImagen(imagen.FileName))
            {
                var nombreArchivoSubido = SubirImagen(imagen, dimensionMaxima, UrlImagenHelper.DirectorioImagenes);
                return nombreArchivoSubido;
            }
            return string.Empty;
        }

        private string SubirImagen(WebImage imagen, int dimensionMaxima, string rutaDirectorio)
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
            string nombreArchivo = GenerarUnNombreUnico(Path.GetFileName(imagen.FileName));

            GuardarImagen(imagen, rutaDirectorio, nombreArchivo);
            // GuardarImagenEnServidor();

            return nombreArchivo;
        }

        private void GuardarImagen(WebImage imagen, string rutaDirectorio, string nombreArchivo)
        {
            GuardarImagenEnAzure(imagen, rutaDirectorio, nombreArchivo);
        }

        private void GuardarImagenEnAzure(WebImage imagen, string rutaDirectorio, string nombreArchivo)
        {
            var storageContainer = AzureStorageService.GetBloobContainer();
            var blobName = AzureStorageService.GetBlobName(rutaDirectorio, nombreArchivo);
            storageContainer.UploadImage(blobName, imagen);
        }

        private void GuardarImagenEnServidor(WebImage imagen, string rutaDirectorio,string nombreArchivo)
        {
             imagen.Save(Path.Combine("~" + rutaDirectorio, nombreArchivo));
             imagen = null;
        }

        private WebImage ObtenerImagenDePeticion()
        {
            var request = HttpContext.Current.Request;

            if (request.Files.Count == 0)
            {
                return null;
            }

            try
            {
                var postedFile = request.Files[0];
                var image = new WebImage(postedFile.InputStream)
                {
                    FileName = postedFile.FileName
                };
                return image;
            }
            catch
            {
                // The user uploaded a file that wasn't an image or an image format that we don't understand
                return null;
            }
        }

        private bool EsUnArchivoImagen(string file)
        {
            return file.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
                file.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase) ||
                file.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) ||
                file.EndsWith(".JPG", StringComparison.CurrentCultureIgnoreCase) ||
                file.EndsWith(".GIF", StringComparison.CurrentCultureIgnoreCase) ||
                file.EndsWith(".PNG", StringComparison.CurrentCultureIgnoreCase);
        }

        private string GenerarUnNombreUnico(string filename)
        {
            var matchGuid = Regex.Match(filename, @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12}[_])");

            if (matchGuid.Success)
            {
                filename = filename.Replace(matchGuid.Value, string.Empty);
            }

            return $"{Guid.NewGuid()}_{filename}".ToLower();
        }
    }
}
