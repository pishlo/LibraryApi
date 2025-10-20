// Program.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Services;
using ApiProject.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Read connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register DbContext using the correct class name
builder.Services.AddDbContext<Library_AppContext>(options =>
    options.UseNpgsql(connectionString));

// Register Identity services using the same DbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<Library_AppContext>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<BorrowRecordService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();



var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();