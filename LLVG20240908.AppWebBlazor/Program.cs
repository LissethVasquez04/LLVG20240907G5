using LLVG20240907.API.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ProductLLVGService>();
builder.Services.AddScoped<ProductLLVGService>();


builder.Services.AddHttpClient("API", c =>
{
    // Configura la dirección base del cliente HTTP desde la configuración
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);
    // Puedes configurar otras opciones del HttpClient aquí según sea necesario
});



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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
