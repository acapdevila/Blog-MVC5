using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Utensilios;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoUtensilioCategoria : EntityTypeConfiguration<UtensilioCategoria>
    {
        public MapeoUtensilioCategoria()
        {
            ToTable("UtensilioCategorias");
            
            Property(m => m.UrlSlug)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_UrlSlug_DeCategoriaUtensilio", 1) { IsUnique = true }));


            Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("varchar")
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_NombreCategoriaUtensilio", 2) { IsUnique = true }));

            
            Property(m => m.UrlSlug)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("varchar");


        }
    }
}
