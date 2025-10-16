using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------
// 🧩 1️⃣ Add services to the container
// ----------------------------------------------------------

// Optional: Swagger/OpenAPI (if you used AddOpenApi earlier)
builder.Services.AddOpenApi();

// ✅ Add Controllers with Newtonsoft.Json for JSON Patch support
builder.Services.AddControllers().AddNewtonsoftJson();

// ----------------------------------------------------------
// 🧩 2️⃣ Build the app
// ----------------------------------------------------------
var app = builder.Build();

// ----------------------------------------------------------
// 🧩 3️⃣ Configure the HTTP request pipeline
// ----------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Enables Swagger/OpenAPI during development
}

// Optional HTTPS redirection
app.UseHttpsRedirection();

// Map all controllers (like MoviesController)
app.MapControllers();

// ----------------------------------------------------------
// 🧩 4️⃣ Run the app
// ----------------------------------------------------------
app.Run();