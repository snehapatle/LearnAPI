
using Microsoft.EntityFrameworkCore;
using UseAppSettings.Models;

namespace UseAppSettings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connection = builder.Configuration.GetConnectionString("DbConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connection));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapPost("/employeepost", (Employee emp) =>
            {
                return Results.Created($"employee created {emp}", emp);
            }).WithName("Minimal").WithTags("Minimal API");
            app.MapGet("/Health", () =>            
            "Healthy app").WithTags("Health End");

            app.Run();
        }
    }
}
