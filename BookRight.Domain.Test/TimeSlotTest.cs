using System;
using System.Collections.Generic;
using System.Text; 
using Xunit;
using BookRight.Domain.ValueObjects;
using System.Data;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;


namespace BookRight.Domain.Test
{
	public class TimeSlotTest
	{
	   // Test 1: To TimeSlot overlapper
	   [Fact]
	   public void Overlapswith_OverlappingSlots_ShouldReturnTrue()
	   {
			//Arrange
			var slot1 = new TimeSlot(
			DateTime.Today.AddDays(1).AddHours(10),
			DateTime.Today.AddDays(1).AddHours(12));

			var slot2 = new TimeSlot(
			DateTime.Today.AddDays(1).AddHours(10),
			DateTime.Today.AddDays(1).AddHours(11));


			//Act
			var result = slot1.OverlapsWith(slot2);

			//Assert
			Assert.True(result);  // de overlapper
	   }

		// Test 2: To TimeSlot overlapper IKKE
		[Fact]
		public void OverlapsWith_NonOverlappingSlots_ShouldRerurnFalse()
		{
			// Arrange
			var slot1 = new TimeSlot(
			DateTime.Today.AddDays(1).AddHours(10),
			DateTime.Today.AddDays(1).AddHours(11));

			var slot2 = new TimeSlot(
			DateTime.Today.AddDays(1).AddHours(12),
			DateTime.Today.AddDays(1).AddHours(13));

			// Act
			var result = slot1.OverlapsWith(slot2);

			//Assert
			Assert.False(result); // de overlapper ikke
		}

		// Test 3: AftenBooking give 15% rabat
		[Fact]
		public void IsEveningSlot_AfterSixPM_ShouldReturnTrue()
		{
			//Arrange
			var slot = new TimeSlot(
			DateTime.Today.AddDays(1).AddHours(18),  //18:00
			DateTime.Today.AddDays(1).AddHours(19));

			// Act
			var result = slot.IsEveningOrWeekend();

			// Assert
			Assert.True(result);  // aftentillæg gælder
		}

		// Test 4: WeekendBooking giver 15% tillæg
		[Fact]
		public void IsWeekendSlot_OnWeekend_ShouldReturnTrue()
		{
			// Arrange
			var saturday = GetNextSaturday();
			var slot = new TimeSlot(
			  saturday.AddHours(10),
			  saturday.AddHours(11));

			// Act
			var result = slot.IsEveningOrWeekend();

			// Assert
			Assert.True(result); // weekendtillæg gælder
		}

		// Test 5: Hverdagsbooking om dagen giver IKKE tillæg
		[Fact]
		public void IsEveningOrWeekend_OrWeekend_weekdayDaytime_ShouldReturnFalse()
		{
			//Arrange
			var slot = new TimeSlot(
			DateTime.Today.AddDays(1).AddHours(10),
			DateTime.Today.AddDays(1).AddHours(11));

			// Act
			var result = slot.IsEveningOrWeekend();

			//Assert
			Assert.False(result); // ingen tillæg
		}

		// Hjælpemetode til at finde næste lørday
		private DateTime GetNextSaturday()
		{
			var date = DateTime.Today.AddDays(1);
			while (date.DayOfWeek != DayOfWeek.Saturday)
				date = date.AddDays(1);

			return date;
		}

	}
}
