using System;
using System.Web;
using System.Web.Mvc;
using Blog.Web.Servicios;

namespace Blog.Web.Controllers
{

    [Authorize]
    public class ImagenesController : Controller
    {
        private readonly SubirArchivoImagenServicio _imagenServicio;
      
        public ImagenesController()
            : this(new SubirArchivoImagenServicio())
        {
        }

        public ImagenesController(SubirArchivoImagenServicio imagenServicio)
        {
            _imagenServicio = imagenServicio;
        }

        
        [HttpPost]
        public ActionResult SubirImagen(HttpPostedFileBase upload, string ckEditorFuncNum, string ckEditor, string langCode)
        {
            if (Request.Url == null) return null;

            string filename = _imagenServicio.ObtenerImagenDePeticionYSubirImagen(1000);

            string url; // url to return
            string message; // message to display (optional)

            if (!String.IsNullOrEmpty(filename))
            {
                //url = Request.Url.GetLeftPart(UriPartial.Authority) + _imagenServicio.DirectorioImagenes + "/" + filename;
                url = (filename).GenerarUrlImagen();

                // es
                message = "La imagen se ha guardado correctamente.";
                
                // since it is an ajax request it requires this string
                string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum +
                                ", \"" + url + "\", \"" + message + "\");</script></body></html>";
                return Content(output);
            }
            //es
            message = "Error: No se ha guardado la imagen.";
            

            url = Request.Url.GetLeftPart(UriPartial.Authority);

            var fail = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum + ", \"" +
                       url + "\", \"" + message + "\");</script></body></html>";

            return Content(fail);

        }
    }



}

