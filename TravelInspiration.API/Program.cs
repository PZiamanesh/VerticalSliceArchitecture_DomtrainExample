using TravelInspiration.API;
using TravelInspiration.API.Shared.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddProblemDetails();
   
builder.Services.RegisterApplicationServices();

builder.Services.RegisterPersistenceServices(builder.Configuration);




var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.UseSlices();

app.Run();