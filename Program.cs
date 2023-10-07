using FluentValidation;
using ProductsDBApp.Config;
using ProductsDBApp.DAO;
using ProductsDBApp.DTO;
using ProductsDBApp.Service;
using ProductsDBApp.Validator;
using Serilog;
using System.Data.SqlClient;

namespace ProductsDBApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IProductDAO, ProductDAOImpl>();
            builder.Services.AddScoped<IProductService, ProductServiceImpl>();
            builder.Services.AddScoped<IValidator<ProductInsertDTO>, ProductInsertValidator>();
            builder.Services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateValidator>();
            builder.Services.AddAutoMapper(typeof(MapperProduct));

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            

            app.Run();
        }
    }
}