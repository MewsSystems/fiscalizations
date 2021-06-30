using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Mews.Eet.Dto;
using Mews.Eet.Dto.Identifiers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Mews.Eet.Tests.IntegrationTests
{
    public class Basics
    {
        [Test]
        public async Task SendRevenueSimple()
        {
            var certificate = CreateCertificate(Fixtures.Second);
            var record = CreateSimpleRecord(certificate, Fixtures.Second);
            var client = new EetClient(certificate, EetEnvironment.Playground);
            var response = await client.SendRevenueAsync(record);
            Assert.Null(response.Error);
            Assert.NotNull(response.Success);
            Assert.NotNull(response.Success.FiscalCode);
            Assert.False(response.Warnings.Any());
        }

        [Test]
        public void TimeoutWorks()
        {
            var certificate = CreateCertificate(Fixtures.Second);
            var record = CreateSimpleRecord(certificate, Fixtures.Second);
            var client = new EetClient(certificate, EetEnvironment.Playground, TimeSpan.FromMilliseconds(1));
            Assert.ThrowsAsync<TaskCanceledException>(async () => await client.SendRevenueAsync(record));
        }

        [Test]
        public async Task SendRevenue()
        {
            var fixture = Fixtures.Third;

            var certificate = new Certificate(
                password: fixture.CertificatePassword,
                data: fixture.CertificateData,
                true
            );
            var record = new RevenueRecord(
                identification: new Identification(
                    taxPayerIdentifier: new TaxIdentifier(fixture.TaxId),
                    registryIdentifier: new RegistryIdentifier("01"),
                    premisesIdentifier: new PremisesIdentifier(fixture.PremisesId),
                    certificate: certificate
                ),
                revenue: new Revenue(
                    gross: new CurrencyValue(1234.00m),
                    notTaxable: new CurrencyValue(0.00m),
                    standardTaxRate: new TaxRateItem(
                        net: new CurrencyValue(100.00m),
                        tax: new CurrencyValue(21.00m),
                        goods: null
                    )
                ),
                billNumber: new BillNumber("2016-123")
            );
            var client = new EetClient(certificate, EetEnvironment.Playground);
            var response = await client.SendRevenueAsync(record);
            Assert.Null(response.Error);
            Assert.NotNull(response.Success);
            Assert.NotNull(response.Success.FiscalCode);
            Assert.False(response.Warnings.Any());
        }

        [Test]
        public async Task HandlesError()
        {
            var certificate = CreateCertificate(Fixtures.First);
            var record =  new RevenueRecord(
                    identification: new Identification(
                    taxPayerIdentifier: new TaxIdentifier("CZ111444789"),
                    registryIdentifier: new RegistryIdentifier("01"),
                    premisesIdentifier: new PremisesIdentifier(Fixtures.First.PremisesId),
                    certificate: certificate
                ),
                revenue: new Revenue(
                    gross: new CurrencyValue(1234.00m)
                ),
                billNumber: new BillNumber("2016-123")
            );
            var client = new EetClient(certificate, EetEnvironment.Playground);
            var response = await client.SendRevenueAsync(record);
            Assert.NotNull(response.Error);
            Assert.AreEqual(6, response.Error.Reason.Code);
        }

        [Test]
        public async Task LoggingIsSerializable()
        {
            var certificate = CreateCertificate(Fixtures.First);
            var record = CreateSimpleRecord(certificate, Fixtures.First);
            var client = new EetClient(
                certificate,
                EetEnvironment.Playground,
                httpTimeout: null,
                logger: new EetLogger((m, d) =>
                {
                    var jsonString = JsonConvert.SerializeObject(d);
                    StringAssert.StartsWith("{", jsonString);
                })
            );
            Assert.DoesNotThrowAsync(async () => await client.SendRevenueAsync(record));
        }

        [Test]
        public async Task ParallelRequestsWork()
        {
            var certificate = CreateCertificate(Fixtures.First);
            var record = CreateSimpleRecord(certificate, Fixtures.First);
            var client = new EetClient(certificate, EetEnvironment.Playground);

            var tasks = Enumerable.Range(0, 10).Select(i => client.SendRevenueAsync(record));
            Assert.DoesNotThrowAsync(async () => await Task.WhenAll(tasks).ConfigureAwait(continueOnCapturedContext: false));
        }

        [Test]
        public async Task TimingMeasurementWorks()
        {
            var certificate = CreateCertificate(Fixtures.First);
            var record = CreateSimpleRecord(certificate, Fixtures.First);
            var client = new EetClient(certificate, EetEnvironment.Playground);
            client.HttpRequestFinished += (sender, args) =>
            {
                var duration = args.Duration;
                Assert.That(duration, Is.InRange(0, 10000));
            };
            await client.SendRevenueAsync(record);
        }

        [Test]
        public async Task XmlExtractionWorks()
        {
            var certificate = CreateCertificate(Fixtures.First);
            var record = CreateSimpleRecord(certificate, Fixtures.First);
            var client = new EetClient(certificate, EetEnvironment.Playground);
            client.XmlMessageSerialized += (sender, args) =>
            {
                Assert.NotNull(args.XmlElement);
            };
            await client.SendRevenueAsync(record);
        }

        [Test]
        public async Task TaxIsSerializedCorrectly()
        {
            var fixture = Fixtures.First;
            var certificate = CreateCertificate(fixture);
            var record = new RevenueRecord(
                identification: new Identification(
                    taxPayerIdentifier: new TaxIdentifier(fixture.TaxId),
                    registryIdentifier: new RegistryIdentifier("01"),
                    premisesIdentifier: new PremisesIdentifier(fixture.PremisesId),
                    certificate: certificate
                ),
                revenue: new Revenue(
                    gross: new CurrencyValue(1234.00m),
                    lowerReducedTaxRate: new TaxRateItem(
                        new CurrencyValue(100m),
                        new CurrencyValue(10m),
                        new CurrencyValue(11m)
                    ),
                    reducedTaxRate: new TaxRateItem(
                        new CurrencyValue(200m),
                        new CurrencyValue(30m),
                        new CurrencyValue(12m)
                    ),
                    standardTaxRate: new TaxRateItem(
                        new CurrencyValue(300m),
                        new CurrencyValue(63m),
                        new CurrencyValue(13m)
                    ),
                    deposit: new CurrencyValue(432m),
                    usedDeposit: new CurrencyValue(543m)
                ),
                billNumber: new BillNumber("2016-123")
            );
            var client = new EetClient(certificate, EetEnvironment.Playground);
            client.XmlMessageSerialized += (sender, args) =>
            {
                var xmlElement = args.XmlElement;
                Assert.NotNull(xmlElement);

                var namespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
                namespaceManager.AddNamespace("eet", "http://fs.mfcr.cz/eet/schema/v3");
                var dataNode = xmlElement.SelectSingleNode("//eet:Data", namespaceManager);
                var attributes = dataNode.Attributes;
                Assert.AreEqual("300.00", attributes["zakl_dan1"].Value);
                Assert.AreEqual("200.00", attributes["zakl_dan2"].Value);
                Assert.AreEqual("100.00", attributes["zakl_dan3"].Value);
                Assert.AreEqual("63.00", attributes["dan1"].Value);
                Assert.AreEqual("30.00", attributes["dan2"].Value);
                Assert.AreEqual("10.00", attributes["dan3"].Value);
                Assert.AreEqual("11.00", attributes["pouzit_zboz3"].Value);
                Assert.AreEqual("12.00", attributes["pouzit_zboz2"].Value);
                Assert.AreEqual("13.00", attributes["pouzit_zboz1"].Value);
                Assert.AreEqual("543.00", attributes["cerp_zuct"].Value);
                Assert.AreEqual("432.00", attributes["urceno_cerp_zuct"].Value);
            };
            await client.SendRevenueAsync(record);
        }



        private Certificate CreateCertificate(TaxPayerFixture fixture)
        {
            return new Certificate(
                password: fixture.CertificatePassword,
                data: fixture.CertificateData
            );
        }

        private RevenueRecord CreateSimpleRecord(Certificate certificate, TaxPayerFixture fixture)
        {
            return new RevenueRecord(
                identification: new Identification(
                    taxPayerIdentifier: new TaxIdentifier(fixture.TaxId),
                    registryIdentifier: new RegistryIdentifier("01"),
                    premisesIdentifier: new PremisesIdentifier(fixture.PremisesId),
                    certificate: certificate
                ),
                revenue: new Revenue(
                    gross: new CurrencyValue(1234.00m)
                ),
                billNumber: new BillNumber("2016-123")
            );
        }
    }
}
