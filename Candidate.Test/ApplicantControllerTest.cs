using AutoMapper;
using Candidate.Application.Dtos;
using Candidate.Application.Services;
using Candidate.Domain;
using FakeItEasy;
using FluentAssertions;
using Moq;

namespace Candidate.Test
{
    public class ApplicantServiceTest
    {
            private readonly Mock<IApplicantRepository> _applicantRepositoryMock;
            private readonly ApplicantService _applicantService;
        private readonly IMapper _mapper;
        private Prospect applicant;

        public ApplicantServiceTest()
            {
            _applicantRepositoryMock = new Mock<IApplicantRepository>();
            _applicantService = new ApplicantService(_applicantRepositoryMock.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            applicant = new Prospect()
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
        }

            [Fact]
            public async Task GetapplicantsAsync_ShouldReturnAllapplicants()
            {
                // Arrange
                var applicants = new List<Prospect>
            {
                new Prospect()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Florence",
                    LastName = "McMonies",
                    Email = "fmcmonies0@ycombinator.com",
                    PhoneNumber = "364-508-8955",
                    LinkedInProfileUrl = "https://senate.gov/sit/amet/sapien/dignissim.json?libero=nec&non=molestie&mattis=sed&pulvinar=justo&nulla=pellentesque&pede=viverra&ullamcorper=pede&augue=ac&a=diam&suscipit=cras&nulla=pellentesque&elit=volutpat&ac=dui&nulla=maecenas&sed=tristique&vel=est&enim=et&sit=tempus&amet=semper&nunc=est&viverra=quam&dapibus=pharetra&nulla=magna&suscipit=ac&ligula=consequat&in=metus&lacus=sapien&curabitur=ut&at=nunc&ipsum=vestibulum&ac=ante&tellus=ipsum&semper=primis&interdum=in&mauris=faucibus",
                    GitHubProfileUrl = "https://blogger.com/sit/amet/diam/in/magna/bibendum/imperdiet.jpg?mauris=blandit&non=mi&ligula=in&pellentesque=porttitor&ultrices=pede&phasellus=justo&id=eu&sapien=massa&in=donec&sapien=dapibus&iaculis=duis&congue=at&vivamus=velit&metus=eu&arcu=est&adipiscing=congue&molestie=elementum&hendrerit=in&at=hac&vulputate=habitasse&vitae=platea&nisl=dictumst&aenean=morbi&lectus=vestibulum&pellentesque=velit&eget=id&nunc=pretium&donec=iaculis&quis=diam&orci=erat&eget=fermentum",
                    Comment = "Donec vitae nisi.",
                    FromDtm = new DateTime(2024, 11, 15, 16, 43, 31),
                    ToDtm = new DateTime(2024, 11, 15, 19, 43, 31)
                },
                new Prospect()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Cornell",
                    LastName = "Brinsford",
                    Email = "cbrinsford2@hubpages.com",
                    PhoneNumber = "854-489-8894",
                    LinkedInProfileUrl = "http://moonfruit.com/odio/porttitor/id/consequat.aspx?lacinia=nisi&eget=venenatis&tincidunt=tristique&eget=fusce&tempus=congue&vel=diam&pede=id&morbi=ornare&porttitor=imperdiet&lorem=sapien&id=urna&ligula=pretium&suspendisse=nisl&ornare=ut&consequat=volutpat&lectus=sapien&in=arcu&est=sed&risus=augue&auctor=aliquam&sed=erat&tristique=volutpat&in=in&tempus=congue&sit=etiam&amet=justo&sem=etiam&fusce=pretium&consequat=iaculis&nulla=justo&nisl=in&nunc=hac&nisl=habitasse&duis=platea&bibendum=dictumst&felis=etiam&sed=faucibus&interdum=cursus&venenatis=urna&turpis=ut&enim=tellus&blandit=nulla&mi=ut&in=erat&porttitor=id&pede=mauris&justo=vulputate&eu=elementum&massa=nullam&donec=varius&dapibus=nulla&duis=facilisi&at=cras&velit=non&eu=velit&est=nec&congue=nisi",
                    GitHubProfileUrl = "http://t.co/vel/nisl/duis/ac/nibh/fusce.xml?nulla=mattis&suspendisse=pulvinar&potenti=nulla&cras=pede&in=ullamcorper&purus=augue&eu=a&magna=suscipit&vulputate=nulla&luctus=elit&cum=ac&sociis=nulla&natoque=sed&penatibus=vel&et=enim&magnis=sit&dis=amet&parturient=nunc&montes=viverra&nascetur=dapibus&ridiculus=nulla&mus=suscipit&vivamus=ligula&vestibulum=in&sagittis=lacus&sapien=curabitur&cum=at&sociis=ipsum&natoque=ac&penatibus=tellus&et=semper&magnis=interdum&dis=mauris&parturient=ullamcorper&montes=purus&nascetur=sit&ridiculus=amet&mus=nulla&etiam=quisque&vel=arcu&augue=libero&vestibulum=rutrum&rutrum=ac",
                    Comment = "Ut at dolor quis odio consequat varius.",
                    FromDtm = new DateTime(2024, 11, 15, 15, 43, 31),
                    ToDtm = new DateTime(2024, 11, 15, 17, 43, 31)
                },
                new Prospect()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mychal",
                    LastName = "Goretti",
                    Email = "mgoretti3@pagesperso-orange.fr",
                    PhoneNumber = "571-177-1585",
                    LinkedInProfileUrl = "https://wordpress.com/amet/nunc/viverra/dapibus/nulla.jsp?duis=est",
                    GitHubProfileUrl = "https://cisco.com/duis/faucibus/accumsan/odio/curabitur/convallis/duis.jsp?integer=non&non=interdum&velit=in&donec=ante&diam=vestibulum&neque=ante&vestibulum=ipsum&eget=primis&vulputate=in&ut=faucibus&ultrices=orci&vel=luctus&augue=et&vestibulum=ultrices&ante=posuere&ipsum=cubilia&primis=curae&in=duis&faucibus=faucibus&orci=accumsan&luctus=odio&et=curabitur&ultrices=convallis&posuere=duis&cubilia=consequat&curae=dui&donec=nec&pharetra=nisi&magna=volutpat&vestibulum=eleifend&aliquet=donec&ultrices=ut&erat=dolor&tortor=morbi&sollicitudin=vel&mi=lectus&sit=in&amet=quam&lobortis=fringilla&sapien=rhoncus&sapien=mauris&non=enim&mi=leo&integer=rhoncus&ac=sed&neque=vestibulum&duis=sit&bibendum=amet&morbi=cursus&non=id&quam=turpis&nec=integer&dui=aliquet&luctus=massa&rutrum=id&nulla=lobortis&tellus=convallis&in=tortor&sagittis=risus&dui=dapibus&vel=augue&nisl=vel&duis=accumsan&ac=tellus&nibh=nisi&fusce=eu&lacus=orci&purus=mauris&aliquet=lacinia&at=sapien&feugiat=quis&non=libero&pretium=nullam&quis=sit",
                    Comment = "Mauris lacinia sapien quis libero.",
                    FromDtm = new DateTime(2024, 11, 14, 16, 43, 31),
                    ToDtm = new DateTime(2024, 11, 15, 18, 43, 31)
                },
                new Prospect()
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

                }
            };

                _applicantRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(applicants);

                // Act
                var result = await _applicantService.GetApplicantsAsync();

                // Assert
                result.Should().HaveCount(2);
                result.Should().Contain(t => t.FirstName == "Mcmonies");
                result.Should().Contain(t => t.FirstName == "Cornell");
            }

        [Fact]
        public async Task AddapplicantAsync_ShouldAddapplicant()
        {
            // Arrange
            var appli = applicant;

            // Act
            await _applicantService.CreateapplicantAsync(_mapper.Map<ApplicantDto>(appli));

            // Assert
            _applicantRepositoryMock.Verify(repo => repo.AddAsync(_mapper.Map<Prospect>(appli)), Moq.Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateapplicant()
        {
            // Arrange
            var applicanti = applicant;

            _applicantRepositoryMock.Setup(repo => repo.GetByEmailAsync(applicanti.Email)).ReturnsAsync(applicanti);

            // Act
            await _applicantService.UpdateApplicantAsync( _mapper.Map<ApplicantDto>(applicanti));

            // Assert
            applicanti.FirstName.Should().Be("Bianka");
            _applicantRepositoryMock.Verify(repo => repo.UpdateAsync(applicanti), Moq.Times.Once);
        }

        [Fact]
        public async Task UpdateapplicantAsync_ShouldThrowNotFoundException_WhenapplicantNotFound()
        {
            // Arrange
            _applicantRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Prospect)null);

            // Act
            Func<Task> act = async () => await _applicantService.UpdateApplicantAsync(_mapper.Map<ApplicantDto>(applicant));

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("applicant item with ID 1 not found.");
            _applicantRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Prospect>()), Moq.Times.Never);
        }


        [Fact]
        public async Task DeleteapplicantAsync_ShouldDeleteapplicant()
        {
            // Arrange
            var applicanti = applicant;
            _applicantRepositoryMock.Setup(repo => repo.GetByEmailAsync(applicanti.Email)).ReturnsAsync(applicant);

            // Act
            await _applicantService.DeleteApplicantAsync(applicanti.Email);

            // Assert
            _applicantRepositoryMock.Verify(repo => repo.DeleteAsync(applicant), Moq.Times.Once);
        }

        [Fact]
        public async Task DeleteapplicantAsync_ShouldThrowNotFoundException_WhenapplicantNotFound()
        {
            // Arrange
            _applicantRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Prospect)null);

            // Act
            Func<Task> act = async () => await _applicantService.DeleteApplicantAsync("@me.me");

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("applicant item with ID 1 not found.");
            _applicantRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Prospect>()), Moq.Times.Never);
        }



    }
}

