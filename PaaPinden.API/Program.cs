using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaaPinden.API.Contracts;
using PaaPinden.API.Data;
using PaaPinden.API.Persistence;
using Scalar.AspNetCore;

namespace PaaPinden.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services
                .AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IFeedPostRepository, FeedPostRepository>();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapIdentityApi<ApplicationUser>();


            app.MapControllers();

            app.Run();
        }
    }
}
