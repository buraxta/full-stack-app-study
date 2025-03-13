using backend.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:3000",
                    "http://127.0.0.1:3000",
                    "http://frontend:3000"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
});

var app = builder.Build();

// Veritabanı migrasyonlarını otomatik olarak uygula
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Veritabanı varsa migrate et, yoksa oluştur
        context.Database.Migrate();
        // Seed data eklemek için buraya kod eklenebilir
        Console.WriteLine("Veritabanı migration başarılı.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı migration hatası.");
    }
}

// Configure the HTTP request pipeline.
// CORS policy'nin HttpsRedirection'dan önce uygulanması önemli
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// UseHttpsRedirection'ı Docker ortamında devre dışı bırakmak
// HTTPS/HTTP karışıklığını önler
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
