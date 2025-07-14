
using Giant_Techie_BE.Database;
using Giant_Techie_BE.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Giant_Techie_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<GiantTicheDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            builder.Services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICompetitionService, CompetitionService>();
            builder.Services.AddScoped<IUserService, UserService>();


            var app = builder.Build();

            var serviceScope = app.Services.CreateAsyncScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<GiantTicheDbContext>();
            dbContext.Database.EnsureCreatedAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");


            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
