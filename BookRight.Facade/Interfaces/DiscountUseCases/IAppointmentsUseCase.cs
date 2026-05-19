using System;
using System.Collections.Generic;
using System.Text;


namespace BookRight.Facade.Interfaces.DiscountUseCases
{
    public interface IAppointmentsUseCase
    {
        Task<decimal> Handle(Guid customerId, Guid treatmentTypeId, DateOnly treatmentDate);
    }
}
