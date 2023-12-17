using AvanceDelProyecto.Configurations;
using AvanceDelProyecto.Services;
using Microsoft.Extensions.Configuration;
using AvanceDelProyecto.Configurations;
using AvanceDelProyecto.Services;

var builder = WebApplication.CreateBuilder(args);

// Obtener la configuración del builder
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configura y registra ApiSettings en el contenedor de servicios usando la sección 'ApiSettings' del archivo de configuración.
builder.Services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

// Agrega el servicio HttpClient al contenedor. Esto permite inyectar y usar HttpClient en cualquier parte de la aplicación.
builder.Services.AddHttpClient();

// Registra el servicio ApiService como 'Scoped', lo que significa que se creará una instancia por cada solicitud. 
builder.Services.AddScoped<IApiService, ApiService>();//Iyectamos nuestra interaz con nuestra clase

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configura la ruta predeterminada para que apunte a la acción 'Login' en el controlador 'Home'.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
