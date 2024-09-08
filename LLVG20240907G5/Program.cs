using LLVG20240907G5.API.Endpoints; // Usar el namespace correcto para tus puntos finales
using LLVG20240907G5.API.Modelos.DAL; // Usar el namespace correcto para tus servicios DAL
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

// Crea un nuevo constructor de la aplicación web.
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios para habilitar la generación de documentación de API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura y agrega un contexto de base de datos para Entity Framework Core.
builder.Services.AddDbContext<CRMContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});

// Agrega una instancia de la clase ProductDAL como un servicio para la inyección de dependencias.
builder.Services.AddScoped<ProductDAL>();

// Construye la aplicación web.
var app = builder.Build();

// Agrega los puntos finales relacionados con los productos a la aplicación.
app.AddProductEndpoints(); // Asegúrate de que el método se llame correctamente

// Verifica si la aplicación se está ejecutando en un entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    // Habilita el uso de Swagger para la documentación de la API.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Agrega middleware para redirigir las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Ejecuta la aplicación.
app.Run();
