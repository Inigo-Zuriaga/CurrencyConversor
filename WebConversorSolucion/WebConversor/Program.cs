
var builder = WebApplication.CreateBuilder(args);

//1- Add services to the container.
builder.Services.AddControllersWithViews();

///PRUEBA******************
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Cambia esto a la URL de tu frontend
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


///******************

/*Creamos el contexto de la base de datos*/
builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:CadenaConexion"]);
});

var app = builder.Build();

//2- Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//Comentario
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors("AllowOrigin");
app.Run();


