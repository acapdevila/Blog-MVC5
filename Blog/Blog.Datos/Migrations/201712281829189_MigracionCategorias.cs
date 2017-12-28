namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionCategorias : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Post", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Post", new[] { "CategoriaId" });
            CreateTable(
                "dbo.CategoriaPost",
                c => new
                    {
                        IdPost = c.Int(nullable: false),
                        IdCategoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPost, t.IdCategoria })
                .ForeignKey("dbo.Post", t => t.IdPost, cascadeDelete: false)
                .ForeignKey("dbo.Categoria", t => t.IdCategoria, cascadeDelete: false)
                .Index(t => t.IdPost)
                .Index(t => t.IdCategoria);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoriaPost", "IdCategoria", "dbo.Categoria");
            DropForeignKey("dbo.CategoriaPost", "IdPost", "dbo.Post");
            DropIndex("dbo.CategoriaPost", new[] { "IdCategoria" });
            DropIndex("dbo.CategoriaPost", new[] { "IdPost" });
            DropTable("dbo.CategoriaPost");
            CreateIndex("dbo.Post", "CategoriaId");
            AddForeignKey("dbo.Post", "CategoriaId", "dbo.Categoria", "Id");
        }
    }
}
