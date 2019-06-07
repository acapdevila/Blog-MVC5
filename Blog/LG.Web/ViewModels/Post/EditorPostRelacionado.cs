namespace LG.Web.ViewModels.Post
{
    public class EditorPostRelacionado
    {
        public EditorPostRelacionado()
        {

        }

        public EditorPostRelacionado(global::Blog.Modelo.Posts.PostRelacionado postRelacionado)
        {
            Posicion = postRelacionado.Posicion;
            Nombre = postRelacionado.Hijo.Titulo;
        }
        public int Posicion { get; set; }
        public string Nombre { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}