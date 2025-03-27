using CarsApi.Context;
using Microsoft.EntityFrameworkCore;

namespace CarsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Obtener cadena de conexión
            var connectionString = builder.Configuration.GetConnectionString("Connection");

            // Registrar servicio para la base de datos
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //  Agregar configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") // Permitir Angular
                              .AllowAnyMethod() // Permitir cualquier método (GET, POST, etc.)
                              .AllowAnyHeader(); // Permitir cualquier encabezado
                    });
            });

            var app = builder.Build();

            // Configuración del pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Habilitar CORS antes de usar controladores
            app.UseCors("AllowAngular");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
