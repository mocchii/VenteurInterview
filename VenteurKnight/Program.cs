
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using VenteurKnight.Interfaces;
using VenteurKnight.Models;
using VenteurKnight.Repository;
using VenteurKnight.Service;

namespace VenteurKnight
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
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            builder.Services.AddScoped<IKnightService, KnightService>();
            builder.Services.AddScoped<IKnightRepository, KnightRepository>();
            builder.Services.AddDbContext<CodingInterviewContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
