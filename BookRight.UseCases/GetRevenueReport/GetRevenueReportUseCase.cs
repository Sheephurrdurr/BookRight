using BookRight.Domain.Enums;
using BookRight.Facade.DTOs.GetRevenueReport;
using BookRight.Facade.Interfaces.RevenueReportUseCase;
using BookRight.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.GetRevenueReport
{
    public class GetRevenueReportUseCase : IGetRevenueReportUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClinicRepository _clinicRepository;       // Tilføjes hvis nødvendigt
        private readonly ITherapistRepository _therapistRepository;

        public GetRevenueReportUseCase(IBookingRepository bookingRepository, IClinicRepository clinicRepository, ITherapistRepository therapistRepository)
        {
            _bookingRepository = bookingRepository;
            _clinicRepository = clinicRepository;
            _therapistRepository = therapistRepository;
        }

        public async Task<GetRevenueReportResponse> ExecuteAsync(GetRevenueReportRequest request)
        {
            // 1. Hent ALLE bookings med tilhørende linjer
            var allBookings = await _bookingRepository.GetAllAsync();

            var filteredBookings = allBookings
                .Where(b => b.Status == BookingStatus.Completed)
                .Where(b => !request.TherapistId.HasValue || b.TherapistId == request.TherapistId)
                .Where(b => !request.ClinicId.HasValue || b.ClinicId == request.ClinicId)
                .Where(b => b.TimeSlot.StartTime >= request.StartDate)
                .Where(b => b.TimeSlot.EndTime <= request.EndDate)
                .ToList();

            // 3. Beregn omsætning
            decimal totalRevenue = filteredBookings
                .SelectMany(b => b.Lines)
                .Sum(l => l.FinalPrice.Value);

            string clinicName = "Alle klinikker";
            string therapistName = "Alle behandlere";

            if (request.ClinicId.HasValue)
            {
                var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId.Value);
                if (clinic != null) clinicName = clinic.Name;
            }

            if (request.TherapistId.HasValue)
            {
                var therapist = await _therapistRepository.GetByIdAsync(request.TherapistId.Value);
                if (therapist != null) therapistName = therapist.Name.ToString();
            }

            return new GetRevenueReportResponse
            {
                TotalRevenue = totalRevenue,
                TotalAppointments = filteredBookings.Count,
                GeneratedAt = DateTime.Now,
                SelectedClinicName = clinicName,
                SelectedTherapistName = therapistName
            };
        }
    }
}
