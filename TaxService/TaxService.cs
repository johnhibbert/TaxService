using System;
using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models;

namespace TaxService
{
    public class TaxService
    {
        ITaxCalculator _taxCalculator;

        public TaxService(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator ?? throw new ArgumentNullException(nameof(taxCalculator));
        }

        public async Task<double> GetTaxRateForLocation(string postalCode, string countryCode = "US", string stateCode = "", string city = "", string street = "") 
        {
            return await _taxCalculator.GetTaxRateForLocation(postalCode, countryCode, stateCode, city, street);
        }

        public async Task<double> GetSalesTaxForThisOrder(Address To, Address From, double orderCost, double shippingCost)
{
            return await _taxCalculator.GetSalesTaxForThisOrder(To, From, orderCost, shippingCost);
        }

    }
}
