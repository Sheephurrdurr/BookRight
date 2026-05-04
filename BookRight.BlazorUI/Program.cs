using BookRight.BlazorUI.Components;
using BookRight.Facade.Interfaces;
using BookRight.Infrastructure.Persistence.Repositories;
using BookRight.Infrastructure.Persistence;
using BookRight.UseCases.CreateCustomer;
using BookRight.UseCases.CreateTherapist;
using BookRight.UseCases.GetAllCustomers;
using BookRight.UseCases.GetallTherapists;
using BookRight.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DbContext
builder.Services.AddDbContext<BookRightDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DI
builder.Services.AddScoped<ITherapistRepository, TherapistRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICreateTherapistUseCase, CreateTherapistUseCase>();
builder.Services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
builder.Services.AddScoped<IGetAllTherapistsUseCase, GetAllTherapistsUseCase>();
builder.Services.AddScoped<IGetAllCustomersUseCase, GetAllCustomersUseCase>();

var app = builder.Build();

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
