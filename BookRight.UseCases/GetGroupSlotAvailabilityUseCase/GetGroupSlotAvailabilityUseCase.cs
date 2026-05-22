using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.GetGroupSlotAvailabilityDTOs;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetGroupSlotAvailabilityUseCase
{
    public class GetGroupSlotAvailabilityUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        public GetGroupSlotAvailabilityUseCase(IBookingRepository bookingRepository, ITreatmentTypeRepository treatmentTypeRepository)
        {
            _bookingRepository = bookingRepository;
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        // This method calculates the availability of a group slot for a specific therapist treatment type and time slot.
        // It counts the current participants and compares it to the maximum allowed participants for that treatment type to determine if the slot is fully booked.
        public async Task<GetGroupSlotAvailabilityResponse> ExecuteAsync(GetGroupSlotAvailabilityRequest request)
        {
            var timeSlot = new TimeSlot(request.TimeSlot.StartTime, request.TimeSlot.EndTime);

            int countedParticipants = await _bookingRepository.CountParticipantsAsync(request.TherapistTreatmentTypeId, timeSlot);

            var treatmentTypes = await _treatmentTypeRepository
                .GetByTherapistTreatmentTypeIdsAsync(
                    new[] { request.TherapistTreatmentTypeId }
                );

            var treatmentType = treatmentTypes.GetValueOrDefault(request.TherapistTreatmentTypeId); // The 'Dictionary Way' to get the treatment type by ID and handle the case where it can't be found, without crashing with an exception.

            if (treatmentType == null)
            {
                throw new InvalidOperationException("Treatment type not found"); // ---Use Monas custom exceptions---
            }

            return new GetGroupSlotAvailabilityResponse
            {
                MaxParticipants = treatmentType.MaxParticipants,
                CurrentParticipants = countedParticipants,
                RemainingSlots = treatmentType.MaxParticipants - countedParticipants,
                IsFullyBooked = countedParticipants >= treatmentType.MaxParticipants
            };
        }
    }
}