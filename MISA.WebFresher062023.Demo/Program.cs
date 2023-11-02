using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;
using MISA.WebFresher062023.Demo.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MISA.WebFresher062023.Demo;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors);
            var errorMessages = errors.Select(error => error.ErrorMessage).ToArray();
            var errorResponse = new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = 400,
                UserMessage = string.Join("! ", errorMessages),
                DevMessage = string.Join("! ", errorMessages),
                TraceId = "",
                MoreInfo = "",
                Errors = errors
            });

            return errorResponse;
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/Fobidden";

});
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]))
    };
});

var connectionString = config["ConnectionString"];



builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IAssetCategoryService, AssetCategoryService>();
builder.Services.AddScoped<IAssetCategoryRepository, AssetCategoryRepository>();

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetManager, AssetManager>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIncreaseManger, IncreaseManager>();

builder.Services.AddScoped<IIncreaseAssetRepository, IncreaseAssetRepository>();
builder.Services.AddScoped<IIncreaseAssetService, IncreaseAssetService>();

builder.Services.AddScoped<IIncreaseRepository, IncreaseRepository>();
builder.Services.AddScoped<IIncreaseService, IncreaseService>();

builder.Services.AddScoped<ISourceRepository, SourceRepository>();
builder.Services.AddScoped<ISourceService, SourceService>();

builder.Services.AddScoped<IAutoCodeService, AutoCodeService>();
builder.Services.AddScoped<IAutoCodeRepository, AutoCodeRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.UseMiddleware<ExceptionMiddleware>();


app.Run();
