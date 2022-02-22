using System;

namespace TaxService.Exceptions
{
    public class TaxRateNotFoundException : Exception
    {
        public TaxRateNotFoundException()
        {
        }

        public TaxRateNotFoundException(string message)
            : base(message)
        {
        }

        public TaxRateNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
