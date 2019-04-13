namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostsRelacionados : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "Post_Id", c => c.Int());
            CreateIndex("dbo.Post", "Post_Id");
            AddForeignKey("dbo.Post", "Post_Id", "dbo.Post", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "Post_Id", "dbo.Post");
            DropIndex("dbo.Post", new[] { "Post_Id" });
            DropColumn("dbo.Post", "Post_Id");
        }
    }
}
