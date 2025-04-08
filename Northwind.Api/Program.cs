using Microsoft.EntityFrameworkCore;
using Northwind.Api.Contexts;
using Northwind.Api.Services;
using Northwind.Api.Services.Interfaces;

namespace Northwind.Api
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

            /*Initiera NorthwindContext(dbContext) för Dependency Injection*/
            builder.Services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindContext"));
            });

            builder.Services.AddScoped<IProductService,ProductService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorClient", policy =>
                {
                    policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowBlazorClient");

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
