namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostsRelacionadosComoEntidad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Post", "Post_Id", "dbo.Post");
            DropIndex("dbo.Post", new[] { "Post_Id" });
            // RenameColumn(table: "dbo.PostRelacionados", name: "Post_Id", newName: "PostId");
            CreateTable(
                "dbo.PostRelacionados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        HijoId = c.Int(nullable: false),
                        Posicion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.HijoId, cascadeDelete: false)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: false)
                .Index(t => t.PostId)
                .Index(t => t.HijoId);
            
            DropColumn("dbo.Post", "Post_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "Post_Id", c => c.Int());
            DropForeignKey("dbo.PostRelacionados", "PostId", "dbo.Post");
            DropForeignKey("dbo.PostRelacionados", "HijoId", "dbo.Post");
            DropIndex("dbo.PostRelacionados", new[] { "HijoId" });
            DropIndex("dbo.PostRelacionados", new[] { "PostId" });
            DropTable("dbo.PostRelacionados");
            RenameColumn(table: "dbo.PostRelacionados", name: "PostId", newName: "Post_Id");
            CreateIndex("dbo.Post", "Post_Id");
            AddForeignKey("dbo.Post", "Post_Id", "dbo.Post", "Id");
        }
    }
}
