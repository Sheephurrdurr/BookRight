using BookRight.Facade.DTOs.UpdateClinicDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces.ClinicsUseCases
{
    public interface IUpdateClinicUseCase 
    {
        Task ExecuteAsync(UpdateClinicRequest request); 
    }
}
