using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels.Editores
{
    public class EditorInstruccion
    {
        public EditorInstruccion()
        {
            
        }

        public EditorInstruccion(Instruccion instruccion)
        {
            Id = instruccion.Id;
            Nombre = instruccion.Nombre;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}