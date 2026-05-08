using BookRight.Facade.DTOs.GetAllTreatmentTypeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces
{
    public interface IGetAllTreatmentTypeUseCase
    {
        Task<IReadOnlyList<GetAllTreatmentTypeResponse>> ExecuteAsync();
    }
}
