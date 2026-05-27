using BookRight.BlazorUI.Components;
using BookRight.Domain.Services;
using BookRight.Domain.Services.DiscountStrategies;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.Facade.Interfaces.ClinicsUseCases;
using BookRight.Facade.Interfaces.CustomerUseCases;
using BookRight.Facade.Interfaces.DiscountUseCases;
using BookRight.Facade.Interfaces.RevenueReportUseCase;
using BookRight.Facade.Interfaces.TherapistUseCases;
using BookRight.Facade.Interfaces.TreatmentTypesUseCase;
using BookRight.Infrastructure;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Persistence.Repositories;
using BookRight.Infrastructure.Repositories;
using BookRight.UseCases.CampaignDiscountUseCases;
using BookRight.UseCases.ChangeCustomerHealthNotes;
using BookRight.UseCases.CreateBooking;
using BookRight.UseCases.CreateClinic;
using BookRight.UseCases.CreateCustomer;
using BookRight.UseCases.CreateTherapist;
using BookRight.UseCases.GetAllCampaignDiscounts;
using BookRight.UseCases.GetAllClinics;
using BookRight.UseCases.GetAllCustomers;
using BookRight.UseCases.GetallTherapists;
using BookRight.UseCases.GetAllTherapistTreatmentType;
using BookRight.UseCases.GetAllTreatmentTypes;
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
using BookRight.UseCases.UpdateTherapist;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DbContext
builder.Services.AddDbContext<BookRightDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Domain Services
builder.Services.AddScoped<LoyaltyService>();
builder.Services.AddScoped<DoubleBookingVerificationService>();

// Register DI for PriceCalculatorService with its dependencies
builder.Services.AddScoped<IDiscountStrategy, LoyaltyDiscountStrategy>();
builder.Services.AddScoped<IDiscountStrategy, BirthdayDiscountStrategy>();
builder.Services.AddScoped<IDiscountStrategy, CampaignDiscountStrategy>();
builder.Services.AddScoped<PriceCalculatorService>();

// Register DI for DbSeeder
builder.Services.AddScoped<DbSeeder>();

// Register DI for repositories
builder.Services.AddScoped<ITherapistRepository, TherapistRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<ITreatmentTypeRepository, TreatmentTypeRepository>();
builder.Services.AddScoped<ICampaignDiscountRepository, CampaignDiscountRepository>();

// Register DI for use cases
builder.Services.AddScoped<ICreateTherapistUseCase, CreateTherapistUseCase>();
builder.Services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
builder.Services.AddScoped<IGetAllTherapistsUseCase, GetAllTherapistsUseCase>();
builder.Services.AddScoped<IGetAllCustomersUseCase, GetAllCustomersUseCase>();
builder.Services.AddScoped<ICreateBookingUseCase, CreateBookingUseCase>();
builder.Services.AddScoped<IGetAllClinicsUseCase, GetAllClinicsUseCase>();
builder.Services.AddScoped<IGetAllTherapistTreatmentTypesUseCase, GetAllTherapistTreatmentTypeUseCase>();
builder.Services.AddScoped<IMarkBookingAsNoShowUseCase, MarkBookingAsNoShowUseCase>();
builder.Services.AddScoped<IMarkBookingArrivedUseCase, MarkArrivedUseCase>();
builder.Services.AddScoped<IMarkBookingCompletedUseCase, MarkBookingCompletedUseCase>();
builder.Services.AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
builder.Services.AddScoped<IGetCustomerHealthNotesUseCase, GetCustomerHealthNotesUseCase>();
builder.Services.AddScoped<IChangeCustomerHealthNotesUseCase, ChangeCustomerHealthNotesUseCase>();
builder.Services.AddScoped<IRestoreBookingFromNoShowUseCase, RestoreBookingFromNoShowUseCase>();
builder.Services.AddScoped<IGetCustomerHistoryUseCase, GetCustomerHistoryUseCase>();
builder.Services.AddScoped<IGetGroupSlotAvailabilityUseCase, GetGroupSlotAvailabilityUseCase>();
builder.Services.AddScoped<ICreateCampaignDiscountUseCase, CreateCampaignDiscountUseCase>();
builder.Services.AddScoped<IGetAllTreatmentTypesUseCase, GetAllTreatmentTypesUseCase>();
builder.Services.AddScoped<IGetRevenueReportUseCase, GetRevenueReportUseCase>();
builder.Services.AddScoped<IGetAllClinicsUseCase, GetAllClinicsUseCase>();
builder.Services.AddScoped<IGetAllTherapistsUseCase, GetAllTherapistsUseCase>();
builder.Services.AddScoped<ICreateClinicUseCase, CreateClinicUseCase>();
builder.Services.AddScoped<IGetAllCampaignDiscountsUseCase, GetAllCampaignDiscountsUseCase>();
builder.Services.AddScoped<IUpdateTherapistUseCase, UpdateTherapistUseCase>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbSeeder = services.GetRequiredService<DbSeeder>();
    await dbSeeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
