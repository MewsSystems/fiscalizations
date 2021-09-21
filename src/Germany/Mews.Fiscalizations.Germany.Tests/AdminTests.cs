using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests
{
    [TestFixture]
    public class AdminTests
    {
        [Test]
        [Order(1)]
        public async Task AdminChangePinSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
            var changePinResponse = await client.ChangeAdminPinAsync(accessToken, TestFixture.TssId, adminPuk: TestFixture.AdminPuk, newAdminPin: "1234567890");

            Assert.IsTrue(changePinResponse.IsSuccess);
        }

        [Test]
        [Order(2)]
        public async Task AdminLoginAndLogoutSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

            Assert.DoesNotThrowAsync(() => client.AdminLoginAsync(accessToken, TestFixture.TssId, "1234567890"));
            Assert.DoesNotThrowAsync(() => client.AdminLogoutAsync(TestFixture.TssId));
        }
    }
}
