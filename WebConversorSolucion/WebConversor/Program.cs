using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
// using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Debug()
//     .WriteTo.Console()
//     .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();


// Agregar servicio de Serilog
// builder.Logging.AddSerilog();
// Add services to the container.
builder.Services.AddHttpClient<IApiService, ApiService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<HistoryService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// Configuracion de JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated: " + context.SecurityToken);
            return Task.CompletedTask;
        }
    };
});

// Configuracion de CORS para permitir solicitudes desde tu frontend Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        //Cambiar esto a la hora de produccion
        builder.WithOrigins("http://localhost:4200") // Cambia esto a la URL de tu frontend
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
    });

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Configuracion de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseSqlServer(
        //builder.Configuration["ConnectionStrings:AzureConexion"]);
    
    // Si se quiere trabajar con la base en local descomentar la siguiente linea
         builder.Configuration["ConnectionStrings:CadenaConexion"]);
});

var app = builder.Build();

// Configuracion del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Exports")),
    // Path.Combine(Directory.GetCurrentDirectory(), "Exports")),
    // Path.Combine(Directory.GetCurrentDirectory(), "Files")),
    RequestPath = "/Files"
});

// Aplica la politica de CORS antes de autorizacion y controladores
app.UseCors("AllowOrigin");

// Configura las rutas y middlewares
app.UseAuthentication();  // Para permitir la autenticaci�n
app.UseAuthorization();   // Para permitir la autorizaci�n

Env.Load();

app.MapControllers();

app.Run();
