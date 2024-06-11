using Candidate.Application.Dtos;
using Candidate.Application.Services;
using Candidate.Infrastructure.Data;
using Candidate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddControllers();
            builder.Services.AddScoped<ApplicantValidator>();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

            builder.Services.AddScoped<IApplicantService, ApplicantService>();
            builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
        }

        var app = builder.Build();
        {
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
 
}