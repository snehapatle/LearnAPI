
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.EntityFrameworkCore;

namespace UseAWSSecrets
{
    public class Program
    {
        public  static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Step 1 : Create client
            var client = new AmazonSecretsManagerClient();

            //Step 2: get value
            var response =await  client.GetSecretValueAsync(new GetSecretValueRequest
            {
                SecretId = "DefaultDapperConnection1"
            });
            
            //Step 3: Use secret
            string conn = response.SecretString;
            Console.WriteLine($"ConnectionString: {conn}");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(conn);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(conn);
            });


            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(conn.Length);

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
