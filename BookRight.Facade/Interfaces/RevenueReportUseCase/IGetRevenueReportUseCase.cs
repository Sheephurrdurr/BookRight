using BookRight.Facade.DTOs.GetRevenueReport;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces.RevenueReportUseCase
{
    public interface IGetRevenueReportUseCase
    {
        Task<GetRevenueReportResponse> ExecuteAsync(GetRevenueReportRequest request);
    }
}
