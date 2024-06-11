using Candidate.Application.Dtos;
using Candidate.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Api
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly ApplicantValidator? _validations;
        public ApplicantController(IApplicantService applicantService, ApplicantValidator validation)
        {
            _applicantService = applicantService;
            _validations = validation;
        }    

            [HttpGet]
            public async Task<IActionResult> GetAllApplicantsAsync()
            {
                var Applicants = await _applicantService.GetApplicantsAsync();
                return Ok(Applicants);
            }
        [ActionName("GetCandidateByEmail")]
        [HttpGet("{email}")]
            public async Task<IActionResult> GetCandidateByEmail(string email)
            {
                var Applicant = await _applicantService.GetApplicantByEmailAsync(email);
                if (Applicant == null)
                {
                    return NotFound();
                }
                return Ok(Applicant);
            }

            [HttpPost]
            public async Task<IActionResult> CreateApplicantAsync([FromBody] ApplicantDto ApplicantItemDto)
            {

            var validationResult = await _validations.ValidateAsync(ApplicantItemDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _applicantService.CreateapplicantAsync(ApplicantItemDto);
                return  RedirectToAction(nameof(GetCandidateByEmail), new { ApplicantItemDto.Email});
            }

           
           [HttpPut("{email}")]
            public async Task<IActionResult> UpdateApplicantAsync(string email, [FromBody] ApplicantDto applicantDto )
            {
            var validationResult = await _validations.ValidateAsync(applicantDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (email != applicantDto.Email)
                {
                    return BadRequest();
                }

                await _applicantService.UpdateApplicantAsync(applicantDto);
                return NoContent();
            }

            [HttpDelete("{email}")]
            public async Task<IActionResult> DeleteApplicantAsync(string email)
            {
            await _applicantService.DeleteApplicantAsync(email);
                return NoContent();
            }
        }
    }

