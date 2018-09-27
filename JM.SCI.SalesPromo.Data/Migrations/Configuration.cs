namespace JM.SCI.SalesPromo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Repository;
    using Model;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Repository context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Campaigns.AddOrUpdate(c => new { c.CampaignName },
                new Campaign()
                {
                    CampaignName = "Test Campaign",
                    MaxNoOfWinner = 10,
                    PrimeCode = "65353",
                    WinType = Model.Enum.WinType.Prime,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(5),
                    CreatedOn = DateTime.Now
                });
            context.Save();
        }
    }
}
