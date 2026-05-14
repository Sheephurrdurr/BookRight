using BookRight.Facade.DTOs.GetAllTherapistsDTOs;

namespace BookRight.Facade.Interfaces
{
    public interface IGetAllTherapistsUseCase
    {
        Task<IReadOnlyList<GetAllTherapistsResponse>> ExecuteAsync();
    }
}
