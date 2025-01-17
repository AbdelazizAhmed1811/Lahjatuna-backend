using LahjatunaAPI.Data;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Models;
using LahjatunaAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyOrigin();
                              });
        });

        builder.Services.AddDbContext<LahjatunaDbContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 10;
        }).AddEntityFrameworkStores<LahjatunaDbContext>()
        .AddDefaultTokenProviders();


        builder.Services.AddAuthentication(options =>
        {

            options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8
                    .GetBytes(builder.Configuration["JWT:SigningKey"]!)
                ),
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });
        builder.Services.AddAuthorization();

        builder.Services.AddControllers();
            //.AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Lahjatuna", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
        });


        // Configure Email Settings
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

        builder.Services.AddTransient<IEmailService, EmailService>();

        builder.Services.AddSingleton<EmailBackgroundService>();
        builder.Services.AddHostedService(provider => provider.GetRequiredService<EmailBackgroundService>());

        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services.AddScoped<ILanguageService, LanguageService>();

        builder.Services.AddScoped<ITranslationLogService, TranslationLogService>();

        builder.Services.AddScoped<ITranslationModelService, TranslationModelService>();

        builder.Services.AddHttpClient<TranslationModelService>();

        builder.Services.AddScoped<IFavoriteService, FavoriteService>();

        builder.Services.AddScoped<IFeedbackService, FeedbackService>();

        builder.Services.AddScoped<IUserService, UserService>();



        var app = builder.Build();

        app.UseRouting();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();


        app.UseCors(MyAllowSpecificOrigins);

        app.MapControllers();


        app.Run();
    }
}