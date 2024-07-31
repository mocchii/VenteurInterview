using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            builder.Services.AddScoped<IKnightService, KnightService>();
            builder.Services.AddScoped<IKnightRepository, KnightRepository>();
            builder.Services.AddDbContext<CodingInterviewContext>();

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseRouting();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
