using Baristasyon.Application.Interfaces.Services;
using Baristasyon.Application.MappingProfile;
using Baristasyon.Persistence.Contexts;
using Baristasyon.Persistence.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ✅ DbContext
builder.Services.AddDbContext<BaristasyonDbContext>(options =>
    options.UseNpgsql(connectionString));

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ✅ Dependency Injection - Service Registration
builder.Services.AddScoped<ICoffeeRecipeService, CoffeeRecipeService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IFavoriteRecipeService, FavoriteRecipeService>();
builder.Services.AddScoped<IJobPostService, JobPostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRatingService, RatingService>();

// ✅ Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // 🔒 Güvenlik için önemli

app.UseAuthorization();

app.MapControllers();

app.Run();
