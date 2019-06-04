namespace Ac.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "acapdevila.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtitulo = c.String(maxLength: 8000, unicode: false),
                        Titulo = c.String(nullable: false, maxLength: 128, unicode: false),
                        Descripcion = c.String(maxLength: 512, unicode: false),
                        PalabrasClave = c.String(maxLength: 256, unicode: false),
                        UrlImagenPrincipal = c.String(maxLength: 512, unicode: false),
                        UrlSlug = c.String(nullable: false, maxLength: 64),
                        FechaPost = c.DateTime(nullable: false),
                        ContenidoHtml = c.String(),
                        PostContenidoHtml = c.String(),
                        EsBorrador = c.Boolean(nullable: false),
                        EsRssAtom = c.Boolean(nullable: false),
                        FechaPublicacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        Autor = c.String(maxLength: 64, unicode: false),
                        TituloSinAcentos = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UrlSlug, unique: true);
            
            CreateTable(
                "acapdevila.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 64, unicode: false),
                        UrlSlug = c.String(maxLength: 64, unicode: false),
                        Descripcion = c.String(),
                        PalabrasClave = c.String(),
                        UrlImagenPrincipal = c.String(),
                        ContenidoHtml = c.String(),
                        EsPublico = c.Boolean(nullable: false),
                        FechaPublicacion = c.DateTime(),
                        NombreSinAcentos = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "acapdevila.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "acapdevila.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("acapdevila.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("acapdevila.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "acapdevila.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "acapdevila.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("acapdevila.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "acapdevila.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("acapdevila.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "acapdevila.TagPost",
                c => new
                    {
                        IdPost = c.Int(nullable: false),
                        IdTag = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPost, t.IdTag })
                .ForeignKey("acapdevila.Post", t => t.IdPost, cascadeDelete: true)
                .ForeignKey("acapdevila.Tag", t => t.IdTag, cascadeDelete: true)
                .Index(t => t.IdPost)
                .Index(t => t.IdTag);
            
        }
        
        public override void Down()
        {
            DropForeignKey("acapdevila.AspNetUserRoles", "UserId", "acapdevila.AspNetUsers");
            DropForeignKey("acapdevila.AspNetUserLogins", "UserId", "acapdevila.AspNetUsers");
            DropForeignKey("acapdevila.AspNetUserClaims", "UserId", "acapdevila.AspNetUsers");
            DropForeignKey("acapdevila.AspNetUserRoles", "RoleId", "acapdevila.AspNetRoles");
            DropForeignKey("acapdevila.TagPost", "IdTag", "acapdevila.Tag");
            DropForeignKey("acapdevila.TagPost", "IdPost", "acapdevila.Post");
            DropIndex("acapdevila.TagPost", new[] { "IdTag" });
            DropIndex("acapdevila.TagPost", new[] { "IdPost" });
            DropIndex("acapdevila.AspNetUserLogins", new[] { "UserId" });
            DropIndex("acapdevila.AspNetUserClaims", new[] { "UserId" });
            DropIndex("acapdevila.AspNetUsers", "UserNameIndex");
            DropIndex("acapdevila.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("acapdevila.AspNetUserRoles", new[] { "UserId" });
            DropIndex("acapdevila.AspNetRoles", "RoleNameIndex");
            DropIndex("acapdevila.Post", new[] { "UrlSlug" });
            DropTable("acapdevila.TagPost");
            DropTable("acapdevila.AspNetUserLogins");
            DropTable("acapdevila.AspNetUserClaims");
            DropTable("acapdevila.AspNetUsers");
            DropTable("acapdevila.AspNetUserRoles");
            DropTable("acapdevila.AspNetRoles");
            DropTable("acapdevila.Tag");
            DropTable("acapdevila.Post");
        }
    }
}
