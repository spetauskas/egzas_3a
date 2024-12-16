
using egzas_3.Repositories.Interfaces;
using egzas_3.Repositories;
using egzas_3.Services;
using egzas_3.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using egzas_3.Mapper.Interfaces;
using egzas_3.Mapper;
using Microsoft.Extensions.DependencyInjection;




namespace egzas_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();
            builder.Services.AddTransient<IAccountService, AccountService>();

            builder.Services.AddTransient<IJwtService, JwtService>();

            //builder.Services.AddTransient<IAccountMapper, AccountMapper>();

            //builder.Services.AddTransient<AuthService>();
            //builder.Services.ConfigureBusinessLayerServices();
            // builder.Services.ConfigureDataLayerServices(builder.Configuration);



            // JWT Authentication configuration


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(builder.Configuration.GetConnectionString("EgzaminasDataBase")));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secretKey = builder.Configuration.GetSection("Jwt:Key").Value!; //retrieves the secret key used to sign the tokens from the application's configuration, such as an appsettings.json file. This key is crucial for the security of the JWTs, as it's used to validate the signature of incoming tokens.
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = key //sets the key used to validate the token's signature.
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

            app.UseCors(builder => //This method is called to add the CORS middleware to the application's request processing pipeline. 
            {
                builder
                .AllowAnyOrigin() //This method call configures the CORS policy to allow requests from any origin. In a production environment, it's generally recommended to be more specific about which origins are allowed to ensure the security of your web application.
                .AllowAnyMethod() //This allows the CORS policy to accept requests made with any HTTP method (such as GET, POST, PUT, DELETE, etc.). This is useful for a RESTful API that needs to support a wide range of actions on resources.
                .AllowAnyHeader(); //his configures the CORS policy to allow any headers in the requests. Headers are often used in requests to carry information about the content type, authentication, etc. Allowing any header supports a wide range of requests that might include custom or standard headers.
            }); //This configuration is very permissive, allowing any web application to make requests to your ASP.NET Core Web API regardless of the origin, HTTP method, or headers used in the request. While this setup is useful for development or when you need to allow wide access to your API, it's important to tighten the CORS policy for production environments to minimize security risks. You would typically do this by specifying allowed origins, methods, and headers that match the requirements of your specific client applications.

            app.UseAuthentication(); // Add this line before UseAuthorization
            app.UseAuthorization();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
