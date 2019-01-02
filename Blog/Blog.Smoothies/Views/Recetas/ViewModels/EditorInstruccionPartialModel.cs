using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public class EditorInstruccionPartialModel
    {
        public EditorInstruccionPartialModel()
        {
            
        }

        public EditorInstruccionPartialModel(Instruccion instruccion)
        {
            Id = instruccion.Id;
            Nombre = instruccion.Nombre;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}