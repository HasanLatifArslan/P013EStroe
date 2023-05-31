using P013EStroe.Data;

using P013EStroe.Service.Abstract;
using P013EStroe.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies; // oturum i�lemleri i�.in 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.LoginPath = "/Admin/Login"; // oturum a�mayan kullan�c�lar�n giri� i�in g�nderilece�i sayfa
	x.LogoutPath = "/Admin/Logout";
	x.AccessDeniedPath = "/AccessDenied"; // yetkilendrime ile ekrana eri�im hakk� olmayan kullan�c�lar�n g�nderilece�i sayfa
	x.Cookie.Name = "Administrator"; // olu�acak kukinin ismi 
	x.Cookie.MaxAge = TimeSpan.FromDays(1); // olu�acak kukinin ya�am s�resi 1 g�n
}); // oturum i�lemeri i�in
builder.Services.AddAuthorization(x =>
{
	x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin"));// admin paneline giri� yapma yetkisine sahip olanlar� bu kuralla kontrol edece�iz
	x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin d���nda yetkilerindrme kullan�rsak bu kural� kullanabiliriz 
});
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // kendi yazd���m�z db i�lemlerini yapan servisi .net core da bu �ekilde mvc projesine servis olarak tan�t�yoruz ki kullanabilelim
builder.Services.AddTransient<IProductService, ProductService>(); // product i�in yazd���m�z �zel servisi uygulamaya tan�tt�k
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // dikkat �nce usauthenticitaion sat�r� gelmeli sonra useautorization olmal�
app.UseAuthorization();
app.MapControllerRoute(
			name: "admin",
			pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
		  );
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
