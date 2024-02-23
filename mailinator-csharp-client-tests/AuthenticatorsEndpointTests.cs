using mailinator_csharp_client.Models.Authenticators.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class AuthenticatorsEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Authenticators.InstantTOTP2FACodeAsync")]
        public async Task InstantTOTP2FACodeAsync()
        {
            var request = new InstantTOTP2FACodeRequest() { TotpSecretKey = AuthSecret };
            var response = await mailinatorClient.AuthenticatorsClient.InstantTOTP2FACodeAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Authenticators.GetAuthenticatorsAsync")]
        public async Task GetAuthenticatorsAsync()
        {
            var response = await mailinatorClient.AuthenticatorsClient.GetAuthenticatorsAsync();
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Authenticators.GetAuthenticatorsByIdAsync")]
        public async Task GetAuthenticatorsByIdAsync()
        {
            var request = new GetAuthenticatorsByIdRequest() { Id = AuthId };
            var response = await mailinatorClient.AuthenticatorsClient.GetAuthenticatorsByIdAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Authenticators.GetAuthenticatorAsync")]
        public async Task GetAuthenticatorAsync()
        {
            var response = await mailinatorClient.AuthenticatorsClient.GetAuthenticatorAsync();
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Authenticators.GetAuthenticatorByIdAsync")]
        public async Task GetAuthenticatorByIdAsync()
        {
            var request = new GetAuthenticatorByIdRequest() { Id = AuthId };
            var response = await mailinatorClient.AuthenticatorsClient.GetAuthenticatorByIdAsync(request);
            Assert.IsTrue(response != null);
        }
    }
}
