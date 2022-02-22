using System;

namespace TaxService.Exceptions
{
    public class AmountToCollectNotFoundException : Exception
    {
        public AmountToCollectNotFoundException()
        {
        }

        public AmountToCollectNotFoundException(string message)
            : base(message)
        {
        }

        public AmountToCollectNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
