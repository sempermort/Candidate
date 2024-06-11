using Candidate.Application.Dtos;

namespace Candidate.Application.Services;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantDto>> GetApplicantsAsync();
    Task<ApplicantDto> GetApplicantByEmailAsync(string email);
    Task CreateapplicantAsync(ApplicantDto applicantDto);
    Task UpdateApplicantAsync(ApplicantDto applicantDto);
    Task DeleteApplicantAsync(string email);
}
