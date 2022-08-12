using mailinator_csharp_client.Models.Domains.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class DomainsEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Domains.GetAllDomainsAsync")]
        public async Task GetAllDomainsAsync()
        {
            var response = await mailinatorClient.DomainsClient.GetAllDomainsAsync();
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Domains.GetDomainAsync")]
        public async Task GetDomainAsync()
        {
            var request = new GetDomainRequest() { DomainId = Domain.Name };
            var response = await mailinatorClient.DomainsClient.GetDomainAsync(request);
            Assert.IsTrue(response != null);
        }
    }
}
