using BookRight.Facade.DTOs.UpdateTherapistDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.UpdateTherapist
{
    public class UpdateTherapistUseCase : IUpdateTherapistUseCase
    {
        private readonly ITherapistRepository _therapistRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        public UpdateTherapistUseCase(
            ITherapistRepository therapistRepository,
            ITreatmentTypeRepository treatmentTypeRepository)
        {
            _therapistRepository = therapistRepository;
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        public async Task<UpdateTherapistResponse> ExecuteAsync(UpdateTherapistRequest request)
        {
            // Update logic comes next
            throw new NotImplementedException();
        }
    }
}