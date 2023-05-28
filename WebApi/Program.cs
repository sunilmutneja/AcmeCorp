using Application;
using Application.Interfaces;
using Domain.Entities.Customer;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Context;
using Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services.AddAutoMapper(typeof(Program));

//#region Swagger
//builder.Services.AddSwaggerGen(c =>
//{
//    c.IncludeXmlComments(string.Format(@"{0}\AcmeCorp.xml", System.AppDomain.CurrentDomain.BaseDirectory));
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "AcmeCorp",
//    });

//});
//#endregion

builder.Services.AddAutoMapper(typeof(CustomerProfile));
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

}

#region Swagger
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AcmeCorp");
});
#endregion

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

