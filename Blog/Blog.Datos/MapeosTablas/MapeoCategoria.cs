using System.Data.Entity.ModelConfiguration;
using Blog.Modelo;
using Blog.Modelo.Categorias;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoCategoria : EntityTypeConfiguration<Categoria>
    {
        public MapeoCategoria()
        {
            ToTable("Categoria");

            Property(m => m.Nombre)
                .HasMaxLength(64)
                .HasColumnType("varchar");


            Property(m => m.UrlSlug)
                .HasMaxLength(64)
                .HasColumnType("varchar");
            
        }
    }
}
