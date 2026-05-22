using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Test
{
    public class TimeSlotTest
    {
        private readonly DateTime _validStart = DateTime.Today.AddDays(1).AddHours(10);
        private readonly DateTime _validEnd = DateTime.Today.AddDays(1).AddHours(12);

        [Fact]
        public void Constructor_ValidTimes_CreatesTimeslot()
        {
            var timeSlot = new TimeSlot(_validStart, _validEnd);

            Assert.Equal(_validStart, timeSlot.StartTime);
            Assert.Equal(_validEnd, timeSlot.EndTime);
        }


        [Fact]
        public void Constructor_EndTimeBeforeStartTime_ThrowsArgumentException()
        {
            var end = _validStart.AddHours(-1);

            Assert.Throws<ArgumentException>(() => new TimeSlot(_validStart, end));
        }

        [Fact]
        public void Constructor_EndTimeEqualToStartTime_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TimeSlot(_validStart, _validStart));
        }

        [Fact]
        public void Duration_ReturnsCorrectTimeSpan()
        {
            var timeSlot = new TimeSlot(_validStart, _validEnd);

            Assert.Equal(120, timeSlot.DurationMinutes());
        }

        [Fact]
        public void OverlapsWith_OverlappingTimeSlot_ReturnsTrue()
        {
            var timeSlot = new TimeSlot(_validStart, _validEnd);
            var overlapping = new TimeSlot(_validStart.AddHours(1), _validEnd.AddHours(1));

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
        public void OverlapsWith_OverlappingDateTimes_ReturnsTrue()
        {
            var timeSlot = new TimeSlot(_validStart, _validEnd);

            Assert.True(timeSlot.OverlapsWith(_validEnd.AddHours(-1), _validEnd.AddHours(3)));
        }

        [Fact]
        public void ToString_ReturnsCorrectFormat()
        {
            var timeSlot = new TimeSlot(_validStart, _validEnd);

            Assert.Equal($"{_validStart} - {_validEnd}", timeSlot.ToString());
        }

        [Fact]
        public void OverlapsWith_OtherStartsBeforeAndEndsInside_ReturnsTrue()
        {
            var timeSlot = new TimeSlot(_validStart, _validEnd);
            var other = new TimeSlot(_validStart.AddHours(-1), _validStart.AddHours(1));

            Assert.True(timeSlot.OverlapsWith(other));
        }

        [Fact]
        public void OverlapsWith_OtherCoversEntireTimeSlot_ReturnsTrue()
        {
            var timeSlot = new TimeSlot(_validStart, _validEnd);
            var other = new TimeSlot(_validStart.AddHours(-1), _validEnd.AddHours(1));

            Assert.True(timeSlot.OverlapsWith(other));
        }
    }
}