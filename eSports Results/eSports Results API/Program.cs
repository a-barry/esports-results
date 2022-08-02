using eSports_Results_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(sp => new HttpClient() );
builder.Services.AddTransient<Common.Interfaces.IDataSource, ZwiftPowerDataSource.Zwiftpower>();
builder.Services.AddTransient<Common.Interfaces.IResultsProcessor, WLCSeriesResultsProcessor.WLCEventProcessor>();

// services
builder.Services.AddScoped<ResultsService>();
builder.Services.AddScoped<SeriesService>();

builder.Services.AddCors(options =>
 {
     options.AddDefaultPolicy(
         policy =>
         {
             policy.WithOrigins("https://localhost:7257");
         });
 });


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

app.UseCors();

app.Run();
