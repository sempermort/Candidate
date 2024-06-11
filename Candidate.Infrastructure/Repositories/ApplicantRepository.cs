using Candidate.Application.Services;
using Candidate.Domain;

namespace Candidate.Infrastructure.Repositories
{
    public class ApplicantRepository:IApplicantRepository
    {
        private readonly List<Prospect> _pros = new();

        public Task<IEnumerable<Prospect>> GetAllAsync() => Task.FromResult(_pros.AsEnumerable());

        public Task<Prospect> GetByEmailAsync(string email) => Task.FromResult(_pros.FirstOrDefault(t => t.Email == email));

        public Task AddAsync(Prospect prospect)
        {
            _pros.Add(prospect);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Prospect prospect)
        {
            var existing = _pros.FirstOrDefault(t => t.Email == prospect.Email);
            if (existing != null)
            {
                existing.UpdateProspect(prospect.FirstName,prospect.LastName,prospect.PhoneNumber,prospect.Email,prospect.LinkedInProfileUrl,
                    prospect.GitHubProfileUrl,prospect.Comment,prospect.FromDtm,prospect.ToDtm);
                
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Prospect prospect)
        {
            _pros.Remove(prospect);
            return Task.CompletedTask;
        }
    }
}

