namespace Blog.ViewModels.Post
{
    public class EditorPostRelacionado
    {
        public EditorPostRelacionado()
        {

        }

        public EditorPostRelacionado(int posicion, Modelo.Posts.Post postRelacionado)
        {
            Posicion = posicion;
            Nombre = postRelacionado.Titulo;
        }
        public int Posicion { get; set; }
        public string Nombre { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}