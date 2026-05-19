using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Interfaces
{
    public interface IAppointmentsUseCase
    {
        Task<decimal> Handle(Guid customerId, Guid treatmentTypeId, DateOnly treatmentDate);
    }
}
