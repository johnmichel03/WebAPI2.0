
using TechTalk.SpecFlow;
using Microsoft.Owin.Testing;
using JM.SCI.SalesPromo.Api;
using JM.SCI.SalesPromo.Data.Repository;
using JM.SCI.SalesPromo.Data.Core;

namespace JM.SCI.SalesPromo.Test.Int
{
    [Binding]
    public class TestBase
    {
        public static TestServer SalesPromoServer { get; set; }
        public static IRepository SalesPromoRepository = new Repository();
       

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            SalesPromoServer = TestServer.Create<Startup>();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            SalesPromoServer.Dispose();
        }
    }
}
