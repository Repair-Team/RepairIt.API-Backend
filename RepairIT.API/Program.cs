using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Persistence.Repositories;
using RepairIT.API.Repairing.Services;
using RepairIT.API.Shared.Domain.Repositories;
using RepairIT.API.Shared.Mapping;
using RepairIT.API.Shared.Persistence;
using RepairIT.API.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "RepairtIt RESTfull API",
        TermsOfService = new Uri("https://repair-team.github.io/Repair-It-Landing-Page/"),
        Contact = new OpenApiContact
        {
            Name = "RepairIt",
            Url = new Uri("https://repair-team.github.io/Repair-It-Landing-Page/")
        }
    });
    options.EnableAnnotations();
    
});

//Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

//Add LowerCase routes

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Dependency Injections


//Technicians
builder.Services.AddScoped<ITechnicianRepository, TechnicianRepository>();
builder.Services.AddScoped<ITechnicianService, TechnicianService>();

//Devices
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

//Reports
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

//Users
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



//AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();


//Validation for ensuring Database Objects are created 

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json","v1");
        options.RoutePrefix = "swagger";
    });

app.UseCors(options =>
{
    options.WithOrigins("https://repair-it-upc.web.app","http://localhost:8080");
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();