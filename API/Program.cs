
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  //middleware //swagger is a way of documenting our API endpoints https://localhost:5001/swagger/index.html pt documentation of our API
    app.UseSwaggerUI();
}


app.UseAuthorization(); //middleware for us to use authorisation

app.MapControllers(); //middleware to map controllers => our API knows where to send the HTTP requests

app.Run(); //runs the app
 