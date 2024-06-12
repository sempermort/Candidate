using Microsoft.EntityFrameworkCore;
using Candidate.Domain;
using Candidate.Infrastructure.Data;
using Candidate.Infrastructure.Repositories;
using FluentAssertions;

namespace Candidate.Tests.IntegrationTest
{
    public class ApplicantRepositoryTest
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public ApplicantRepositoryTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"InMemoryDb-{Guid.NewGuid()}")
                .Options;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProspects()
        {
            // Arrange
            using var context = new AppDbContext(_dbContextOptions);
            context.Prospects.AddRange(
                new Prospect
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "123-456-7890",
                    LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                    GitHubProfileUrl = "http://github.com/johndoe",
                    Comment = "Great candidate",
                    FromDtm = DateTime.Now,
                    ToDtm = DateTime.Now.AddHours(5)
                },
                new Prospect
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                PhoneNumber = "123-456-7890",
                    Email = "jane.doe@example.com",
                    LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                    GitHubProfileUrl = "http://github.com/johndoe",
                    Comment = "Great candidate",
                    FromDtm = DateTime.Now,
                    ToDtm = DateTime.Now.AddHours(5)
                }
            );
            await context.SaveChangesAsync();

            var repository = new ApplicantRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnProspect_WhenEmailExists()
        {
            // Arrange
            var email = "john.doe@example.com";
            using var context = new AppDbContext(_dbContextOptions);
            context.Prospects.Add(new Prospect
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                Email = email,
                LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                GitHubProfileUrl = "http://github.com/johndoe",
                Comment = "Great candidate",
                FromDtm = DateTime.Now,
                ToDtm = DateTime.Now.AddHours(5)
            });
            await context.SaveChangesAsync();

            var repository = new ApplicantRepository(context);

            // Act
            var result = await repository.GetByEmailAsync(email);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(email);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            // Arrange
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ApplicantRepository(context);

            // Act
            var result = await repository.GetByEmailAsync("nonexistent@example.com");

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_ShouldAddProspect()
        {
            // Arrange
            var prospect = new Prospect
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                GitHubProfileUrl = "http://github.com/johndoe",
                Comment = "Great candidate",
                FromDtm = DateTime.Now,
                ToDtm = DateTime.Now.AddYears(1)
            };
            using var context = new AppDbContext(_dbContextOptions);
            var repository = new ApplicantRepository(context);

            // Act
            await repository.AddAsync(prospect);

            // Assert
            context.Prospects.Should().ContainSingle(p => p.Email == prospect.Email);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingProspect()
        {
            // Arrange
            var email = "john.doe@example.com";
            using var context = new AppDbContext(_dbContextOptions);
            var prospect = new Prospect
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                Email = email,
                LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                GitHubProfileUrl = "http://github.com/johndoe",
                Comment = "Great candidate",
                FromDtm = DateTime.Now,
                ToDtm = DateTime.Now.AddHours(5)
            };
            context.Prospects.Add(prospect);
            await context.SaveChangesAsync();

            var updatedProspect = new Prospect
            {
                Id = prospect.Id,
                FirstName = "Johnny",
                LastName = "Doe",
                Email = email,
                PhoneNumber = "123-456-7890",
                LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                GitHubProfileUrl = "http://github.com/johndoe",
                Comment = "Updated candidate",
                FromDtm = DateTime.Now,
                ToDtm = DateTime.Now.AddYears(1)
            };

            var repository = new ApplicantRepository(context);

            // Act
            await repository.UpdateAsync(updatedProspect);

            // Assert
            var result = context.Prospects.FirstOrDefault(p => p.Email == email);
            result.FirstName.Should().Be("Johnny");
            result.Comment.Should().Be("Updated candidate");
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteExistingProspect()
        {
            // Arrange
            var email = "john.doe@example.com";
            using var context = new AppDbContext(_dbContextOptions);
            var prospect = new Prospect
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                Email = email,
                LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                GitHubProfileUrl = "http://github.com/johndoe",
                Comment = "Great candidate",
                FromDtm = DateTime.Now,
                ToDtm = DateTime.Now.AddHours(5)
            };
            context.Prospects.Add(prospect);
            await context.SaveChangesAsync();

            var repository = new ApplicantRepository(context);

            // Act
            await repository.DeleteAsync(prospect);

            // Assert
            context.Prospects.Should().NotContain(p => p.Email == email);
        }
    }
}
