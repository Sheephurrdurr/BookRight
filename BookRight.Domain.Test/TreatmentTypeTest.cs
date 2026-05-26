using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace BookRight.Domain.Test
{
    public class TreatmentTypeTest
    {
        //Treatmenttype oprettelse uden navn giver domain exception
        [Fact]
        
        public void CreateTreatmentType_NoName_DomainException()
        {
            //Arrange
            var name = "";
            int durationMin = 40;
            int maxParticipants = 1;
            var money = new Money(400);
            bool combination = true;
            var requiredSpecialization = "Massør";

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new TreatmentType(name, durationMin, maxParticipants, money, combination, requiredSpecialization));
        }





    }
}
