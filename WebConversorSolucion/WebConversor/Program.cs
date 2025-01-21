using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IApiService, ApiService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<HistoryService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddTransient<PdfService>();


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
    options.AddPolicy("AllowAll", builder =>
    {
        //Cambiar esto a la hora de produccion
        // builder.WithOrigins("https://conversor-git-main-inigozuriagas-projects.vercel.app/") // Cambia esto a la URL de tu frontend
        // Cambia esto a la URL de tu frontend
        builder
                //.WithOrigins("http://localhost:4200")
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
    });


// Configuracion de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseSqlServer(
    // Si se quiere trabajar con la base en local descomentar la siguiente linea
         builder.Configuration["ConnectionStrings:AzureConexion"]);
});

builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Configuracion del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la politica de CORS antes de autorizacion y controladores
//app.UseCors("AllowOrigin");
app.UseCors("AllowAll");
// Configura las rutas y middlewares
app.UseHttpsRedirection();
app.UseAuthentication();  // Para permitir la autenticacion
app.UseAuthorization();   // Para permitir la autorizacion

Env.Load();

app.MapControllers();

app.Run();
