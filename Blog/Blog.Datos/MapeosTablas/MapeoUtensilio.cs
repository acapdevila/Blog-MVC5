using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Utensilios;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoUtensilio : EntityTypeConfiguration<Utensilio>
    {
        public MapeoUtensilio()
        {
            ToTable("Utensilio");
            
            Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("varchar")
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_NombreUtensilio", 1) { IsUnique = true }));

            Property(m => m.Link)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(256);

            HasRequired(m => m.Categoria)
                .WithMany(m=>m.Utensilios);
        }
    }
}
