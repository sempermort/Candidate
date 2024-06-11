using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Candidate.Domain
{

    public class Prospect
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LinkedInProfileUrl { get; set; }
        public string GitHubProfileUrl { get; set; }
        public string Comment { get; set; }
        public DateTime FromDtm { get; set; }
        public DateTime ToDtm { get; set; }
        public Guid Id { get; set; }

        public Prospect() { }
        public Prospect(string firstName,
                        string lastName,
                        string phoneNumber,
                        string email,
                        string linkedInProfileUrl,
                        string gitHubProfileUrl,
                        string comment,
                        DateTime fromDtm,
                        DateTime toDtm)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            LinkedInProfileUrl = linkedInProfileUrl;
            GitHubProfileUrl = gitHubProfileUrl;
            Comment = comment;
            FromDtm = fromDtm;
            ToDtm = toDtm;
        }


        public void UpdateProspect(
            Guid id,
            string firstName,
                        string lastName,
                        string phoneNumber,
                        string email,
                        string linkedInProfileUrl,
                        string gitHubProfileUrl,
                        string comment,
                        DateTime fromDtm,
                        DateTime toDtm)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            LinkedInProfileUrl = linkedInProfileUrl;
            GitHubProfileUrl = gitHubProfileUrl;
            Comment = comment;
            FromDtm = fromDtm;
            ToDtm = toDtm;
        }
    }
}
