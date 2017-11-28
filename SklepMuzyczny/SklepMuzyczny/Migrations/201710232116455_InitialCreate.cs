namespace SklepMuzyczny.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        NameCategory = c.String(),
                        Decription = c.String(),
                        NameFilePicture = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Song",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        NameSong = c.String(),
                        Author = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Bestseller = c.Boolean(nullable: false),
                        NameFilePicture = c.String(),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Song", "CategoryId", "dbo.Category");
            DropIndex("dbo.Song", new[] { "CategoryId" });
            DropTable("dbo.Song");
            DropTable("dbo.Category");
        }
    }
}
