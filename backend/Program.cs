using Microsoft.EntityFrameworkCore;
using backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.RateLimiting;

var corsPolicy = "_myPolicy";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//Dependency Injections
builder.Services.AddScoped<DatabaseRepository>();
builder.Services.AddScoped<GenerateSignedUrl>();
builder.Services.AddScoped<FrontendActions>();
builder.Services.AddScoped<HttpClient>();

var issuer = builder.Configuration["Api:ValidIssuer"];
// Rate Limiting Configuration
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions
        .AddSlidingWindowLimiter(policyName: "public_rate", options =>
        {
            options.PermitLimit = 50;
            options.Window = TimeSpan.FromMinutes(1);
            options.SegmentsPerWindow = 2;
        })
        .AddSlidingWindowLimiter(policyName: "authenticated_rate", options =>
        {
            options.PermitLimit = 100;
            options.Window = TimeSpan.FromMinutes(1);
            options.SegmentsPerWindow = 2;
        })
        .AddSlidingWindowLimiter(policyName: "storage_rate", options =>
        {
            options.PermitLimit = 25;
            options.Window = TimeSpan.FromMinutes(1);
            options.SegmentsPerWindow = 2;
        });

    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtOptions =>
{
    
    jwtOptions.Authority = issuer;

    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = issuer
    };
});

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, corsPolicy =>
    {
        //Compound assignment, if allowedOrigins null then make it [""] else keep it as is
        if (allowedOrigins == null || allowedOrigins.Length == 0)
        {
            allowedOrigins = ["http://localhost:5174"];
        }

        corsPolicy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider
        .GetRequiredService<ILogger<Program>>();

    try
    {
        var db = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        if (db.Database.CanConnect())
        {
            logger.LogInformation("Database connection successful");
            logger.LogInformation("Valid Issuer: " + issuer);
        }
        else
        {
            logger.LogWarning("Database connection failed");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database startup connection failure");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Site successfully loaded").RequireRateLimiting("public_rate");
app.Run();
