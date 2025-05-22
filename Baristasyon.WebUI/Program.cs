var builder = WebApplication.CreateBuilder(args);

// ✅ HttpClient tanımı (API çağrıları için)
builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7030/api/"); // kendi API portun
});

// ✅ Session desteği
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ MVC + Razor View
builder.Services.AddControllersWithViews();

// ✅ AutoMapper (MappingProfile sınıfını kullanacak şekilde)
builder.Services.AddAutoMapper(typeof(Baristasyon.Application.MappingProfile.MappingProfile));


var app = builder.Build();


// ✅ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Session middleware mutlaka burada olmalı
app.UseSession();



app.UseAuthorization();

// ✅ Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
