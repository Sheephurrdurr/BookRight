using BookRight.Facade.DTOs.GetAllTherapistsDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetallTherapists
{
    public class GetAllTherapistsUseCase : IGetAllTherapistsUseCase
    {
        private readonly ITherapistRepository _repository;

        public GetAllTherapistsUseCase(ITherapistRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetAllTherapistsResponse>> ExecuteAsync()
        {
            var therapists = await _repository.GetAllAsync();

            return therapists.Select(t => new GetAllTherapistsResponse(
                t.Id,
                t.Name.FirstName,
                t.Name.LastName,
                t.Email.Value,
                t.Specialization
            )).ToList();
        }
    }
}
