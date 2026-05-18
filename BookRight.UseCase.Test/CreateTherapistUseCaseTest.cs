using BookRight.UseCases.CreateTherapist;
using BookRight.UseCases.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
        public void PlaceHolder()
        {
            Assert.True(true);
        }

    }
}
