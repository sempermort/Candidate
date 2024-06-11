using Candidate.Application.Services;
using Candidate.Domain;
using Candidate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Infrastructure.Repositories
{
    public class ApplicantRepository:IApplicantRepository
    {
        private  AppDbContext _prosContext;

        public ApplicantRepository() { }
        public ApplicantRepository(AppDbContext prosContext) 
        {
            _prosContext = prosContext;        
        }
        public async Task<IEnumerable<Prospect>> GetAllAsync() => await await  Task.FromResult(_prosContext.Prospects.ToListAsync());

        public Task<Prospect> GetByEmailAsync(string email)
        {
            return Task.FromResult(_prosContext.Prospects.FirstOrDefault(t => t.Email == email));
        }

        public async Task AddAsync(Prospect prospect)
        {
            prospect.Id = Guid.NewGuid();
            _prosContext.Prospects.Add(prospect);
            await _prosContext.SaveChangesAsync();         
        }

        public Task UpdateAsync(Prospect prospect)
        {
            var existing = _prosContext.Prospects.FirstOrDefault(t => t.Email == prospect.Email);
            if (existing != null)
            {
                existing.UpdateProspect(prospect.Id,prospect.FirstName,prospect.LastName,prospect.PhoneNumber,prospect.Email,prospect.LinkedInProfileUrl,
                    prospect.GitHubProfileUrl,prospect.Comment,prospect.FromDtm,prospect.ToDtm);
                _prosContext.Update<Prospect>(existing);
                _prosContext.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Prospect prospect)
        {
            _prosContext.Prospects.Remove(prospect);
            _prosContext.SaveChanges(true);
            return Task.CompletedTask;
        }
    }
}

