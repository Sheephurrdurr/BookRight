using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetRevenueReport
{
    public sealed record GetRevenueReportResponse
    {
        public decimal TotalRevenue { get; set; }
        public int TotalAppointments { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.Now;
        public string SelectedClinicName { get; set; } = "Alle klinikker";
        public string SelectedTherapistName { get; set; } = "Alle behandlere";
    }
}
