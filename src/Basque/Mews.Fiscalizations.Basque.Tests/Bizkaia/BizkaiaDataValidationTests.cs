using Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;
using System.Text.Json;

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

    private const string ExpectedSerializationResult = "{\"con\":\"LROE\",\"apa\":\"1.1\",\"inte\":{\"nif\":\"NIF\",\"nrs\":\"Name\",\"ap1\":\"Family name 1\",\"ap2\":\"Family name 2\"},\"drs\":{\"mode\":\"240\",\"ejer\":2022}}";

    [Test]
    public void Create_BizkaiaData_JsonSerialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            var serializedData = JsonSerializer.Serialize(SampleBizkaiData);
            File.WriteAllText("tmp.json", serializedData);
            Assert.IsNotEmpty(serializedData);
            Assert.AreEqual(serializedData, ExpectedSerializationResult);
        });
    }

    [Test]
    public void Create_BizkaiaData_Serialization_Deserialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            var serializedData = JsonSerializer.Serialize(SampleBizkaiData);
            var bizkaiaHeaderDataDeserialized = JsonSerializer.Deserialize<BizkaiaHeaderData>(serializedData);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.HeaderType, SampleBizkaiData.HeaderType);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.Section, SampleBizkaiData.Section);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.FiscalData.FiscalYear, SampleBizkaiData.FiscalData.FiscalYear);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.FiscalData.Mode, SampleBizkaiData.FiscalData.Mode);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.Issuer.FirstNameOrCompanyName, SampleBizkaiData.Issuer.FirstNameOrCompanyName);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.Issuer.TaxpayerIdentificationNumber, SampleBizkaiData.Issuer.TaxpayerIdentificationNumber);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.Issuer.Surname, SampleBizkaiData.Issuer.Surname);
            Assert.AreEqual(bizkaiaHeaderDataDeserialized.Issuer.SecondSurname, SampleBizkaiData.Issuer.SecondSurname);
            Assert.IsNotEmpty(serializedData);
        });
    }

}
