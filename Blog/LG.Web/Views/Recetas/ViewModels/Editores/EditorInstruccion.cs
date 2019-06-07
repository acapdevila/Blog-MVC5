using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels.Editores
{
    public class EditorInstruccion
    {
        public EditorInstruccion()
        {
            
        }

        public EditorInstruccion(int posicion, Instruccion instruccion)
        {
            Posicion = posicion;
            Nombre = instruccion.Nombre;
        }
        public int Posicion { get; set; }
        public string Nombre { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}