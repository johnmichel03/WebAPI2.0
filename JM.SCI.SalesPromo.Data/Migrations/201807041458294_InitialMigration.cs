namespace JM.SCI.SalesPromo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        CampaignId = c.Int(nullable: false, identity: true),
                        CampaignName = c.String(nullable: false, maxLength: 256),
                        MaxNoOfWinner = c.Int(),
                        PrimeCode = c.String(maxLength: 64),
                        WinType = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignId);
            
            CreateTable(
                "dbo.CampaignWinners",
                c => new
                    {
                        CampaignId = c.Int(nullable: false),
                        CouponCode = c.String(nullable: false, maxLength: 128),
                        WinnerId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.CampaignId, t.CouponCode })
                .ForeignKey("dbo.Campaigns", t => t.CampaignId, cascadeDelete: true)
                .ForeignKey("dbo.Winners", t => t.WinnerId, cascadeDelete: true)
                .Index(t => t.CampaignId)
                .Index(t => t.WinnerId);
            
            CreateTable(
                "dbo.Winners",
                c => new
                    {
                        WinnerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 128),
                        AddressLine = c.String(nullable: false, maxLength: 256),
                        PostalCode = c.String(nullable: false, maxLength: 32),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WinnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CampaignWinners", "WinnerId", "dbo.Winners");
            DropForeignKey("dbo.CampaignWinners", "CampaignId", "dbo.Campaigns");
            DropIndex("dbo.CampaignWinners", new[] { "WinnerId" });
            DropIndex("dbo.CampaignWinners", new[] { "CampaignId" });
            DropTable("dbo.Winners");
            DropTable("dbo.CampaignWinners");
            DropTable("dbo.Campaigns");
        }
    }
}
