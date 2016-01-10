namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtitulo = c.String(maxLength: 128, unicode: false),
                        Titulo = c.String(nullable: false, maxLength: 64, unicode: false),
                        UrlSlug = c.String(nullable: false, maxLength: 64),
                        FechaPost = c.DateTime(nullable: false),
                        ContenidoHtml = c.String(),
                        EsBorrador = c.Boolean(nullable: false),
                        FechaPublicacion = c.DateTime(nullable: false),
                        Autor = c.String(maxLength: 64, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagPost",
                c => new
                    {
                        IdPost = c.Int(nullable: false),
                        IdTag = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPost, t.IdTag })
                .ForeignKey("dbo.Post", t => t.IdPost, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.IdTag, cascadeDelete: true)
                .Index(t => t.IdPost)
                .Index(t => t.IdTag);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPost", "IdTag", "dbo.Tag");
            DropForeignKey("dbo.TagPost", "IdPost", "dbo.Post");
            DropIndex("dbo.TagPost", new[] { "IdTag" });
            DropIndex("dbo.TagPost", new[] { "IdPost" });
            DropTable("dbo.TagPost");
            DropTable("dbo.Post");
        }
    }
}
