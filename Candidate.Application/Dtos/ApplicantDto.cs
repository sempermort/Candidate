namespace Candidate.Application.Dtos
{
    public class ApplicantDto
    {   public Guid Id { get; set; }
        public string FirstName { get;  set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get;  set; }
        public string LinkedInProfileUrl { get; set; }
        public string GitHubProfileUrl { get; set; }
        public string Comment { get; set; }
        public DateTime FromDtm { get; set; }
        public DateTime ToDtm { get; set; }

    }
}
