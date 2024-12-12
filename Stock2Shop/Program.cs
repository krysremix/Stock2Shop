using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Stock2Shop.API;
using Stock2Shop.Application.Services;
using Stock2Shop.Core.Repositories;
using Stock2Shop.Core.Services;
using Stock2Shop.Infrastructure.Data;
using Stock2Shop.Infrastructure.Repositories;

namespace Stock2Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the DI container
            var configuration = builder.Configuration;
            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });
            });

            var app = builder.Build();

            // Apply pending migrations and create the database if it doesn't exist
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                dbContext.Database.Migrate(); // This ensures the SQLite database is created and migrations are applied
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1"));
            }

            app.MapControllers();

            app.Run();
        }
    }
}