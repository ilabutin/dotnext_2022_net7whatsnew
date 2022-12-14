using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

//// Define services

// Configure auth
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// Configure the database
var connectionString = builder.Configuration.GetConnectionString("Todos") ?? "Data Source=Todos.db";
builder.Services.AddSqlite<TodoDbContext>(connectionString);

// Configure Open API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

// Configure rate limiting
builder.Services.AddRateLimiting();

// Configure output cache
builder.Services.AddOutputCache(cacheOptions =>
{
    cacheOptions.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(10)));
    cacheOptions.AddPolicy("Expire30", builder => builder.Expire(TimeSpan.FromSeconds(30)));
});

// Configure controllers
builder.Services.AddControllers();

// Configure OpenTelemetry
builder.AddOpenTelemetry();

//// Define pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();
app.UseRateLimiter();

app.Map("/", () => Results.Redirect("/swagger"));

//// Group APIs
var group = app.MapGroup("/todos");

group.MapTodos()
     .RequireAuthorization(pb => pb.RequireClaim("id"))
     .AddOpenApiSecurityRequirement();

app.MapControllers();

// Configure the prometheus endpoint for scraping metrics
app.MapPrometheusScrapingEndpoint();
// NOTE: This should only be exposed on an internal port!
// .RequireHost("*:9100");

app.Run();
