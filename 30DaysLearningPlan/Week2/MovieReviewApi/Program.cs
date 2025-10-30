using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieReviewApi.Data;
using MovieReviewApi.Services;
using Microsoft.OpenApi.Models;
using MovieReviewApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// =============================================================
// 1️⃣ Add services to the container
// =============================================================

// Add DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MovieService for DI
builder.Services.AddScoped<IMovieService, MovieService>();

// Add controllers + Newtonsoft JSON for PATCH support
builder.Services.AddControllers().AddNewtonsoftJson();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieReview API", Version = "v1" });
});

var app = builder.Build();

// =============================================================
// 2️⃣ Configure the HTTP request pipeline
// =============================================================

// Swagger (development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieReview API V1"));
}

// =============================================================
// 🧩 Global Exception Middleware (MUST come before other middleware)
// =============================================================
app.UseMiddleware<GlobalExceptionMiddleware>();

// =============================================================
// Remaining middleware pipeline
// =============================================================
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();