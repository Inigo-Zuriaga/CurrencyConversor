using Npgsql.EntityFrameworkCore.PostgreSQL;
var builder = WebApplication.CreateBuilder(args);

//1- Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:CadenaConexion"]);
});
//builder.Services.AddDbContext<DbContexto>(options => {
//    options.UseNpgsql(
//        builder.Configuration["ConnectionStrings:CadenaConexion"]);
//});

var app = builder.Build();

//2- Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
