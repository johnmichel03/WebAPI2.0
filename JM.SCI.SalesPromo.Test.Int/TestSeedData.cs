using JM.SCI.SalesPromo.Data.Core;
using JM.SCI.SalesPromo.Data.Repository;
using JM.SCI.SalesPromo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SCI.SalesPromo.Test.Int
{
    public class TestSeedData
    {
        private readonly IRepository _repository;
        public TestSeedData(IRepository repository)
        {
            _repository = repository;
        }
        public void SetupSeedData()
        {
            try
            {
                _repository.Add<Campaign>(new Campaign()
                {
                    CampaignName = "Test Campaign",
                    MaxNoOfWinner = 2,
                    PrimeCode = "ABCDEF",
                    WinType = Model.Enum.WinType.Prime,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(5),
                    CreatedOn = DateTime.Now
                });
                // _repository.Save();
                _repository.Add<CampaignWinner>(new CampaignWinner()
                {
                    CouponCode = "ABCDEF",
                    CampaignId = 1,
                    CreatedOn = DateTime.Now,
                    Winner = new Winner()
                    {
                        FirstName = "Nathaniel",
                        LastName = "John Michel",
                        AddressLine = "93 high road",
                        PostalCode = "IG12E",
                        CreatedOn = DateTime.Now
                    }
                });
                _repository.Save();
            }
            catch (Exception ex)
            {
               
            }
           
        }

        //public static void SetupSeedData()
        //{
        //    var sqlDropDb = @"IF (EXISTS (SELECT NAME FROM MASTER.DBO.SYSDATABASES 
        //            WHERE NAME + ']' = 'SCI_SalesPromo_TestDb'
        //            OR NAME = 'SCI_SalesPromo_TestDb'))
        //                BEGIN
        //                    DROP DATABASE SCI_SalesPromo_TestDb
        //                END
        //            ";
        //    var repo = new Repository();

        //    repo.Database.ExecuteSqlCommand(sqlDropDb);

        //}
    }
}
