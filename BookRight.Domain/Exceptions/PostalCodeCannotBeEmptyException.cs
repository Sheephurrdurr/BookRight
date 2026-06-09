using BookRight.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Exceptions
{
    public class PostalCodeCannotBeEmptyException : DomainException
    {
        public PostalCodeCannotBeEmptyException()
            :base(DomainErrorMessages.PostalCodeCannotBeEmpty)
        { }
    }
}
