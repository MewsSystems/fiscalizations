using AutoFixture;
using Mews.Fiscalization.Greece.Dto.Xsd;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Schema;
using Xunit;

namespace Mews.Fiscalization.Greece.Tests.UnitTests
{
    public class OutboundRequestSerializationTests
    {
        private readonly Fixture _fixture;

        public OutboundRequestSerializationTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void OutboundRequestSerializationWorks()
        {
            // Arrange
            var invoicesDocModel = BuildInvoicesDocModel();

            var xmlDocument = XmlManipulator.Serialize(invoicesDocModel);

            xmlDocument.Schemas.Add(InvoicesDoc.Namespace, GetPath("Data/Schemas/InvoicesDoc.xsd"));
            xmlDocument.Schemas.Add(IncomeClassification.Namespace, GetPath("Data/Schemas/IncomeClassification.xsd"));
            xmlDocument.Schemas.Add(ExpenseClassification.Namespace, GetPath("Data/Schemas/ExpensesClassificaton.xsd"));

            bool hasErrors = false;
            ValidationEventHandler eventHandler = new ValidationEventHandler((o, e) =>
            {
                hasErrors = true;
            });

            // Act
            xmlDocument.Validate(eventHandler);

            // Assert
            Assert.False(hasErrors);
        }

        private InvoicesDoc BuildInvoicesDocModel()
        {
            var incomeClassification = _fixture
                .Build<IncomeClassification>()
                .With(x => x.SerialNumber, 1)
                .Create();

            var expensesClassification = _fixture
                .Build<ExpenseClassification>()
                .With(x => x.SerialNumber, 1)
                .Create();

            var invoiceDetail = _fixture
                .Build<InvoiceDetail>()
                .With(x => x.IncomeClassification, new[] { incomeClassification })
                .With(x => x.ExpenseClassification, new[] { expensesClassification })
                .Create();

            var invoiceSummary = _fixture
                .Build<InvoiceSummary>()
                .With(x => x.IncomeClassification, new[] { incomeClassification })
                .With(x => x.ExpenseClassification, new[] { expensesClassification })
                .Create();

            var taxes = _fixture
                .Build<Tax>()
                .With(x => x.TaxCategory, 1)
                .With(x => x.LineNumber, 3)
                .Create();

            var invoice = _fixture.Build<Invoice>()
                .With(x => x.Taxes, new[] { taxes })
                .With(x => x.InvoiceDetails, new[] { invoiceDetail })
                .With(x => x.InvoiceSummary, invoiceSummary)
                .Create();

            return new InvoicesDoc
            {
                Invoices = new[]
                {
                    invoice
                }
            };
        }

        private string GetPath(string relativePath)
        {
            var codeBaseUri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUri.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return Path.Combine(dirPath, relativePath);
        }
    }

}
