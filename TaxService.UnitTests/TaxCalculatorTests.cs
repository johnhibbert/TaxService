using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxService.Interfaces;
using TaxService.Calculators;
using TaxService.WebClients;
using System;
using Moq;
using TaxService.Exceptions;
using TaxService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaxService.UnitTests
{
    [TestClass]
    public class TaxCalculatorTests
    {
        
       


        #region Constructor Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WebClientNull_ShouldFail()
        {
            new TaxCalculator(null);
        }

        #endregion

        #region GetTaxRateForLocation Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetTaxRateForLocation_PostalCodeNull_ShouldPass()
        {
            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut.GetTaxRateForLocation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetTaxRateForLocation_PostalCodeEmptyString_ShouldPass()
        {
            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut.GetTaxRateForLocation(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetTaxRateForLocation_PostalCodeWhitespace_ShouldPass()
        {
            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut .GetTaxRateForLocation(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(TaxRateNotFoundException))]
        public async Task GetTaxRateForLocation_WebClientReturnsError_ShouldPass() 
        {
            string webClientReturnValue = "{\"status\":404,\"error\":\"Not Found\",\"detail\":\"Resource can not be found\"}";

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();
            mockWebClient.Setup(x => x.GetRequest(It.IsAny<string>())).ReturnsAsync(webClientReturnValue);

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut .GetTaxRateForLocation("TEST");
        }

        [TestMethod]
        public async Task GetTaxRateForLocation_CombinedRate_ShouldPass()
        {
            string webClientReturnValue = "{\"rate\":{\"combined_rate\":\"0.1025\"}}";

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();
            mockWebClient.Setup(x => x.GetRequest(It.IsAny<string>())).ReturnsAsync(webClientReturnValue);

            var sut = new TaxCalculator(mockWebClient.Object);
            var result = await sut.GetTaxRateForLocation("TEST");
            Assert.AreEqual(0.1025, result);
        }

        [TestMethod]
        public async Task GetTaxRateForLocation_StandardRateResponse_ShouldPass()
        {

            string webClientReturnValue = "{\"rate\":{\"standard_rate\":\"0.2\"}}";

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();
            mockWebClient.Setup(x => x.GetRequest(It.IsAny<string>())).ReturnsAsync(webClientReturnValue);

            var sut = new TaxCalculator(mockWebClient.Object);
            var result = await sut.GetTaxRateForLocation("TEST");
            Assert.AreEqual(0.2, result);
        }

        #endregion

        #region GetSalesTaxForThisOrder Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetSalesTaxForThisOrder_ToNull_ShouldPass()
        {
            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            double orderCost = 0.0;
            double shippingCost = 0.0;

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut.GetSalesTaxForThisOrder(from, null, orderCost, shippingCost);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetSalesTaxForThisOrder_FromNull_ShouldPass()
        {
            Address to = new Address()
            {
                Country = "US",
                PostalCode = "90002",
                State = "CA",
                City = "Los Angeles",
                Street = "1335 E 103rd St"
            };

            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            double orderCost = 0.0;
            double shippingCost = 0.0;

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut.GetSalesTaxForThisOrder(null, to, orderCost, shippingCost);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetSalesTaxForThisOrder_ToCountryNull_ShouldPass()
        {
            Address to = new Address()
            {
                Country = null,
                PostalCode = "90002",
                State = "CA",
                City = "Los Angeles",
                Street = "1335 E 103rd St"
            };

            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            double orderCost = 1.0;
            double shippingCost = 2.0;

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut.GetSalesTaxForThisOrder(from, to, orderCost, shippingCost);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetSalesTaxForThisOrder_ToCountryEmpty_ShouldPass()
        {
            Address to = new Address()
            {
                Country = string.Empty,
                PostalCode = "90002",
                State = "CA",
                City = "Los Angeles",
                Street = "1335 E 103rd St"
            };

            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            double orderCost = 1.0;
            double shippingCost = 2.0;

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut.GetSalesTaxForThisOrder(from, to, orderCost, shippingCost);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetSalesTaxForThisOrder_ToCountryWhitespace_ShouldPass()
        {
            Address to = new Address()
            {
                Country = string.Empty,
                PostalCode = "90002",
                State = "CA",
                City = "Los Angeles",
                Street = "1335 E 103rd St"
            };

            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            double orderCost = 1.0;
            double shippingCost = 2.0;

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();

            var sut = new TaxCalculator(mockWebClient.Object);
            await sut.GetSalesTaxForThisOrder(from, to, orderCost, shippingCost);
        }

        [TestMethod]
        [ExpectedException(typeof(AmountToCollectNotFoundException))]
        public async Task GetSalesTaxForThisOrder_WebClientReturnsError_ShouldPass()
        {
            string webClientReturnValue = "{\"status\":\"406\",\"error\":\"Not Acceptable\",\"detail\":\"to_country must be a two-letter ISO code.\"}";

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();
            mockWebClient.Setup(x => x.PostRequest(It.IsAny<string>(), It.IsAny<Dictionary<string,string>>())).ReturnsAsync(webClientReturnValue);

            Address to = new Address()
            {
                Country = "US",
                PostalCode = "90002",
                State = "CA",
                City = "Los Angeles",
                Street = "1335 E 103rd St"
            };

            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            double orderCost = 1.0;
            double shippingCost = 2.0;

            var sut = new TaxCalculator(mockWebClient.Object);
            var result = await sut.GetSalesTaxForThisOrder(from, to, orderCost, shippingCost);
        }

        [TestMethod]
        public async Task GetSalesTaxForThisOrder_WebClientReturnsResponse_ShouldPass()
        {
            string webClientReturnValue = "{\"tax\":{\"amount_to_collect\":0.39,\"freight_taxable\":false,\"has_nexus\":true,\"jurisdictions\":{\"city\":\"LA JOLLA\",\"country\":\"US\",\"county\":\"SAN DIEGO COUNTY\",\"state\":\"CA\"},\"order_total_amount\":15.0,\"rate\":0.0775,\"shipping\":10.0,\"tax_source\":\"destination\",\"taxable_amount\":5.0}}";

            Mock<IWebClient> mockWebClient = new Mock<IWebClient>();
            mockWebClient.Setup(x => x.PostRequest(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(webClientReturnValue);

            Address to = new Address()
            {
                Country = "US",
                PostalCode = "90002",
                State = "CA",
                City = "Los Angeles",
                Street = "1335 E 103rd St"
            };

            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            double orderCost = 1.0;
            double shippingCost = 2.0;

            var sut = new TaxCalculator(mockWebClient.Object);
            var result = await sut.GetSalesTaxForThisOrder(from, to, orderCost, shippingCost);

            Assert.AreEqual(0.39, result);
        }

        #endregion

    }
}
