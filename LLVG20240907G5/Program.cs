using LLVG20240907G5.API.Endpoints; // Usar el namespace correcto para tus puntos finales
using LLVG20240907G5.API.Modelos.DAL; // Usar el namespace correcto para tus servicios DAL
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

// Crea un nuevo constructor de la aplicaci�n web.
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios para habilitar la generaci�n de documentaci�n de API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura y agrega un contexto de base de datos para Entity Framework Core.
builder.Services.AddDbContext<CRMContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});

// Agrega una instancia de la clase ProductDAL como un servicio para la inyecci�n de dependencias.
builder.Services.AddScoped<ProductDAL>();

// Construye la aplicaci�n web.
var app = builder.Build();

// Agrega los puntos finales relacionados con los productos a la aplicaci�n.
app.AddProductEndpoints(); // Aseg�rate de que el m�todo se llame correctamente

// Verifica si la aplicaci�n se est� ejecutando en un entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    // Habilita el uso de Swagger para la documentaci�n de la API.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Agrega middleware para redirigir las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Ejecuta la aplicaci�n.
app.Run();
