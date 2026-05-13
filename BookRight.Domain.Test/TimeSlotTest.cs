using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Test
{
	public class TimeSlotTest
	{
		private readonly DateTime _validStart = DateTime.Today.AddDays(1).AddHours(10);
		private readonly DateTime _validEnd = DateTime.Today.AddDays(1).AddHours(12);

		// Constructor test
		[Fact]
		public void Constructor_ValidTimes_CreatesTimeslot()
		{
			var timeSlot = new TimeSlot(_validStart, _validEnd);

			Assert.Equal(_validStart, timeSlot.StartTime);
			Assert.Equal(_validEnd, timeSlot.EndTime);

		}

		[Fact]
		public void Constructor_StartTimeInPast_ThrowsArgumentException()
		{
			var pastStart = DateTime.Today.AddDays(-1);
			var end = DateTime.Today.AddDays(-1).AddHours(2);

			Assert.Throws<ArgumentException>(() => new TimeSlot(pastStart, end)); 
	     }

	[Fact]
		public void Constructor_EndtimeBeforeStartTime_ThrowsArgumentException()
		{
			var end = _validStart.AddHours(-1);

			Assert.Throws<ArgumentException>(() => new TimeSlot(_validStart, end));

		}

		[Fact]
		public void Constructor_EndTimeEqualToStartTime_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() => new TimeSlot(_validStart, _validStart));

		}

		//Duration test
		[Fact]
		public void Duration_ReturnsCorrectTimeSpan()
		{
			var timeSlot = new TimeSlot(_validStart, _validEnd);

			Assert.Equal(120, timeSlot.DurationMinutes());
		}

		// OverLapsWith tests
		[Fact]
		public void OverlapsWith_OverlappingTimeSlot_ReturnsTrue()
		{
			//Arrange
			var start = DateTime.Now;
			var end = start.AddHours(2);
			var timeSlot = new TimeSlot(start, end);

			// Lav et overlap der starter midt i det første (1 to,e efter start)
			var overlapping = new TimeSlot(start.AddHours(1), end.AddHours(1));

			// Act Assert
			Assert.True(timeSlot.OverlapsWith(overlapping));

		}

		[Fact]
		public void OverlapsWith_NonOverlappingTimeSlot_ReturnsFalse()
		{
			var timeSlot = new TimeSlot(_validStart, _validEnd);
			var nonOverlapping = new TimeSlot(_validEnd.AddHours(1), _validEnd.AddHours(3));

			Assert.False(timeSlot.OverlapsWith(nonOverlapping));


		}

		[Fact]
		public void OverlapsWith_AdjacentTimeSlot_ReturnsFalse()
		{
			var timeSlot = new TimeSlot(_validStart, _validEnd);
			var adjacent = new TimeSlot(_validEnd, _validEnd.AddHours(2));

			Assert.False(timeSlot.OverlapsWith(adjacent));
		}

		[Fact]
		public void OverlapsWith_OverlappingDateTimes_Returns()
		{
			var timeSlot = new TimeSlot(_validStart, _validEnd);

			Assert.True(timeSlot.OverlapsWith(_validEnd.AddHours(1), _validEnd.AddHours(3)));

		}

		// ToString test
		public void ToString_ReturnsCorrectFormat()
		{
			var timeSlot = new TimeSlot(_validStart, _validEnd);

			Assert.Equal($"{_validStart} - {_validEnd}", timeSlot.ToString());

		}
	}
}
