namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionCategoriasQuitarBlogId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categoria", "BlogId", "dbo.Blog");
            DropIndex("dbo.Categoria", new[] { "BlogId" });
            RenameColumn(table: "dbo.Categoria", name: "BlogId", newName: "Blog_Id");
            AlterColumn("dbo.Categoria", "Blog_Id", c => c.Int());
            CreateIndex("dbo.Categoria", "Blog_Id");
            AddForeignKey("dbo.Categoria", "Blog_Id", "dbo.Blog", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categoria", "Blog_Id", "dbo.Blog");
            DropIndex("dbo.Categoria", new[] { "Blog_Id" });
            AlterColumn("dbo.Categoria", "Blog_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Categoria", name: "Blog_Id", newName: "BlogId");
            CreateIndex("dbo.Categoria", "BlogId");
            AddForeignKey("dbo.Categoria", "BlogId", "dbo.Blog", "Id", cascadeDelete: true);
        }
    }
}
