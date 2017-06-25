namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewingEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Viewings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        ViewAt = c.DateTime(nullable: false),
                        PropertyId = c.Int(nullable: false),
                        BuyerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PropertyId)
                .Index(t => t.BuyerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Viewings", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.Viewings", "BuyerId", "dbo.AspNetUsers");
            DropIndex("dbo.Viewings", new[] { "BuyerId" });
            DropIndex("dbo.Viewings", new[] { "PropertyId" });
            DropTable("dbo.Viewings");
        }
    }
}
