using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.Interfaces
{
    public interface ITaxCalculator
    {
        //For these two methods, I wonder if I should instead have some sort of model that wraps these.
        //It might be cleaner, but they are just models.
        //The other question would be if they should be under an interface.
        //But again, they are just models with no functionality.
        //As I commented on the Address class, it feels like I won't need it?  But perhaps I am wrong.

        public Task<double> GetTaxRateForLocation(string postalCode, string countryCode = "US", string stateCode = "", string city = "", string street = "");

        public Task<double> GetSalesTaxForThisOrder(Address To, Address From, double orderCost, double shippingCost);
    }
}
