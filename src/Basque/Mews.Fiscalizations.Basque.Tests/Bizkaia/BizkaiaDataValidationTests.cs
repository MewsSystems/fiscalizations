using Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;
using Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers;
using System.Text.Json;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia;

[TestFixture]
public class BizkaiaDataValidationTests
{
    [Test]
    public void Create_BizkaiaData_JsonSerialization_Succeeds()
    {
        var bizkaiaData = BizkaiaDataHelper.CreateSampleBizkaiaData();

        Assert.DoesNotThrow(() =>
        {
            string serializedData = JsonSerializer.Serialize(bizkaiaData);
            File.WriteAllText("tmp.json", serializedData);
            Assert.IsFalse(string.IsNullOrEmpty(serializedData));
        });
    }
}
