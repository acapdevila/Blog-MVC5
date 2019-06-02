using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Ac.Modelo;

namespace Ac.Datos.MapeosTablas
{
    public  class MapeoPost : EntityTypeConfiguration<Post>
    {
        public MapeoPost()
        {
            ToTable("Post");

            HasKey(m => m.Id);
            
            Property(m => m.UrlSlug)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_UrlSlug", 2) { IsUnique = true }));



            Property(m => m.Titulo)
                .IsRequired()
             .HasMaxLength(128)
             .HasColumnType("varchar");

            Property(m => m.Descripcion)
                .HasMaxLength(512)
                .HasColumnType("varchar");

            Property(m => m.PalabrasClave)
                .HasMaxLength(256)
                .HasColumnType("varchar");

            Property(m => m.UrlImagenPrincipal)
                .HasMaxLength(512)
                .HasColumnType("varchar");

            Property(m => m.Subtitulo)
                .HasColumnType("varchar");

            Property(m => m.Autor)
                  .HasMaxLength(64)
             .HasColumnType("varchar");

            Property(m => m.UrlSlug)
                .IsRequired()
               .HasMaxLength(64);

            
            HasMany(m => m.Tags)
               .WithMany(m => m.Posts)
               .Map(m =>
               {
                   m.ToTable("TagPost");
                   m.MapLeftKey("IdPost");
                   m.MapRightKey("IdTag");
               });

          
        }
    }
}
