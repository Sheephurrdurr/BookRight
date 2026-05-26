using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.UseCases.Interfaces;
using BookRight.UseCases.UpdateTreatmentType;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCase.Test
{
    public class UpdateTreatmentTypeUseCaseTests
    {
        private readonly Mock<ITreatmentTypeRepository> _mockRepository;
        private readonly UpdateTreatmentTypeUseCase _sut;

        public UpdateTreatmentTypeUseCaseTests()
        {
            _mockRepository = new Mock<ITreatmentTypeRepository>();
            _sut = new UpdateTreatmentTypeUseCase(_mockRepository.Object);
        }



    }
}

