namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostsCampoEsRss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "EsRssAtom", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "EsRssAtom");
        }
    }
}
