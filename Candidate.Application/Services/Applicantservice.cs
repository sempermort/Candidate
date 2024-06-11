using Candidate.Application.Dtos;
using Candidate.Domain;

namespace Candidate.Application.Services;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _applicantRepository;

    public ApplicantService(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }

    public async Task<IEnumerable<ApplicantDto>> GetApplicantsAsync()
    {
        var applicant = await _applicantRepository.GetAllAsync();
        return applicant.Select(t => new ApplicantDto
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            PhoneNumber = t.PhoneNumber,
            Email = t.Email,
            LinkedInProfileUrl = t.LinkedInProfileUrl,
            GitHubProfileUrl = t.GitHubProfileUrl,
            Comment = t.Comment,
            FromDtm = t.ToDtm,
            ToDtm = t.ToDtm
        });
    }

    public async Task<ApplicantDto> GetApplicantByEmailAsync(string email)
    {
        var applicant = await _applicantRepository.GetByEmailAsync(email);
        if (applicant == null)
        {
            return null;
        }
        return new ApplicantDto
        {
            FirstName = applicant.FirstName,
            LastName = applicant.LastName,
            PhoneNumber = applicant.PhoneNumber,
            Email = applicant.Email,
            LinkedInProfileUrl = applicant.LinkedInProfileUrl,
            GitHubProfileUrl = applicant.GitHubProfileUrl,
            Comment = applicant.Comment,
            FromDtm = applicant.ToDtm,
            ToDtm = applicant.ToDtm
        };
    }

    public async Task CreateapplicantAsync(ApplicantDto applicant)
    {
        var prospect = new Prospect(
            applicant.FirstName,
            applicant.LastName,
            applicant.PhoneNumber,
            applicant.Email,
            applicant.LinkedInProfileUrl,
            applicant.GitHubProfileUrl,
            applicant.Comment,
            applicant.ToDtm,
            applicant.ToDtm
        );
        await _applicantRepository.AddAsync(prospect);
    }

    public async Task UpdateApplicantAsync(ApplicantDto applicantDto)
    {

        var applicant = await _applicantRepository.GetByEmailAsync(applicantDto.Email);
        if (applicant != null)
        {
            applicant.UpdateProspect(applicantDto.FirstName, applicant.LastName, applicantDto.PhoneNumber, applicantDto.Email,
            applicantDto.LinkedInProfileUrl, applicantDto.GitHubProfileUrl, applicantDto.Comment, applicantDto.FromDtm, applicant.ToDtm);

            await _applicantRepository.UpdateAsync(applicant);
        }
    }

    public async Task DeleteApplicantAsync(string email)
    {
        var applicant = await _applicantRepository.GetByEmailAsync(email);
        if (applicant != null)
        {
            await _applicantRepository.DeleteAsync(applicant);
        }
    }
}



