using Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;
using System.Text.Json;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia;

[TestFixture]
public class BizkaiaDataValidationTests
{
    private static readonly BizkaiaHeaderData SampleBizkaiData = new BizkaiaHeaderData
    {
        With = "LROE",
        Section = "1.1",
        Inte = new Inte
        {
            TaxPayerIdentificationNumber = "NIF",
            Name = "Name",
            FamilyName1 = "Family name 1",
            FamiliyName2 = "Family name 2"
        },
        Drs = new Drs
        {
            Mode = "240",
            FiscalYear = "2022"
        }
    };

    [Test]
    public void Create_BizkaiaData_JsonSerialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            var serializedData = JsonSerializer.Serialize(SampleBizkaiData);
            Assert.IsNotEmpty(serializedData);
        });
    }
}
