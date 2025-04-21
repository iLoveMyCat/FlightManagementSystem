
using FlightManagement.AlertApi.Configurations;
using FlightManagement.AlertApi.Repositories;
using FlightManagement.AlertApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FlightManagement.AlertApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Config
            builder.Services.Configure<DbSettings>(
                builder.Configuration.GetSection(nameof(DbSettings)));

            // Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAlertService, AlertService>();
            builder.Services.AddScoped<IAlertRepository, AlertRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

          

            builder.Services.AddAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
