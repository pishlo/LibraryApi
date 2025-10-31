using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ApiProject.Data;
using ApiProject.Services;
using ApiProject.Middleware;

var builder = WebApplication.CreateBuilder(args);

// -----------------------
// Connection string
// -----------------------
// Try environment variable first (from Render)
var connectionString = Environment.GetEnvironmentVariable("NEON_DB_CONNECTION")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("No database connection string configured.");

// Database
builder.Services.AddDbContext<Library_AppContext>(options =>
    options.UseNpgsql(connectionString));

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<Library_AppContext>();

// Controllers & Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API",
        Version = "v1",
        Description = "API for managing books, authors, members, and borrow records."
    });
});

// Services
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<BorrowRecordService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();

// -----------------------
// CORS
// Allow local dev + deployed frontend
// -----------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",                  // local dev
                "https://YOUR_NETLIFY_FRONTEND_URL"      // replace with your Netlify URL
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// -----------------------
// Middleware
// -----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Global exception handling should be before authorization
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
