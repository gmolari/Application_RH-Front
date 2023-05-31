using Microsoft.EntityFrameworkCore;
using WebApiRh.Data;
using WebApiRh.Repositorios;
using WebApiRh.Repositorios.Interfaces;

namespace WebApiRh
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

            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<WebApiRhDBContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );
            builder.Services.AddCors();

            builder.Services.AddScoped<ITecnologiaRepositorio, TecnologiaRepositorio>();
            builder.Services.AddScoped<ICandidatoRepositorio, CandidatoRepositorio>();
            builder.Services.AddScoped<IVagaRepositorio, VagaRepositorio>();

            var app = builder.Build();

            app.UseCors(o => o.AllowAnyOrigin());

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