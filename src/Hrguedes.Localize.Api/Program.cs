using System.Reflection;
using System.Text;
using System.Text.Json;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Hrguedes.Localize.Api.Common;
using Hrguedes.Localize.Application.DependencyInjection;
using Hrguedes.Localize.Infra.Persistence;
using Hrguedes.Localize.Infra.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.ListenAnyIP(8080);
    serverOptions.AddServerHeader = false;
});

// Add services to the container.
builder.Services.AddApplicationInjection(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


// options
builder.Services.Configure<PaginationOption>(builder.Configuration.GetSection("PaginationOption"));

// cors
const string allowAllOrigins = "_allowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAllOrigins, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


// JWT Config
var key = Encoding.ASCII.GetBytes(Settings.JwtToken);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// swagger
builder.Services
    .AddApiVersioning(p =>
    {
        p.DefaultApiVersion = new ApiVersion(1, 0);
        p.ReportApiVersions = true;
        p.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddApiExplorer(p =>
    {
        p.GroupNameFormat = "'v'VVV";
        p.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "Exemplo: Bearer 12345ASDASD",
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Localize - Client Debits Manager",
        Description = "Clients Debits - Web Service",
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});


// App
var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Apply migrations and seeds
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    DatabaseInitializerSeed.Seed(scope.ServiceProvider);
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        c.RoutePrefix = string.Empty;
    }

    c.DocExpansion(DocExpansion.List);
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(allowAllOrigins);
app.MapControllers();
app.Run();