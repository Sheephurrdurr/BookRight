using BookRight.Facade.DTOs.GetAvailableTherapistsDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces.TherapistUseCases
{
    public interface IGetAvailableTherapistsUseCase
    {
        Task<List<AvailableTherapistResponse>> ExecuteAsync(GetAvailableTherapistsRequest request);
    }
}
