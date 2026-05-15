using BookRight.BlazorUI.Components;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Persistence.Repositories;
using BookRight.Infrastructure.Repositories;
using BookRight.Infrastructure;

using BookRight.UseCases.CreateCustomer;
using BookRight.UseCases.CreateTherapist;
using BookRight.UseCases.GetAllCustomers;
using BookRight.UseCases.GetallTherapists;
using BookRight.UseCases.GetAllClinics;
using BookRight.UseCases.CreateBooking;

using BookRight.UseCases.Interfaces;

using Microsoft.EntityFrameworkCore;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Interfaces.ClinicsUseCases;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DbContext
builder.Services.AddDbContext<BookRightDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DI for DbSeeder
builder.Services.AddScoped<DbSeeder>();

// Register DI for repositories
builder.Services.AddScoped<ITherapistRepository, TherapistRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<ITreatmentTypeRepository, TreatmentTypeRepository>();

// Register DI for use cases
builder.Services.AddScoped<ICreateTherapistUseCase, CreateTherapistUseCase>();
builder.Services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
builder.Services.AddScoped<IGetAllTherapistsUseCase, GetAllTherapistsUseCase>();
builder.Services.AddScoped<IGetAllCustomersUseCase, GetAllCustomersUseCase>();
builder.Services.AddScoped<IGetAllClinicsUseCase, GetAllClinicsUseCase>();
builder.Services.AddScoped<ICreateBookingUseCase, CreateBookingUseCase>();

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
