using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxService.Calculators;
using TaxService.Exceptions;
using TaxService.Interfaces;
using TaxService.Models;
using TaxService.WebClients;

namespace TaxService.UnitTests
{
    [TestClass]
    public class IntegrationTests
    {
        /*
        //These are not really Unit Tests.  They call into the real API.  These are closer to integration tests.
        //But tests like this can help us build up code with the real environment, especially when dealing with an external
        //API that may need a little fiddling to get right.
        //Especially if you're me and did not know how non-US postal codes worked.

        //Normally I'd write a console application to do this, but that's a little out of scope for this task
        //So I took this little shortcut.

        //I'm leaving them commented out, but uncomment them if you want to try them.

        //Sanitized so we don't commit creds to GitHub.  Again, reading this from a settings file that's in the gitignore would be better
        //But this is simple enough for this project.
        const string apiKey = "PLACEHOLDER";

        [TestMethod]
        public async Task IntegrationTest1()
        {
            ITaxCalculator taxCalculator = new TaxCalculator(new WebClient(apiKey));

            //var usTax = taxCalculator.GetTaxRateForLocation("  ");
            var usTax = await taxCalculator.GetTaxRateForLocation("90210");
            var otTax = await taxCalculator.GetTaxRateForLocation("K2V 1A5", "CA"); ;
            var auTax = await taxCalculator.GetTaxRateForLocation("3002", "AU", "VIC");
            var frTax = await taxCalculator.GetTaxRateForLocation("93200", "FR", "", "Saint - Denis", "20 Rue Jules Saulnier");

            //Don't do multiple asserts like this if you can avoid it.
            //It makes it harder to fix your unit tests.
            //But this isn't a real unit test anyway.
            Assert.AreEqual(0.1025, usTax);
            Assert.AreEqual(0.13, otTax);
            Assert.AreEqual(0.1, auTax);
            Assert.AreEqual(0.2, frTax);

        }

        [TestMethod]
        [ExpectedException(typeof(TaxRateNotFoundException))]
        public async Task IntegrationTest2()
        {
            ITaxCalculator taxCalculator = new TaxCalculator(new WebClient(apiKey));

            await taxCalculator.GetTaxRateForLocation("00000");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task IntegrationTest3()
        {
            ITaxCalculator taxCalculator = new TaxCalculator(new WebClient(apiKey));

            await taxCalculator.GetTaxRateForLocation(null);
        }

        [TestMethod]
        public async Task IntegrationTest4()
        {
            ITaxCalculator taxCalculator = new TaxCalculator(new WebClient(apiKey));

            Address from = new Address()
            {
                Country = "US",
                PostalCode = "92093",
                State = "CA",
                City = "La Jolla",
                Street = "9500 Gilman Drive"
            };

            Address to = new Address()
            {
                Country = "US",
                PostalCode = "90002",
                State = "CA",
                City = "Los Angeles",
                Street = "1335 E 103rd St"
            };

            var toBeCollected = await taxCalculator.GetSalesTaxForThisOrder(to, from, 5.00, 10.00);

            Assert.AreEqual(0.39, toBeCollected);
        }
        */
    }
}
