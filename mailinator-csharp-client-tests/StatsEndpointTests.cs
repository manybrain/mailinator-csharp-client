using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class StatsEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Stats.GetTeamStatsAsync")]
        public async Task GetTeamStatsAsync()
        {
            var response = await mailinatorClient.StatsClient.GetTeamStatsAsync();
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Stats.GetTeamAsync")]
        public async Task GetTeamAsync()
        {
            var response = await mailinatorClient.StatsClient.GetTeamAsync();
            Assert.IsTrue(response != null);
        }
    }
}
