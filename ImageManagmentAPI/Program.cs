using DAL;
using DAL.Data;
using DAL.Helpers;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        // Add services to the container.
        // adding DAL services
        builder.Services.AddDAL(connectionString);
        // preventing cyclical references during JSON serialization
        builder.Services.AddControllers().
            AddJsonOptions(options=> options.JsonSerializerOptions.ReferenceHandler = 
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        // Seed initial data
        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                db.Database.Migrate();
                if (!db.Customers.Any())
                {
                    db.Customers.AddRange(
                        new Customer { Name = "Runtime Customer 1", Email = "rt.customer1@test.com", CustomerCode = "CUST-010" },
                        new Customer { Name = "Runtime Customer 2", Email = "rt.customer2@test.com", CustomerCode = "CUST-011" },
                        new Customer { Name = "Runtime Customer 3", Email = "rt.customer3@test.com", CustomerCode = "CUST-012" }
                    );
                }

                if (!db.Leads.Any())
                {
                    db.Leads.AddRange(
                        new Lead { Name = "Runtime Lead 1", Email = "rt.lead1@test.com", LeadSource = "LinkedIn" },
                        new Lead { Name = "Runtime Lead 2", Email = "rt.lead2@test.com", LeadSource = "Meta Ad Campaign" },
                        new Lead { Name = "Runtime Lead 3", Email = "rt.lead3@test.com", LeadSource = "Google Ad Campaign" }
                    );
                }
                db.SaveChanges();
            }
        }
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
    }
}