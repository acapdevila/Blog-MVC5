using System.Data.Entity.ModelConfiguration;
using Blog.Modelo.Posts;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoPostsUtensilios : EntityTypeConfiguration<PostUtensilio>
    {
        public MapeoPostsUtensilios()
        {
            ToTable("PostUtensilios");

            HasKey(m => m.Id);

            HasRequired(m => m.Post)
                .WithMany(m=> m.Utensilios);

            HasRequired(m => m.Utensilio);
        }
    }
}
