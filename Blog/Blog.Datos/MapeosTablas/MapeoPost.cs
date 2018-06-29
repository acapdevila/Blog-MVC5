using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Blog.Modelo;
using Blog.Modelo.Posts;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoPost : EntityTypeConfiguration<Post>
    {
        public MapeoPost()
        {
            ToTable("Post");

            Property(m => m.BlogId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_UrlSlug", 1) { IsUnique = true })
                );

            Property(m => m.UrlSlug)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_UrlSlug", 2) { IsUnique = true }));



            Property(m => m.Titulo)
                .IsRequired()
             .HasMaxLength(128)
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

            HasMany(m => m.Categorias)
                .WithMany(m => m.Posts)
                .Map(m =>
                {
                    m.ToTable("CategoriaPost");
                    m.MapLeftKey("IdPost");
                    m.MapRightKey("IdCategoria");
                    
                });
        }
    }
}
