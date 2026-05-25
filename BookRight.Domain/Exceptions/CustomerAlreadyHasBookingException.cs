using BookRight.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Exceptions
{
    public class CustomerAlreadyHasBookingException : DomainException
    {
        public CustomerAlreadyHasBookingException()
            : base(DomainErrorMessages.CUstomerAlreadyHasBooking)
        {
        }
    }
}
