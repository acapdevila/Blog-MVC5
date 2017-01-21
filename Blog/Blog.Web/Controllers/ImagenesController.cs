using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Blog.Servicios;
using Microsoft.Ajax.Utilities;

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
            if (upload == null)
                return Content("Selecciona una imagen");

            if (!upload.FileName.TerminaConUnaExtensionDeImagenValida())
                return Content("Selecciona una archivo jpg, gif o png");

            WebImage imagen = upload.ToWebImage();

            string filename = _imagenServicio.SubirImagen(imagen, dimensionMaxima: 1000);

            string respuestaCkEditor = CrearRespuestaParaCkEditor(filename, ckEditorFuncNum);

            return Content(respuestaCkEditor);
        }

        private string CrearRespuestaParaCkEditor(string filename, string ckEditorFuncNum)
        {
            if (!String.IsNullOrEmpty(filename))
            {
                 return CrearMensageErrorParaCkEditor(ckEditorFuncNum, "Error: No se ha guardado la imagen.");
            }

            var url = (filename).GenerarUrlImagen();

            return CrearRespuestaCorrectaParaCkEditor(ckEditorFuncNum, url);
        }

        private string CrearMensageErrorParaCkEditor(string ckEditorFuncNum, string message)
        {
            var url = Request.Url.GetLeftPart(UriPartial.Authority);
            return @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum + ", \"" +
                   url + "\", \"" + message + "\");</script></body></html>";
        }

        private string CrearRespuestaCorrectaParaCkEditor(string ckEditorFuncNum, string url)
        {
            // es
            // message = "La imagen se ha guardado correctamente.";

            // since it is an ajax request it requires this string
            //string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum +
            //                ", \"" + url + "\", \"" + message + "\");</script></body></html>";

            return @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum +
                         ", \"" + url + "\", function() { " +

            "var element, dialog = this.getDialog();" +
            "if (dialog.getName() == 'image')" +
            "{" +

                "element = dialog.getContentElement('advanced', 'txtGenClass');" +
                "if (element)" +
                    "element.setValue('img-responsive');" +

            "}" +
            "});</script></body></html>";
        }
    }



}

