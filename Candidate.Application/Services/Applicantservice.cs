using Candidate.Application.Dtos;
using Candidate.Domain;

namespace Candidate.Application.Services;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ICacheService _cacheService;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(50);
    private IApplicantRepository @object;

    public ApplicantService(IApplicantRepository applicantRepository, ICacheService cacheService)
    {
        _applicantRepository = applicantRepository;
        _cacheService = cacheService;
    }

    public ApplicantService(IApplicantRepository @object)
    {
        this.@object = @object;
    }

    public async Task<IEnumerable<ApplicantDto>> GetApplicantsAsync()
    {
        var cacheKey = "applicants";
        var items = _cacheService.Get<IEnumerable<ApplicantDto>>(cacheKey);

        if (items == null)
        {
            var applicant = await _applicantRepository.GetAllAsync();
            items = applicant.Select(t => new ApplicantDto
            {Id=t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                PhoneNumber = t.PhoneNumber,
                Email = t.Email,
                LinkedInProfileUrl = t.LinkedInProfileUrl,
                GitHubProfileUrl = t.GitHubProfileUrl,
                Comment = t.Comment,
                FromDtm = t.ToDtm,
                ToDtm = t.ToDtm
            }).ToList();

            _cacheService.Set(cacheKey, items, _cacheExpiration);
        }
            return items;
    }

    public async Task<ApplicantDto> GetApplicantByEmailAsync(string email)
    {
        var cacheKey = $"ap_{email}";
        var item  = await _cacheService.GetAsync<ApplicantDto>(cacheKey);

        if (item == null)
        {
            var applicant = await _applicantRepository.GetByEmailAsync(email);
            if (applicant == null)
            {
                return null;
            }

            item = new ApplicantDto
            {Id=applicant.Id,
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
            await _cacheService.SetAsync(cacheKey, item, _cacheExpiration);
        }
        return item;
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
            _cacheService.Remove("applicants");
    }

    public async Task UpdateApplicantAsync(ApplicantDto applicantDto)
    {

        var applicant = await _applicantRepository.GetByEmailAsync(applicantDto.Email);
        if (applicant != null)
        {
            applicant.UpdateProspect(applicantDto.Id,applicantDto.FirstName, applicantDto.LastName, applicantDto.PhoneNumber, applicantDto.Email,
            applicantDto.LinkedInProfileUrl, applicantDto.GitHubProfileUrl, applicantDto.Comment, applicantDto.FromDtm, applicant.ToDtm);

            await _applicantRepository.UpdateAsync(applicant);

            _cacheService.Remove("applicants");
            _cacheService.Remove($"ap_{applicantDto.Email}");
        }
    }

    public async Task DeleteApplicantAsync(string email)
    {
        var applicant = await _applicantRepository.GetByEmailAsync(email);
        if (applicant != null)
        {
            await _applicantRepository.DeleteAsync(applicant);

            _cacheService.Remove("applicants");
            _cacheService.Remove($"ap_{email}");
        }
    }
}



