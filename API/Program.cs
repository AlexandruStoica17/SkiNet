using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline. //POSITION IS IMPORTANT
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");


    app.UseSwagger();  //middleware //swagger is a way of documenting our API endpoints https://localhost:5001/swagger/index.html pt documentation of our API
    app.UseSwaggerUI();


app.UseStaticFiles();

app.UseAuthorization(); //middleware for us to use authorisation

app.MapControllers(); //middleware to map controllers => our API knows where to send the HTTP requests

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    
    logger.LogError(ex, "An error occured during migration");
    }

app.Run(); //runs the app
 