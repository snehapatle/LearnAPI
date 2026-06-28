
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;

namespace UseAzureKeyVault
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

            var client = new SecretClient(
                new Uri("https://snehap-keyvault-demo.vault.azure.net/"),
                new DefaultAzureCredential());

            var conn = client.GetSecret("DefaultDapperConnection").Value.Value;
            Console.WriteLine($"ConnectionString: {conn}");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(conn);
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

            app.Run();
        }
    }
}
