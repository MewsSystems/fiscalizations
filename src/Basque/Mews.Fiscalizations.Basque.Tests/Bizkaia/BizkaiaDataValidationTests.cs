using Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;
using Newtonsoft.Json;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia;

[TestFixture]
public class BizkaiaDataValidationTests
{
    private static readonly BizkaiaHeaderData SampleBizkaiData = new BizkaiaHeaderData
    {
        Issuer = new IssuerData
        {
            TaxpayerIdentificationNumber = "NIF",
            FirstNameOrCompanyName = "Name",
            Surname = "Family name 1",
            SecondSurname = "Family name 2"
        },
        FiscalData = new FiscalData
        {
            FiscalYear = 2022
        }
    };

    [Test]
    public void Create_BizkaiaData_JsonSerialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            var serializedData = JsonConvert.SerializeObject(SampleBizkaiData);
            Assert.IsNotEmpty(serializedData);
        });
    }
}
