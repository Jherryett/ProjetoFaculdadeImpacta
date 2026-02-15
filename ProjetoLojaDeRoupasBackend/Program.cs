var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers();

builder.Services.AddScoped<IRoupaRepository, RoupaRepository>();
builder.Services.AddScoped<IRoupaService, RoupaService>();


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();