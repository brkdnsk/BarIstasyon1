using Baristasyon.Application.Interfaces.Services;
using Baristasyon.Application.MappingProfile;
using Baristasyon.Persistence.Contexts;
using Baristasyon.Persistence.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ✅ DbContext servisibuilder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddDbContext<BaristasyonDbContext>(options =>
    options.UseNpgsql(connectionString));

// ✅ Service registration (BU KISIM builder.Build()'DAN ÖNCE OLMALI)
builder.Services.AddScoped<ICoffeeRecipeService, CoffeeRecipeService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IFavoriteRecipeService, FavoriteRecipeService>();
builder.Services.AddScoped<IJobPostService, JobPostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRatingService, RatingService>();





// ✅ Controller ve Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔨 Artık Build zamanı
var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
