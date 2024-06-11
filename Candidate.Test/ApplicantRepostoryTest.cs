using Candidate.Domain;
using Candidate.Infrastructure.Data;
using Candidate.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Text;

namespace Candidate.Test
{
    public class ApplicantRepostoryTest:IntegrationTestBase
    {
        private readonly HttpClient _client;

        public ApplicantRepostoryTest(WebApplicationFactory<Program> factory) : base(factory)
            {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task CreateApplicant_ShouldAddApplicantToDatabase()
        {
            // Arrange
            var Applicant = new Prospect()
            {
                Id = Guid.NewGuid(),
                FirstName = "Biahinka",
                LastName = "Birchett",
                Email = "bbirchegytt4@tuttocitta.it",
                PhoneNumber = "973-443-2795",
                LinkedInProfileUrl = "http://storify.com/orci/luctus/et/ultrices/posuere.jsp?et=tortor&ultrices=id&posuere=nulla&cubilia=ultrices&curae=aliquet&nulla=maecenas&dapibus=leo&dolor=odio&vel=condimentum&est=id&donec=luctus&odio=nec&justo=molestie&sollicitudin=sed&ut=justo&suscipit=pellentesque&a=viverra&feugiat=pede&et=ac&eros=diam&vestibulum=cras&ac=pellentesque&est=volutpat&lacinia=dui&nisi=maecenas&venenatis=tristique&tristique=est&fusce=et&congue=tempus&diam=semper&id=est&ornare=quam&imperdiet=pharetra&sapien=magna&urna=ac&pretium=consequat&nisl=metus&ut=sapien&volutpat=ut&sapien=nunc&arcu=vestibulum&sed=ante&augue=ipsum&aliquam=primis&erat=in&volutpat=faucibus&in=orci&congue=luctus",
                GitHubProfileUrl = "https://simplemachines.org/mauris/laoreet/ut.xml?nunc=mauris&purus=ullamcorper&phasellus=purus&in=sit&felis=amet&donec=nulla&semper=quisque&sapien=arcu&a=libero",
                Comment = "Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.",
                FromDtm = new DateTime(2024, 11, 15, 13, 43, 31),
                ToDtm = new DateTime(2024, 11, 15, 17, 43, 31)
            };
            var content = new StringContent(JsonConvert.SerializeObject(Applicant), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Applicants", content);
            response.EnsureSuccessStatusCode();

            // Assert
            var Applicants = await DbContext.Prospects.ToListAsync();
            Applicants.Should().HaveCount(1);
            Applicants[0].FirstName.Should().Be("New Task");
        }

        [Fact]
        public async Task UpdateApplicant_ShouldModifyApplicantInDatabase()
        {
            // Arrange
            var Applicant = new Prospect()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bianka",
                LastName = "Birchett",
                Email = "bbirchett4@tuttocitta.it",
                PhoneNumber = "973-443-2795",
                LinkedInProfileUrl = "http://storify.com/orci/luctus/et/ultrices/posuere.jsp?et=tortor&ultrices=id&posuere=nulla&cubilia=ultrices&curae=aliquet&nulla=maecenas&dapibus=leo&dolor=odio&vel=condimentum&est=id&donec=luctus&odio=nec&justo=molestie&sollicitudin=sed&ut=justo&suscipit=pellentesque&a=viverra&feugiat=pede&et=ac&eros=diam&vestibulum=cras&ac=pellentesque&est=volutpat&lacinia=dui&nisi=maecenas&venenatis=tristique&tristique=est&fusce=et&congue=tempus&diam=semper&id=est&ornare=quam&imperdiet=pharetra&sapien=magna&urna=ac&pretium=consequat&nisl=metus&ut=sapien&volutpat=ut&sapien=nunc&arcu=vestibulum&sed=ante&augue=ipsum&aliquam=primis&erat=in&volutpat=faucibus&in=orci&congue=luctus",
                GitHubProfileUrl = "https://simplemachines.org/mauris/laoreet/ut.xml?nunc=mauris&purus=ullamcorper&phasellus=purus&in=sit&felis=amet&donec=nulla&semper=quisque&sapien=arcu&a=libero",
                Comment = "Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.",
                FromDtm = new DateTime(2024, 11, 15, 13, 43, 31),
                ToDtm = new DateTime(2024, 11, 15, 17, 43, 31)

            };
            DbContext.Prospects.Add(Applicant);
            await DbContext.SaveChangesAsync();

            var updateApplicant = new { FirstName = "Bianka" };
            var content = new StringContent(JsonConvert.SerializeObject(updateApplicant), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/Applicants/{Applicant.Email}", content);
            response.EnsureSuccessStatusCode();

            // Assert
            var updatedApplicant = await DbContext.Prospects.FindAsync(Applicant.Id);
            updatedApplicant.FirstName.Should().Be("Updated Task");
        }
    }
    }

