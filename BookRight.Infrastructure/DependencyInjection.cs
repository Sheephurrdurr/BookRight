using BookRight.Domain.Services;
using BookRight.Domain.Services.DiscountStrategies;
using BookRight.Domain.Services.Interfaces;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.Facade.Interfaces.ClinicsUseCases;
using BookRight.Facade.Interfaces.CustomerUseCases;
using BookRight.Facade.Interfaces.DiscountUseCases;
using BookRight.Facade.Interfaces.RevenueReportUseCase;
using BookRight.Facade.Interfaces.TherapistUseCases;
using BookRight.Facade.Interfaces.TreatmentTypesUseCase;
using BookRight.Facade.Interfaces.TreatmentTypeUseCase;
using BookRight.Infrastructure;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Repositories;
using BookRight.UseCases.AddQualification;
using BookRight.UseCases.CampaignDiscountUseCases;
using BookRight.UseCases.ChangeCustomerHealthNotes;
using BookRight.UseCases.CreateBooking;
using BookRight.UseCases.CreateClinic;
using BookRight.UseCases.CreateCustomer;
using BookRight.UseCases.CreateTherapist;
using BookRight.UseCases.CreateTreatmentType;
using BookRight.UseCases.DeleteTreatmentType;
using BookRight.UseCases.GetAllCampaignDiscounts;
using BookRight.UseCases.GetAllClinics;
using BookRight.UseCases.GetAllCustomers;
using BookRight.UseCases.GetallTherapists;
using BookRight.UseCases.GetAllTherapistTreatmentType;
using BookRight.UseCases.GetAllTreatmentTypes;
using BookRight.UseCases.GetBookingByWeek;
using BookRight.UseCases.GetBookingsForToday;
using BookRight.UseCases.GetClinicById;
using BookRight.UseCases.GetCustomerById;
using BookRight.UseCases.GetCustomerHealthNotes;
using BookRight.UseCases.GetCustomerHistory;
using BookRight.UseCases.GetGroupSlotAvailabilityUseCase;
using BookRight.UseCases.GetRevenueReport;
using BookRight.UseCases.Interfaces;
using BookRight.UseCases.MarkBookingArrived;
using BookRight.UseCases.MarkBookingAsNoShow;
using BookRight.UseCases.MarkBookingCompleted;
using BookRight.UseCases.RestoreBookingFromNoShow;
using BookRight.UseCases.UpdateClinic;
using BookRight.UseCases.UpdateTherapist;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            // Add DbContext
            services.AddDbContext<BookRightDbContext>(options =>
                options.UseSqlServer(connectionString));

            RegisterDependencies(services);
            return services;
        }
        private static void RegisterDependencies(IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<ITreatmentTypeRepository, TreatmentTypeRepository>();
            services.AddScoped<ITherapistRepository, TherapistRepository>();
            services.AddScoped<ICampaignDiscountRepository, CampaignDiscountRepository>();

            // Register DI for DbSeeder
            services.AddScoped<DbSeeder>();

            // Register Services
            services.AddScoped<LoyaltyService>(); // Domain service live in the Domain layer. They dont need any data to be mocked or any other implementation, so no interface.
            services.AddScoped<IDiscountStrategy, LoyaltyDiscountStrategy>(); 
            services.AddScoped<IDiscountStrategy, BirthdayDiscountStrategy>();
            services.AddScoped<IDiscountStrategy, CampaignDiscountStrategy>();
            services.AddScoped<PriceCalculatorService>();
            services.AddScoped<DoubleBookingVerificationService>();


            // Register DI for use cases
            services.AddScoped<ICreateTherapistUseCase, CreateTherapistUseCase>();
            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
            services.AddScoped<IGetAllTherapistsUseCase, GetAllTherapistsUseCase>();
            services.AddScoped<IGetAllCustomersUseCase, GetAllCustomersUseCase>();
            services.AddScoped<ICreateBookingUseCase, CreateBookingUseCase>();
            services.AddScoped<IGetAllClinicsUseCase, GetAllClinicsUseCase>();
            services.AddScoped<IGetAllTherapistTreatmentTypesUseCase, GetAllTherapistTreatmentTypeUseCase>();
            services.AddScoped<IMarkBookingAsNoShowUseCase, MarkBookingAsNoShowUseCase>();
            services.AddScoped<IMarkBookingArrivedUseCase, MarkArrivedUseCase>();
            services.AddScoped<IMarkBookingCompletedUseCase, MarkBookingCompletedUseCase>();
            services.AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
            services.AddScoped<IGetCustomerHealthNotesUseCase, GetCustomerHealthNotesUseCase>();
            services.AddScoped<IChangeCustomerHealthNotesUseCase, ChangeCustomerHealthNotesUseCase>();
            services.AddScoped<IRestoreBookingFromNoShowUseCase, RestoreBookingFromNoShowUseCase>();
            services.AddScoped<IGetCustomerHistoryUseCase, GetCustomerHistoryUseCase>();
            services.AddScoped<IGetGroupSlotAvailabilityUseCase, GetGroupSlotAvailabilityUseCase>();
            services.AddScoped<ICreateCampaignDiscountUseCase, CreateCampaignDiscountUseCase>();
            services.AddScoped<IGetAllTreatmentTypesUseCase, GetAllTreatmentTypesUseCase>();
            services.AddScoped<IGetRevenueReportUseCase, GetRevenueReportUseCase>();
            services.AddScoped<IGetAllClinicsUseCase, GetAllClinicsUseCase>();
            services.AddScoped<IGetAllTherapistsUseCase, GetAllTherapistsUseCase>();
            services.AddScoped<ICreateClinicUseCase, CreateClinicUseCase>();
            services.AddScoped<IUpdateClinicUseCase, UpdateClinicUseCase>();
            services.AddScoped<IGetAllCampaignDiscountsUseCase, GetAllCampaignDiscountsUseCase>();
            services.AddScoped<IUpdateTherapistUseCase, UpdateTherapistUseCase>();
            services.AddScoped<IGetClinicByIdUseCase, GetClinicByIdUseCase>();
            services.AddScoped<ICreateTreatmentTypeUseCase, CreateTreatmentTypeUseCase>();
            services.AddScoped<IDeleteTreatmentTypeUseCase, DeleteTreatmentTypeUseCase>();
            services.AddScoped<IGetByWeekUseCase, GetByWeekUseCase>();
            services.AddScoped<IAddQualificationUseCase, AddQualificationUseCase>();
            services.AddScoped<IGetBookingsForTodayUseCase, GetBookingsForTodayUseCase>();

        }
    }
}
