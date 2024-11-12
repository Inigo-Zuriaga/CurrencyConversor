using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IApiService, ApiService>();
builder.Services.AddScoped<UserService>();

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
});

// Configuracion de CORS para permitir solicitudes desde tu frontend Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Cambia esto a la URL de tu frontend
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddControllers();

// Configuracion de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:CadenaConexion"]);
});


// Configuracion de base de datos


var app = builder.Build();

// Configura Entity Framework y ASP.NET Core Identity
// builder.Services.AddDbContext<DbContexto>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura Identity, DbContext, etc.
// builder.Services.AddDefaultIdentity<IdentityUser>()
//     .AddEntityFrameworkStores<DbContexto>()
//     .AddDefaultTokenProviders();

// Otras configuraciones necesarias (controladores y vistas)
// builder.Services.AddControllersWithViews();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// Configuracion del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la politica de CORS antes de autorizacion y controladores
app.UseCors("AllowOrigin");

// Configura las rutas y middlewares
app.UseAuthentication();  // Para permitir la autenticaci�n
app.UseAuthorization();   // Para permitir la autorizaci�n

Env.Load();

app.MapControllers();


app.Run();
