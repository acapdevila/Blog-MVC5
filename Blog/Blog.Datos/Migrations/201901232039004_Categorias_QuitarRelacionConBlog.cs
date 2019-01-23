namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categorias_QuitarRelacionConBlog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categoria", "Blog_Id", "dbo.Blog");
            DropIndex("dbo.Categoria", new[] { "Blog_Id" });
            DropColumn("dbo.Categoria", "Blog_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categoria", "Blog_Id", c => c.Int());
            CreateIndex("dbo.Categoria", "Blog_Id");
            AddForeignKey("dbo.Categoria", "Blog_Id", "dbo.Blog", "Id");
        }
    }
}
