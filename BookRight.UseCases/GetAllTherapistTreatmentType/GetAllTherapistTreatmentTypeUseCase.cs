using BookRight.UseCases.Interfaces;
using BookRight.Facade.DTOs.GetAllTherapistTreatmentTypesDTOs;
using BookRight.Facade.Interfaces.TherapistUseCases;
namespace BookRight.UseCases.GetAllTherapistTreatmentType
{
    public class GetAllTherapistTreatmentTypeUseCase : IGetAllTherapistTreatmentTypesUseCase
    {
        private readonly ITherapistRepository _therapistRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        public GetAllTherapistTreatmentTypeUseCase(ITherapistRepository therapistRepository, ITreatmentTypeRepository treatmentTypeRepository)
        {
            _therapistRepository = therapistRepository;
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        // Metode bruger dictonary for at optimere opslag af behandlingstyper, og håndterer tilfælde hvor en behandlingstype ikke findes
        // Alterantivt kunne man lave et database kald per qualification (N1+1 problem)
        public async Task<IReadOnlyList<GetAllTherapistTreatmentTypesResponse>> ExecuteAsync()
        {
            var therapists = await _therapistRepository.GetAllAsync();
            var treatmentTypes = await _treatmentTypeRepository.GetAllAsync();

            var treatmentTypeDict = treatmentTypes.ToDictionary(t => t.Id);

            var result = new List<GetAllTherapistTreatmentTypesResponse>();

            foreach (var therapist in therapists)
            {
                foreach (var qualification in therapist.Qualifications)
                {
                    string treatmentTypeName;

                    if (treatmentTypeDict.TryGetValue(qualification.TreatmentTypeId, out var treatmentType))
                    {
                        treatmentTypeName = treatmentType.Name;
                    }
                    else
                    {
                        treatmentTypeName = "Ukendt behandlingstype";
                    }

                    result.Add(new GetAllTherapistTreatmentTypesResponse(
                        qualification.Id,
                        therapist.Id,
                        $"{therapist.Name.FirstName} {therapist.Name.LastName}",
                        qualification.TreatmentTypeId,
                        treatmentTypeName,
                        qualification.BasePrice
                    ));



                }
                
            }

            return result;
        }
    }
}
