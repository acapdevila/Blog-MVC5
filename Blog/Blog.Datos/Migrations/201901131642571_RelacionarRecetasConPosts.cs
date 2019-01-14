namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionarRecetasConPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "Receta_Id", c => c.Int());
            CreateIndex("dbo.Post", "Receta_Id");
            AddForeignKey("dbo.Post", "Receta_Id", "dbo.Receta", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "Receta_Id", "dbo.Receta");
            DropIndex("dbo.Post", new[] { "Receta_Id" });
            DropColumn("dbo.Post", "Receta_Id");
        }
    }
}
