using Candidate.Application.Services;
using Candidate.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddScoped <IApplicantService, ApplicantService> ();
    builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}