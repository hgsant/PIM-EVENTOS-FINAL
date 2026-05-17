using EventosPRO.Web.Data;
using EventosPRO.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CONEXÃO BANCO
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

// POSTGRESQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// IDENTITY
builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
    })
    .AddEntityFrameworkStores<AppDbContext>();

// MVC
builder.Services.AddControllersWithViews();

// IA
builder.Services.AddScoped<IAService>();

var app = builder.Build();

// ERRO
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// LOGIN
app.UseAuthentication();

app.UseAuthorization();

// ROTAS
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Eventos}/{action=Index}/{id?}");

// ROTAS IDENTITY
app.MapRazorPages();

app.Run();