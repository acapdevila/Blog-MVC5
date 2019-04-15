using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Posts;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoPostsRelacionados : EntityTypeConfiguration<PostRelacionado>
    {
        public MapeoPostsRelacionados()
        {
            ToTable("PostRelacionados");

            HasKey(m => m.Id);

            HasRequired(m => m.Post)
                .WithMany(m=>m.PostRelacionados);

            HasRequired(m => m.Hijo);
        }
    }
}
