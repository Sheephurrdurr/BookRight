using BookRight.Domain.Entities;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateTherapistDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CreateTherapist
{
    // Welcome, welcome. Here we have the use case layer. 
    // This place holds the implementation (the actual logic/code) from the interfaces in Facade. We'll get to those.
    // Why are the interfaces not just here, along with the rest of the use case stuff? Because facade is the contact point between the UI and our program. More on that later.

    public class CreateTherapistUseCase : ICreateTherapistUseCase
    {
        private readonly ITherapistRepository _repository;

        public CreateTherapistUseCase(ITherapistRepository repository)
        {
            _repository = repository; // First we do some constructor dependency injection
        }

        public async Task<CreateTherapistResponse> ExecuteAsync(CreateTherapistRequest request)
        {
            var alreadyExists = await _repository.ExistsByEmailAsync(request.Email);

            if (alreadyExists)
                throw new InvalidOperationException($"A therapist with email '{request.Email}' already exists."); // Ved ikke lige hvorfor den samme person skulle blive oprettet som medarbejder...

            // Now we fill the entity with the data from the DTO, called CreateTherapistRequest (in the method's parameter).
            // When the user interacts with the UI, the data from the interaction gets packaged into a DTO, that gets sent here. 
            // Here the DTO gets "repackaged" into an entity from our Domain, that then gets sent to the database via the repository.
            // AND FOR WHAT REASON?! Because then our UI doesnt have to know about Domain and yada yada. I'm gonna stop talking now. 
            var therapist = new Therapist(
                new FullName(request.FirstName, request.LastName),
                new Email(request.Email),
                request.Specialization
            );

            await _repository.AddAsync(therapist); // ADDASYNC!! Look! Look! This is where our use case calls the repository, whiiich is in the Infrastructure layer.

            return new CreateTherapistResponse(therapist.Id); // Response DTO is just here to send back a confirmation. The DTO contains the ID of the thing that has been changed,
                                                              // so u can send back; {id} has changed! Or something to that effect.
        }
    }
}
