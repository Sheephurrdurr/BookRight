using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.Therapist;
using BookRight.Facade.DTOs.CreateTherapistDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CreateTherapist
{
    public class CreateTherapistUseCase : ICreateTherapistUseCase
    {
        private readonly ITherapistRepository _repository;

        public CreateTherapistUseCase(ITherapistRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateTherapistResponse> ExecuteAsync(CreateTherapistRequest request)
        {
            var alreadyExists = await _repository.ExistsByEmailAsync(request.Email);

            if (alreadyExists)
                throw new InvalidOperationException($"A therapist with email '{request.Email}' already exists."); // Ved ikke lige hvorfor den samme person skulle blive oprettet som medarbejder...

            var therapist = new Therapist(
                new FullName(request.FirstName, request.LastName),
                new Email(request.Email),
                request.Specialization
            );

            await _repository.AddAsync(therapist); 

            return new CreateTherapistResponse(therapist.Id); 
                                                              
        }
    }
}
