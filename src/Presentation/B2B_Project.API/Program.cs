using B2B_Project.Application;
using B2B_Project.Domain.Identity;
using B2B_Project.Infrastructure;
using B2B_Project.Persistance;
using B2B_Project.Persistance.Context;
using B2B_Project.Persistance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 1;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<B2B_ProjectDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

            NameClaimType = ClaimTypes.Name
        };

        //signalr test
        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/chathub")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };

    });


//swagger authorization
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseStaticFiles();



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.Run();
