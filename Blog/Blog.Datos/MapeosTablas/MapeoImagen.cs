using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Imagenes;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoImagen : EntityTypeConfiguration<Imagen>
    {
        public MapeoImagen()
        {
            ToTable("Imagen");

            Property(m => m.Alt)
                .HasMaxLength(256)
                .HasColumnType("varchar");


            Property(m => m.Url)
                .HasMaxLength(256)
                .HasColumnType("varchar");

            
        }
    }
}
