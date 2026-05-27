using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.ValueObjects;
using BookRight.UseCases.CreateTreatmentType;
using BookRight.UseCases.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCase.Test
{
    public class CreateTreatmentTypeUseCaseTests
    {
        private readonly Mock<ITreatmentTypeRepository> _mockRepository;
        private readonly CreateTreatmentTypeUseCase _sut;

        public CreateTreatmentTypeUseCaseTests()
        {
            _mockRepository = new Mock<ITreatmentTypeRepository>();
            _sut = new CreateTreatmentTypeUseCase(
                _mockRepository.Object);
        }

        private TreatmentType CreateTreatmentType() => new TreatmentType(
            "Test Behandling",
            30,
            1,
            new Money(300),
            true,
            "Massør"
            );

       /* [Fact]
        public async Task ExecuteAsync_Valid_CreatesTreatment_AndReturnsId()
        {
            //Arrange
            var treatmentId = Guid.NewGuid();
            _mockRepository
                .Setup(r => r.AddAsync(It)
        }*/
    }
}
