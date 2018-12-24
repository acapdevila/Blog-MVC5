using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Recetas;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoIngrediente : EntityTypeConfiguration<Ingrediente>
    {
        public MapeoIngrediente()
        {
            ToTable("Ingrediente");

            Property(m => m.Nombre)
                .HasMaxLength(512)
                 .HasColumnType("varchar");

         


        }
    }
}
