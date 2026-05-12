using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates.AddOn
{
    public class AddOn
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; }

        private AddOn() { } // parameterløs cunstructor for EF Core

        public AddOn(string name, Money price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }
    }

}
