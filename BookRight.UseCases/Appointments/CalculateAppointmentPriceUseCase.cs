using BookRight.UseCases.Interfaces;
using BookRight.Facade.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Facade.Interfaces.DiscountUseCases;

namespace BookRight.UseCases.Appointments
{
   /* public class CalculateAppointmentPriceUseCase : IAppointmentsUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        public CalculateAppointmentPriceUseCase(ICustomerRepository customerRepository, ITreatmentTypeRepository treatmentTypeRepository)
        {
            _customerRepository = customerRepository;
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        public async Task<decimal> Handle(Guid customerId, Guid treatmentTypeId, DateOnly treatmentDate)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null) throw new KeyNotFoundException("Kunden blev ikke fundet.");

            var treatmentType = await _treatmentTypeRepository.GetByIdAsync(treatmentTypeId);
            if (treatmentType == null) throw new KeyNotFoundException("Behandlingstypen blev ikke fundet.");

            decimal basePrice = treatmentType.Price.Value;

            var birthdayStrategy = new BirthdayDiscountStrategy();

            decimal discount = birthdayStrategy.CalculateDiscount(customer, basePrice, treatmentDate);

            decimal finalPrice = basePrice - discount;

            return finalPrice;
        }
    }*/
}
