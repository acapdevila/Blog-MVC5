using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Recetas;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoInstruccion : EntityTypeConfiguration<Instruccion>
    {
        public MapeoInstruccion()
        {
            ToTable("Instruccion");

            Property(m => m.Nombre)
                .HasMaxLength(512)
                .HasColumnType("varchar");


        }
    }
}
