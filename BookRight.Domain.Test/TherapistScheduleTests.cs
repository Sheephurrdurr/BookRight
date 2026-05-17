using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Therapist;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookRight.Domain.Test
{
    public class TherapistScheduleTests
    {
        //Test 1 : Tjekker om behandler arbejder
        [Fact]
        public void IsAvailable_ReturnsTrue_WhenTherapistIsWorkingAndNoOverlap()
        {
            //Arrange
            var therapistId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var date = new DateOnly(2028, 05, 16);
            var timeslot = new TimeSlot(
                            DateTime.Today.AddDays(1).AddHours(1),
                            DateTime.Today.AddDays(1).AddHours(2));
            var newtherapistSchedule = TherapistSchedule.Create(therapistId, clinicId, date, isWorking: true);
            //Act
            var results = newtherapistSchedule.IsAvailable(timeslot);
            //Assert
            Assert.True(results);
        }

        //Test 2 : Tjekker om behandler ikke arbejder
        [Fact]
        public void IsAvailableReturnsFalse_WhenTherapistIsNotWorkingAndNoOverlap()
        {
            //Arrange
            var therapistId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var date = new DateOnly(2028, 05, 16);
            var timeslot = new TimeSlot(
                            DateTime.Today.AddDays(1).AddHours(1),
                            DateTime.Today.AddDays(1).AddHours(2));
            var newtherapistSchedule = TherapistSchedule.Create(therapistId, clinicId, date, isWorking: false);
            //Act
            var results = newtherapistSchedule.IsAvailable(timeslot);
            //Assert
            Assert.False(results);
        }

        //Test 3 : Legger til en ny timeslot
        [Fact]
        public void AddBlockedSlot_Success()
        {
            //Arrange
            var therapistId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var date = new DateOnly(2028, 05, 16);
            var timeslot = new TimeSlot(
                            DateTime.Today.AddDays(1).AddHours(1),
                            DateTime.Today.AddDays(1).AddHours(2));
            var newtherapistSchedule = TherapistSchedule.Create(therapistId, clinicId, date, isWorking: true);

            //Act
            newtherapistSchedule.BlockSlot(timeslot);
            //Assert
            Assert.Contains(timeslot, newtherapistSchedule.BlockedSlots);
        }

        //Test 4 : Tjekker om behandler er på arbejde, men allerede har behandling det tidspunkt
        [Fact]
        public void IsWorking_butOverlap_returnsFalse()
        {
            //Arrange
            var therapistId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var date = new DateOnly(2028, 05, 16);
            var timeslot = new TimeSlot(
                            DateTime.Today.AddDays(1).AddHours(1),
                            DateTime.Today.AddDays(1).AddHours(2));
            var newtherapistSchedule = TherapistSchedule.Create(therapistId, clinicId, date, isWorking: true);
            newtherapistSchedule.BlockSlot(timeslot);
            //Act
            var results = newtherapistSchedule.IsAvailable(timeslot);

            //Assert
            Assert.False(results);
        }
    }
}
