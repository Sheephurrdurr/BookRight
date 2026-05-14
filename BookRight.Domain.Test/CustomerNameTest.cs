using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Services;

namespace BookRight.Domain.Test
{
    public class CustomerNameTest
    {
        [Fact]
        public void Customer_LastName_Has_WhiteSpace_Throws_ArguementException() //Unhappy path
        {
            //Arrange, Act & Assert
            var exception = Assert.Throws<ArgumentException>
                 (() => new FullName("Cate", " "));
        }
    }
}
