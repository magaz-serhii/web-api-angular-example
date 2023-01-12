using DataAccess;
using Microsoft.EntityFrameworkCore;
using Products.WebApi.Services;

namespace Products.WebApi
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

            builder.Services.AddRouting(opts =>
            {
                opts.LowercaseUrls = true;
            });

            builder.Services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseNpgsql(builder.Configuration.GetConnectionString("productsapp"));
                // opts.LogTo(Console.Write);
            });

            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(new[] { typeof(Program) }));

            builder.Services.AddTransient<ProductsService>();
            builder.Services.AddTransient<CategoriesService>();
            builder.Services.AddTransient<ManufacturersService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}