namespace TaxService.Models
{
    //It feels weird not adding an interface, but it's just a model, and 'YAGNI' says not to overdo it.
    public class Address
    {
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}


