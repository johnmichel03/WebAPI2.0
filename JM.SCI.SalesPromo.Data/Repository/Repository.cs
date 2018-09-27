using JM.SCI.SalesPromo.Data.Core;
using JM.SCI.SalesPromo.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using JM.SCI.SalesPromo.Data.Migrations;

namespace JM.SCI.SalesPromo.Data.Repository
{
    public class Repository: SCIDbContext
    {
        public Repository() : base(nameOrConnectionString: "SCIDbConnection")
        {
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Repository>());
            // Configuration.ValidateOnSaveEnabled = false;
           //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Repository, Configuration>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<Repository>());
        }
      

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            return base.ShouldValidateEntity(entityEntry);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<Winner> Winners { get; set; }
        public virtual DbSet<CampaignWinner> CampaignWinners { get; set; }

    }
}
