using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetRevenueReport
{
    public sealed record GetRevenueReportRequest
    {
        public Guid? ClinicId { get; set; }
        public Guid? TherapistId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-1); // Standard: Sidste måned
        public DateTime EndDate { get; set; } = DateTime.Today;
    }
}
