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
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
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
                await _applicantService.CreateapplicantAsync(ApplicantItemDto);
                return CreatedAtAction(nameof(GetApplicantByEmail), new { Email = ApplicantItemDto.Email }, ApplicantItemDto);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateApplicant(string email, [FromBody] ApplicantDto applicantDto )
            {
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

