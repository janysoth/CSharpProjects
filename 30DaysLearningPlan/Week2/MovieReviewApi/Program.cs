var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // ✅ enables controller support
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ maps all controller routes (e.g., /api/movies)
app.MapControllers();

app.Run();