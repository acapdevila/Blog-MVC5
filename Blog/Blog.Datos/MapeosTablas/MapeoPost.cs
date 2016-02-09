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
        }
    }
}
