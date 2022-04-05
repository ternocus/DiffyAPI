using DiffyAPI.AccessAPI.Core;
using DiffyAPI.AccessAPI.Database;
using DiffyAPI.CalendarAPI.Core;
using DiffyAPI.CalendarAPI.Database;
using DiffyAPI.CommunicationAPI.Core;
using DiffyAPI.CommunicationAPI.Database;
using DiffyAPI.UserAPI.Core;
using DiffyAPI.UserAPI.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to Access
builder.Services.AddScoped<IAccessManager, AccessManager>();
builder.Services.AddScoped<IAccessDataRepository, AccessDataRepository>();

// Add services to Calendar
builder.Services.AddScoped<ICalendarManager, CalendarManager>();
builder.Services.AddScoped<ICalendarDataRepository, CalendarDataRepository>();

// Add services to Communication
builder.Services.AddScoped<ICommunicationManager, CommunicationManager>();
builder.Services.AddScoped<ICommunicationDataRepository, CommunicationDataRepository>();

// Add services to UpdateUser
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();

// Add services to logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
