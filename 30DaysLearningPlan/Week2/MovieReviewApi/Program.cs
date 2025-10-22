using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieReviewApi.Data;
using Microsoft.OpenApi.Models; // ✅ Required for Swagger

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------
// 🧩 1️⃣ Add services to the container
// ----------------------------------------------------------

// ✅ Add EF Core (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add Controllers with Newtonsoft.Json for JSON Patch support
builder.Services.AddControllers().AddNewtonsoftJson();

// ✅ Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MovieReview API",
        Version = "v1"
    });
});

// ----------------------------------------------------------
// 🧩 2️⃣ Build the app
// ----------------------------------------------------------
var app = builder.Build();

// ----------------------------------------------------------
// 🧩 3️⃣ Configure the HTTP request pipeline
// ----------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieReview API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ Map all controllers (like MoviesController)
app.MapControllers();

// ----------------------------------------------------------
// 🧩 4️⃣ Run the app
// ----------------------------------------------------------
app.Run();