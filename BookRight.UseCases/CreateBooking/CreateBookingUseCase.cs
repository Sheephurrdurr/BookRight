using BookRight.Domain.Entities;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.ValueObjectDTOs;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;


namespace BookRight.UseCases.CreateBooking
{
    public class CreateBookingUseCase : ICreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        //private readonly IClinicRepository _clinicRepository;  ---LINJE TILFØJES IGEN NÅR ClinicRepository og interface er lavet

        public CreateBookingUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            // Tilføj: _clinicRepository = clinicRepository;
            _customerRepository = customerRepository;

        }

        public async Task<CreateBookingResponse> ExecuteAsync (CreateBookingRequest request)
        {
            // 1. valider at kunde findes
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id: {request.CustomerId} findes ikke.");

            var timeSlot = new TimeSlot(request.TimeSlotDto.StartTime, request.TimeSlotDto.EndTime); 

            // 2. opret booking via domain factory
            var booking = new Booking(
                Guid.NewGuid(),
                request.CustomerId,
                request.ClinicId,
                timeSlot
            );

            // 4. Gem i databasen
            await _bookingRepository.CreateAsync(booking);

            // 5. Returener response DTO
            return new CreateBookingResponse
            {
                BookingId = booking.Id,
                CustomerId = booking.CustomerId,
                TimeSlot = new TimeSlotDto(booking.TimeSlot.StartTime, booking.TimeSlot.EndTime),
                Status = booking.Status.ToString()
            };
        }
    }
}
