using Candidate.Domain;

namespace Candidate.Application.Services
{
    public interface IApplicantRepository
    {
        Task<IEnumerable<Prospect>> GetAllAsync();
        Task<Prospect> GetByEmailAsync(string email);
        Task AddAsync(Prospect prospect);
        Task UpdateAsync(Prospect prospect);
        Task DeleteAsync(Prospect prospect);
    }
}


