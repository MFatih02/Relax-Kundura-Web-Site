using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using RelaxKundura3.Abstract;
using RelaxKundura3.Data;
using RelaxKundura3.Extentions;
using RelaxKundura3.Services;


// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddSession();

builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IKategoriService, KategoriService>();
builder.Services.AddScoped<ISepetService, SepetService>();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

var app = builder.Build();
//using(var scope = app.Services.CreateScope())
//{
//    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.UseEndpoints
    (endpoints =>
    {
		endpoints.MapControllerRoute(
	name: "default",
	pattern: "{controller=Kullanici}/{action=Index}/{id?}");
		endpoints.MapControllerRoute(
	  name: "Admin",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);

	});
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Kullanici}/{action=Index}/{id?}");
//app.UseEndpoints(endpoints =>
//{
//	endpoints.MapControllerRoute(
//	  name: "areas",
//	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//	);
//});
app.MapRazorPages();

app.Run();
