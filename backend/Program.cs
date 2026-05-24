using Microsoft.EntityFrameworkCore;
using backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var corsPolicy = "_myPolicy";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//Dependency Injections
builder.Services.AddScoped<DatabaseRepository>();
builder.Services.AddScoped<GenerateSignedUrl>();
builder.Services.AddScoped<FrontendActions>();
builder.Services.AddScoped<HttpClient>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtOptions =>
{
    jwtOptions.Authority = builder.Configuration.GetSection("Api:ValidIssuer").Get<string>();

    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration.GetSection("Api:ValidIssuer").Get<string>()
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Site Succefully Loaded");
app.Run();
