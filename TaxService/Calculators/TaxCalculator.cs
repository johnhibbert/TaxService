using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Exceptions;
using TaxService.Interfaces;
using TaxService.Models;

namespace TaxService.Calculators
{
    public class TaxCalculator : ITaxCalculator
    {
        IWebClient _webClient;

        public TaxCalculator(IWebClient webClient)
        {
            _webClient = webClient ?? throw new ArgumentNullException(nameof(webClient));
        }

        public async Task<double> GetSalesTaxForThisOrder(Address From, Address To, double orderCost, double shippingCost)
        {

            if (From == null)
            {
                throw new ArgumentNullException(nameof(From));
            }
            if (To == null)
            {
                throw new ArgumentNullException(nameof(To));
            }
            if (To.Country == null)
            {
                throw new ArgumentException("To.Country cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(To.Country))
            {
                throw new ArgumentException("To.Country cannot be empty or whitespace.");
            }

            string url = "https://api.taxjar.com/v2/taxes/";
            var dict = GetPropsFromAddresses(To, From);
            dict.Add("amount", orderCost.ToString());
            dict.Add("shipping", shippingCost.ToString());

            string responseContent = await _webClient.PostRequest(url, dict);

            dynamic myObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            double toCollect;
            if (myObject.tax?.amount_to_collect == null)
            {
                //There is probably an argument to be made that this should be an ArgumentException.
                //But in theory this could also happen if the API is down or something.
                throw new AmountToCollectNotFoundException($"Amount to Collect not found.");
            }
            else
            {
                toCollect = Convert.ToDouble(myObject.tax.amount_to_collect);
            }

            return toCollect;
        }

        public async Task<double> GetTaxRateForLocation(string postalCode, string countryCode = "US", string stateCode = "", string city = "", string street = "")
        {
            if (postalCode == null)
            {
                throw new ArgumentNullException(nameof(postalCode));
            }

            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentException($"Argument {nameof(postalCode)} cannot be empty or whitespace;");
            }

            //See https://developers.taxjar.com/api/reference/#rates
            //https://taxjar.netlify.app/.netlify/functions/calculator?street=&city=&zip=K2V 1A5&country=CA

            string url = "https://api.taxjar.com/v2/rates/" + $"?street={street}&city={city}&state={stateCode}&country={countryCode}&zip={postalCode}";

            string responseContent = await _webClient.GetRequest(url);

            //Why dynamic?  The return values are different for US, Canada and other places, but Combined Rate is the same.
            //This does NOT work for Europe, so we check for nullness instead of 'zero-ness'.
            dynamic myObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            double rate;
            if (myObject.rate == null) 
            {
                //There is probably an argument to be made that this should be an ArgumentException.
                //But in theory this could also happen if the API is down or something.
                throw new TaxRateNotFoundException($"Tax Rate Not Found for ?street={street}&city={city}&state={stateCode}&country={countryCode}&zip={postalCode}");
            }
            //The requirements didn't get into the exact specifics, but these sound correct to me.
            else if (myObject.rate.combined_rate != null)
            {
                rate = Convert.ToDouble(myObject.rate.combined_rate);
            }
            else
            {
                rate = Convert.ToDouble(myObject.rate.standard_rate);
            }

            return rate;
        }


        private Dictionary<string, string> GetPropsFromAddresses(Address to, Address from) 
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (to.Street != null)
            {
                dict.Add("to_street", to.Street);
            }

            if (to.PostalCode != null)
            {
                dict.Add("to_zip", to.PostalCode);
            }

            if (to.City != null)
            {
                dict.Add("to_city", to.City);
            }

            if (to.State != null)
            {
                dict.Add("to_state", to.State);
            }

            if (to.Country != null)
            {
                dict.Add("to_country", to.Country);
            }

            if (from.Street != null)
            {
                dict.Add("from_street", from.Street);
            }

            if (from.PostalCode != null)
            {
                dict.Add("from_zip", from.PostalCode);
            }

            if (from.City != null)
            {
                dict.Add("from_city", from.City);
            }

            if (from.State != null)
            {
                dict.Add("from_state", from.State);
            }

            if (from.Country != null)
            {
                dict.Add("from_country", from.Country);
            }

            return dict;
        }
    }
}
