using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Therapist;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookRight.Domain.Test
{
    public class TherapistTests
    {
        //Test 1 : Tjekker om behandler er ledig
        [Fact]
        public void IsAvailable_ReturnsTrue_WhenTherapistIsWorkingAndNoOverlap()
        {
            //Arrange

            var therapistId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var date = new DateOnly(2026, 05, 16);

            var timeslot = new TimeSlot(
                DateTime.Today.AddDays(1).AddHours(1),
                DateTime.Today.AddDays(1).AddHours(2));

            var newtherapistSchedule = TherapistSchedule.Create(therapistId, clinicId, date, isWorking:true);


            //Act
            var results = newtherapistSchedule.IsAvailable(timeslot);

            //Assert
            Assert.True(results);
        }

        public void AddBlockedSlot_Success()
        {
            //Arrange

        }

    }
}

