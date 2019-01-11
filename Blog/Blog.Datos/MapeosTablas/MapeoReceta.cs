using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Recetas;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoReceta : EntityTypeConfiguration<Receta>
    {
        public MapeoReceta()
        {
            ToTable("Receta");
            
            Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("varchar");

            Property(m => m.Autor)
                .HasMaxLength(64)
                .HasColumnType("varchar");

            Property(m => m.Descripcion)
                .HasColumnType("varchar");

            Property(m => m.Keywords)
                .HasMaxLength(256)
                .HasColumnType("varchar");

            Property(m => m.CategoriaReceta)
                .HasMaxLength(64)
                .HasColumnType("varchar");


            Property(m => m.Raciones)
                .HasMaxLength(128)
                .HasColumnType("varchar");


        }
    }
}
