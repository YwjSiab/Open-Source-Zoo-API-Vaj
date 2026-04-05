using Microsoft.EntityFrameworkCore;
using NTC_Zoo_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger (API testing UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Entity Framework (SQL Server)
builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZooConnection")));

// Allow Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Optional — you can remove this if the warning annoys you
app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

// THIS IS CRITICAL (your 404 issue if missing)
app.MapControllers();

// 🔥 JSON → Database import happens here
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ZooContext>();
    var environment = services.GetRequiredService<IWebHostEnvironment>();

    // Create DB + apply migrations
    context.Database.Migrate();

    // Import animals.json into DB
    await JsonAnimalSeeder.SeedAsync(context, environment);
}

app.Run();