using BookRight.Domain.Entities;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BookRight.Domain.Test
{
    //Herinde tester du dine metoder, act arrange assert, 
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
