namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionarUtensiliosAPosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostUtensilios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UtensilioId = c.Int(nullable: false),
                        Posicion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Utensilio", t => t.UtensilioId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.UtensilioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostUtensilios", "UtensilioId", "dbo.Utensilio");
            DropForeignKey("dbo.PostUtensilios", "PostId", "dbo.Post");
            DropIndex("dbo.PostUtensilios", new[] { "UtensilioId" });
            DropIndex("dbo.PostUtensilios", new[] { "PostId" });
            DropTable("dbo.PostUtensilios");
        }
    }
}
