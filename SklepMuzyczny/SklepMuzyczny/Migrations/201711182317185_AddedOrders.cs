namespace SklepMuzyczny.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false),
                        Zip = c.String(nullable: false),
                        AdressLine = c.String(nullable: false),
                        State = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Comment = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderPosition",
                c => new
                    {
                        OrderPositionId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        SongId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PricePurchase = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderPositionId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Song", t => t.SongId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.SongId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderPosition", "SongId", "dbo.Song");
            DropForeignKey("dbo.OrderPosition", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderPosition", new[] { "SongId" });
            DropIndex("dbo.OrderPosition", new[] { "OrderId" });
            DropTable("dbo.OrderPosition");
            DropTable("dbo.Order");
        }
    }
}
