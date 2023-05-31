using P013EStroe.Data;

using P013EStroe.Service.Abstract;
using P013EStroe.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies; // oturum iþlemleri iç.in 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.LoginPath = "/Admin/Login"; // oturum açmayan kullanýcýlarýn giriþ için gönderileceði sayfa
	x.LogoutPath = "/Admin/Logout";
	x.AccessDeniedPath = "/AccessDenied"; // yetkilendrime ile ekrana eriþim hakký olmayan kullanýcýlarýn gönderileceði sayfa
	x.Cookie.Name = "Administrator"; // oluþacak kukinin ismi 
	x.Cookie.MaxAge = TimeSpan.FromDays(1); // oluþacak kukinin yaþam süresi 1 gün
}); // oturum iþlemeri için
builder.Services.AddAuthorization(x =>
{
	x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin"));// admin paneline giriþ yapma yetkisine sahip olanlarý bu kuralla kontrol edeceðiz
	x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin dýþýnda yetkilerindrme kullanýrsak bu kuralý kullanabiliriz 
});
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // kendi yazdýðýmýz db iþlemlerini yapan servisi .net core da bu þekilde mvc projesine servis olarak tanýtýyoruz ki kullanabilelim
builder.Services.AddTransient<IProductService, ProductService>(); // product için yazdýðýmýz özel servisi uygulamaya tanýttýk
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
app.UseAuthentication(); // dikkat önce usauthenticitaion satýrý gelmeli sonra useautorization olmalý
app.UseAuthorization();
app.MapControllerRoute(
			name: "admin",
			pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
		  );
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
