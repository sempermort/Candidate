using FluentAssertions;
using Moq;
using Candidate.Application.Dtos;
using Candidate.Application.Services;
using Candidate.Domain;

namespace Candidate.Tests.UnitTest
{
    public class ApplicantServiceTests
    {
        private readonly Mock<IApplicantRepository> _applicantRepositoryMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly ApplicantService _applicantService;

        public ApplicantServiceTests()
        {
            _applicantRepositoryMock = new Mock<IApplicantRepository>();
            _cacheServiceMock = new Mock<ICacheService>();
            _applicantService = new ApplicantService(_applicantRepositoryMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task GetApplicantsAsync_ShouldReturnApplicants_FromCache()
        {
            // Arrange
            var cachedApplicants = new List<ApplicantDto>
            {
                new ApplicantDto { Id = new Guid(), FirstName = "John", LastName = "Doe" }
            };
            _cacheServiceMock.Setup(c => c.Get<IEnumerable<ApplicantDto>>("applicants")).Returns(cachedApplicants);

            // Act
            var result = await _applicantService.GetApplicantsAsync();

            // Assert
            result.Should().BeEquivalentTo(cachedApplicants);
            _applicantRepositoryMock.Verify(r => r.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async Task GetApplicantsAsync_ShouldReturnApplicants_FromRepository_WhenCacheIsEmpty()
        {
            // Arrange
            _cacheServiceMock.Setup(c => c.Get<IEnumerable<ApplicantDto>>("applicants")).Returns((IEnumerable<ApplicantDto>)null);
            var applicants = new List<Prospect>
            {
                new Prospect { Id =new Guid(), FirstName = "John", LastName = "Doe" }
            };
            _applicantRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(applicants);

            // Act
            var result = await _applicantService.GetApplicantsAsync();

            // Assert
            result.Should().HaveCount(1);
            _cacheServiceMock.Verify(c => c.Set("applicants", It.IsAny<IEnumerable<ApplicantDto>>(), It.IsAny<TimeSpan>()), Times.Once);
        }

        [Fact]
        public async Task GetApplicantByEmailAsync_ShouldReturnApplicant_FromCache()
        {
            // Arrange
            var email = "john.doe@example.com";
            var cachedApplicant = new ApplicantDto { Id = new Guid(), FirstName = "John", LastName = "Doe", Email = email };
            _cacheServiceMock.Setup(c => c.GetAsync<ApplicantDto>($"ap_{email}")).ReturnsAsync(cachedApplicant);

            // Act
            var result = await _applicantService.GetApplicantByEmailAsync(email);

            // Assert
            result.Should().BeEquivalentTo(cachedApplicant);
            _applicantRepositoryMock.Verify(r => r.GetByEmailAsync(email), Times.Never);
        }

        [Fact]
        public async Task GetApplicantByEmailAsync_ShouldReturnApplicant_FromRepository_WhenCacheIsEmpty()
        {
            // Arrange
            var email = "john.doe@example.com";
            _cacheServiceMock.Setup(c => c.GetAsync<ApplicantDto>($"ap_{email}")).ReturnsAsync((ApplicantDto)null);
            var applicant = new Prospect { Id = new Guid(), FirstName = "John", LastName = "Doe", Email = email };
            _applicantRepositoryMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(applicant);

            // Act
            var result = await _applicantService.GetApplicantByEmailAsync(email);

            // Assert
            result.Should().NotBeNull();
            _cacheServiceMock.Verify(c => c.SetAsync($"ap_{email}", It.IsAny<ApplicantDto>(), It.IsAny<TimeSpan>()), Times.Once);
        }

        [Fact]
        public async Task CreateApplicantAsync_ShouldCreateNewApplicant()
        {
            // Arrange
            var applicantDto = new ApplicantDto
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
            };

            // Act
            await _applicantService.CreateapplicantAsync(applicantDto);

            // Assert
            _applicantRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Prospect>()), Times.Once);
            _cacheServiceMock.Verify(c => c.Remove("applicants"), Times.Once);
        }

        [Fact]
        public async Task UpdateApplicantAsync_ShouldUpdateExistingApplicant()
        {
            // Arrange
            var applicantDto = new ApplicantDto
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                LinkedInProfileUrl = "http://linkedin.com/in/johndoe",
                GitHubProfileUrl = "http://github.com/johndoe",
                Comment = "Great candidate",
                FromDtm = DateTime.Now,
                ToDtm = DateTime.Now.AddHours(5)
            };
            var existingApplicant = new Prospect
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            _applicantRepositoryMock.Setup(r => r.GetByEmailAsync(applicantDto.Email)).ReturnsAsync(existingApplicant);

            // Act
            await _applicantService.UpdateApplicantAsync(applicantDto);

            // Assert
            _applicantRepositoryMock.Verify(r => r.UpdateAsync(existingApplicant), Times.Once);
            _cacheServiceMock.Verify(c => c.Remove("applicants"), Times.Once);
            _cacheServiceMock.Verify(c => c.Remove($"ap_{applicantDto.Email}"), Times.Once);
        }

        [Fact]
        public async Task DeleteApplicantAsync_ShouldDeleteExistingApplicant()
        {
            // Arrange
            var email = "john.doe@example.com";
            var existingApplicant = new Prospect { Id = new Guid(), FirstName = "John", LastName = "Doe", Email = email };
            _applicantRepositoryMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(existingApplicant);

            // Act
            await _applicantService.DeleteApplicantAsync(email);

            // Assert
            _applicantRepositoryMock.Verify(r => r.DeleteAsync(existingApplicant), Times.Once);
            _cacheServiceMock.Verify(c => c.Remove("applicants"), Times.Once);
            _cacheServiceMock.Verify(c => c.Remove($"ap_{email}"), Times.Once);
        }
    }
}
