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

        [TestMethod, TestCategory("Domains.CreateDomainAsync")]
        public async Task CreateDomainAsync()
        {
            var createNewDomainResponse = await CreateNewDomainAsync();
            Assert.IsTrue(createNewDomainResponse.Item1 != null);
            Assert.IsTrue(createNewDomainResponse.Item1.Status == "ok");

            var request = new DeleteDomainRequest() { DomainId = createNewDomainResponse.Item2 };
            var response = await mailinatorClient.DomainsClient.DeleteDomainAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Domains.DeleteDomainAsync")]
        public async Task DeleteDomainAsync()
        {
            var createNewDomainResponse = await CreateNewDomainAsync();
            Assert.IsTrue(createNewDomainResponse.Item1 != null);
            Assert.IsTrue(createNewDomainResponse.Item1.Status == "ok");

            var request = new DeleteDomainRequest() { DomainId = createNewDomainResponse.Item2 };
            var response = await mailinatorClient.DomainsClient.DeleteDomainAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }
    }
}
