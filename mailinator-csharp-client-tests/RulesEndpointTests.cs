using mailinator_csharp_client.Models.Rules.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class RulesEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Rules.CreateRuleAsync")]
        public async Task CreateRuleAsync()
        {
            var response = await CreateNewRuleAsync();
            Assert.IsTrue(response != null);

            await mailinatorClient.RulesClient.DeleteRuleAsync(new DeleteRuleRequest() { DomainId = Domain.Name, RuleId = response?.Id });
        }

        [TestMethod, TestCategory("Rules.EnableRuleAsync")]
        public async Task EnableRuleAsync()
        {
            var rule = await CreateNewRuleAsync();

            var request = new EnableRuleRequest() { DomainId = Domain.Name, RuleId = rule?.Id };
            var response = await mailinatorClient.RulesClient.EnableRuleAsync(request);
            Assert.IsTrue(response?.Status == "ok");

            await mailinatorClient.RulesClient.DeleteRuleAsync(new DeleteRuleRequest() { DomainId = Domain.Name, RuleId = rule?.Id });
        }

        [TestMethod, TestCategory("Rules.DisableRuleAsync")]
        public async Task DisableRuleAsync()
        {
            var rule = await CreateNewRuleAsync();

            var request = new DisableRuleRequest() { DomainId = Domain.Name, RuleId = rule?.Id };
            var response = await mailinatorClient.RulesClient.DisableRuleAsync(request);
            Assert.IsTrue(response?.Status == "ok");

            await mailinatorClient.RulesClient.DeleteRuleAsync(new DeleteRuleRequest() { DomainId = Domain.Name, RuleId = rule?.Id });
        }

        [TestMethod, TestCategory("Rules.GetAllRulesAsync")]
        public async Task GetAllRulesAsync()
        {
            var request = new GetAllRulesRequest() { DomainId = Domain.Name };
            var response = await mailinatorClient.RulesClient.GetAllRulesAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Rules.GetRuleAsync")]
        public async Task GetRuleAsync()
        {
            var rule = await CreateNewRuleAsync();

            var request = new GetRuleRequest() { DomainId = Domain.Name, RuleId = rule?.Id };
            var response = await mailinatorClient.RulesClient.GetRuleAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Id != null);

            await mailinatorClient.RulesClient.DeleteRuleAsync(new DeleteRuleRequest() { DomainId = Domain.Name, RuleId = rule?.Id });
        }

        [TestMethod, TestCategory("Rules.DeleteRuleAsync")]
        public async Task DeleteRuleAsync()
        {
            var rule = await CreateNewRuleAsync();

            var request = new DeleteRuleRequest() { DomainId = Domain.Name, RuleId = rule?.Id };
            var response = await mailinatorClient.RulesClient.DeleteRuleAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }
    }
}
