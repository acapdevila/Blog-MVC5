using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Modelo;

namespace Blog.Datos.MapeosTablas
{
    public  class MapeoTag : EntityTypeConfiguration<Tag>
    {
        public MapeoTag()
        {
            ToTable("Tag");

            Property(m => m.Nombre)
                .HasMaxLength(64)
                .HasColumnType("varchar");


            Property(m => m.UrlSlug)
                .HasMaxLength(64)
                .HasColumnType("varchar");
        }
    }
}
