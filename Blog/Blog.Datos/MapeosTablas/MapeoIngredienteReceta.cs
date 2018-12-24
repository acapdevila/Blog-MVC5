using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Recetas;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoIngredienteReceta : EntityTypeConfiguration<IngredienteReceta>
    {
        public MapeoIngredienteReceta()
        {
            ToTable("IngredienteReceta");

            HasRequired(m => m.Receta);
                HasRequired(m=>m.Ingrediente);


        }
    }
}
