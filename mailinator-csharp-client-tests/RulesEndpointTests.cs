using mailinator_csharp_client.Models.Rules.Entities;
using mailinator_csharp_client.Models.Rules.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class RulesEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Rules.CreateRuleAsync")]
        public async Task CreateRuleAsync()
        {
            RuleToCreate rule = new RuleToCreate()
            {
                Name = "RuleName",
                Priority = 15,
                Description = "Description",
                Conditions = new List<Condition>()
                {
                    new Condition()
                    {
                        Operation = OperationType.PREFIX,
                        ConditionData = new ConditionData()
                        {
                            Field = "to",
                            Value = "raul"
                        }
                    }
                },
                Enabled = true,
                Match = MatchType.ANY,
                Actions = new List<ActionRule>() { new ActionRule() { Action = ActionType.WEBHOOK, 
                                                                      ActionData = new ActionData() { Url = "https://google.com" } } }
            };
            var request = new CreateRuleRequest() { DomainId = Domain.Name, Rule = rule };
            var response = await mailinatorClient.RulesClient.CreateRuleAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Rules.EnableRuleAsync")]
        public async Task EnableRuleAsync()
        {
            var request = new EnableRuleRequest() { DomainId = Domain.Name, RuleId = Rule.Id };
            var response = await mailinatorClient.RulesClient.EnableRuleAsync(request);
            Assert.IsTrue(response?.Status == "ok");
        }

        [TestMethod, TestCategory("Rules.DisableRuleAsync")]
        public async Task DisableRuleAsync()
        {
            var request = new DisableRuleRequest() { DomainId = Domain.Name, RuleId = Rule.Id };
            var response = await mailinatorClient.RulesClient.DisableRuleAsync(request);
            Assert.IsTrue(response?.Status == "ok");
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
            var request = new GetRuleRequest() { DomainId = Domain.Name, RuleId = Rule.Id };
            var response = await mailinatorClient.RulesClient.GetRuleAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Rules.DeleteRuleAsync")]
        public async Task DeleteRuleAsync()
        {
            var request = new DeleteRuleRequest() { DomainId = DeleteDomain, RuleId = Rule.Id };
            var response = await mailinatorClient.RulesClient.DeleteRuleAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }
    }
}
