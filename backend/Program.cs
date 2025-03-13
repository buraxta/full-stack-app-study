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
    
    // PostgreSQL bağlantısı için yeniden deneme mekanizması
    var maxRetryCount = 5;
    var retryIntervalSeconds = 10;
    var currentRetry = 0;
    
    while (currentRetry < maxRetryCount)
    {
        try
        {
            Console.WriteLine($"Veritabanı bağlantısı deneniyor... Deneme: {currentRetry + 1}");
            
            var context = services.GetRequiredService<AppDbContext>();
            
            // Önce bağlantıyı test et
            context.Database.OpenConnection();
            context.Database.CloseConnection();
            
            // Bağlantı başarılıysa migrasyonu uygula
            context.Database.Migrate();
            
            Console.WriteLine("Veritabanı migration başarılı.");
            break;
        }
        catch (Exception ex)
        {
            currentRetry++;
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, $"Veritabanı migration hatası. Kalan deneme: {maxRetryCount - currentRetry}");
            
            if (currentRetry < maxRetryCount)
            {
                Console.WriteLine($"{retryIntervalSeconds} saniye sonra tekrar denenecek...");
                Thread.Sleep(retryIntervalSeconds * 1000);
            }
            else
            {
                Console.WriteLine("Maksimum deneme sayısına ulaşıldı. Veritabanı migrasyonu başarısız!");
            }
        }
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
