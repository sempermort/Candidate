using Candidate.Application.Dtos;
using Candidate.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Api
{
    [Route("applicant")]
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
            public async Task<IActionResult> GetApplicants()
            {
                var Applicants = await _applicantService.GetApplicantsAsync();
                return Ok(Applicants);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetApplicantByEmail(string email)
            {
                var Applicant = await _applicantService.GetApplicantByEmailAsync(email);
                if (Applicant == null)
                {
                    return NotFound();
                }
                return Ok(Applicant);
            }

            [HttpPost]
            public async Task<IActionResult> CreateApplicant([FromBody] ApplicantDto ApplicantItemDto)
            {

            var validationResult = await _validations.ValidateAsync(ApplicantItemDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _applicantService.CreateapplicantAsync(ApplicantItemDto);
                return CreatedAtAction(nameof(GetApplicantByEmail), new { Email = ApplicantItemDto.Email }, ApplicantItemDto);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateApplicant(string email, [FromBody] ApplicantDto applicantDto )
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

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteApplicantAsync(string email)
            {
            await _applicantService.DeleteApplicantAsync(email);
                return NoContent();
            }
        }
    }

