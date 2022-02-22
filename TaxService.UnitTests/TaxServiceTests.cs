using TaxService.Interfaces;
using TaxService.Calculators;
using TaxService.WebClients;
using System;
using Moq;
using TaxService.Exceptions;
using TaxService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaxService.UnitTests
{
    [TestClass]
    public class TaxServiceTests
    {
        #region Constructor Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_TaxCalculatorNull_ShouldFail()
        {
            new TaxService(null);
        }

        #endregion

        #region GetTaxRateForLocation Tests
        [TestMethod]
        public async Task GetTaxRateForLocation_ReturnsValueFromCalculator_ShouldFail()
        {
            Mock<ITaxCalculator> mockTaxCalculator = new Mock<ITaxCalculator>();
            mockTaxCalculator.Setup(x => x.GetTaxRateForLocation(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(3.0);

            var sut = new TaxService(mockTaxCalculator.Object);
            var result = await sut.GetTaxRateForLocation("test");

            Assert.AreEqual(3.0, result);

        }
        #endregion

        #region GetSalesTaxForThisOrder
        [TestMethod]
        public async Task GetSalesTaxForThisOrder_ReturnsValueFromCalculator_ShouldFail()
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

            Mock<ITaxCalculator> mockTaxCalculator = new Mock<ITaxCalculator>();
            mockTaxCalculator.Setup(x => x.GetSalesTaxForThisOrder(It.IsAny<Address>(), It.IsAny<Address>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(9.0);

            var sut = new TaxService(mockTaxCalculator.Object);
            var result = await sut.GetSalesTaxForThisOrder(to, from, orderCost, shippingCost);

            Assert.AreEqual(9.0, result);

        }
        #endregion

    }
}
