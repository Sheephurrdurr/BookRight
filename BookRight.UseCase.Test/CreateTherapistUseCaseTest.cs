using BookRight.Domain.Aggregates.TherapistAggregate;
using BookRight.Facade.DTOs.CreateTherapistDTOs;
using BookRight.UseCases.Interfaces;
using BookRight.UseCases.TherapistUC.CreateTherapist;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCase.Test
{
    //For at kunne teste, at en Therapist bliver oprettet rigtig, eller fejl,
    // istedenfor, at måtte kontakte en database, for at kunne teste dette, laver man en mock
    // med en "dummy therapist", i dette tilfeldet, for at teste med.. måske :,) 

    public class CreateTherapistUseCaseTest
    {
        private readonly Mock<ITherapistRepository> _mockTherapistRepository = new(); //Falsk copy af vores therapist repo

        private CreateTherapistUseCase CreateSut() => new(_mockTherapistRepository.Object); //SUT = System Under Test,
                                                                                            //sådan at man ikke behæver at endre
                                                                                            //masse kode om det kommer flere afhengigheder                                                                                           
                                                                                            //senere, kun denne
        
        [Fact]
        public async Task ExecuteAsync_ValidRequest_ReturnsTherapistId()
        {
            // Arrange
            var mockRepo = new Mock<ITherapistRepository>();

            // Make mockRepo able to mock ExistsByEmailAsync()
            mockRepo.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Make mockRepo able to mock AddAsync()
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Therapist>()))
                .Returns(Task.CompletedTask);

            var request = new CreateTherapistRequest
            {
                FirstName = "Test",
                LastName = "Therapist",
                Email = "test@therapist.dk",
                Specialization = "Fysioterapeut",
                ClinicId = Guid.NewGuid(),
            };

            //SUT = System Under Test,
            var sut = new CreateTherapistUseCase(mockRepo.Object);

            // Act
            var response = await sut.ExecuteAsync(request);

            // Assert
            Assert.NotEqual(Guid.Empty, response.TherapistId);
        }
    }
}
