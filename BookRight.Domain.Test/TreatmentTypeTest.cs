using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Test
{
    public class TreatmentTypeTest
    {
        //Treatmenttype oprettelse uden navn giver exception
        [Fact]
        
        public void CreateTreatmentType_NoName_Exception()
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
                new TreatmentType(
                    name, 
                    durationMin,
                    maxParticipants,
                    money, 
                    combination,
                    requiredSpecialization));
        }
        //TreatmentType oprettelse med duration på under 1 giver exception
        [Fact]
        public void CreateTreatmentType_LessThan1min_Exception()
        {
            //Arrange
            var name = "Name";
            int durationMin = 0;
            int maxParticipants = 1;
            var money = new Money(400);
            bool combination = true;
            var requiredSpecialization = "Massør";

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new TreatmentType(
                    name, 
                    durationMin,
                    maxParticipants,
                    money, 
                    combination,
                    requiredSpecialization));
        }

        //TreamtnType giver exception om under 1 deltager
        [Fact]
        public void CreateTreatmentType_LessThan1Participant_Exception()
        {
            //Arrange
            var name = "Name";
            int durationMin = 40;
            int maxParticipants = -4;
            var money = new Money(400);
            bool combination = true;
            var requiredSpecialization = "Massør";

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new TreatmentType(
                    name,
                    durationMin,
                    maxParticipants,
                    money,
                    combination, 
                    requiredSpecialization));
        }

        //TreatmentType ændringer giver faktisk ændring
        [Fact]
        public void EditTreatmentTypeName_ChangesName()
        {
            //Arrange
            var name = "Name";
            int durationMin = 45;
            int maxParticipants = 1;
            var money = new Money(400);
            bool combination = true;
            var requiredSpecialization = "Massør";

            var testTreatmentType = new TreatmentType(
                name, 
                durationMin,
                maxParticipants,
                money, 
                combination, 
                requiredSpecialization);
            //Act
            var newName = "NewName";
            int newDurationMin = 45;
            int newMaxParticipants = 1;
            var newMoney = new Money(400);
            bool newCombination = true;
            var newRequiredSpecialization = "Massør";

            testTreatmentType.UpdateTreatmentType(
                newName,
                newDurationMin,
                newMaxParticipants,
                newMoney,
                newCombination, 
                newRequiredSpecialization);
            //Assert
            Assert.Equal(newName, testTreatmentType.Name);
        }

    }





    
}
