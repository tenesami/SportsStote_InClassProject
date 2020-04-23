using CSC237_tatomsa_InClassProject.DataLayer;
using CSC237_tatomsa_InClassProject.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsPro.Tests
{
    public class CheckClassTests
    {
        [Fact]
        public void EmailExit_ReturnsAstring()
        {
            //Arrange 
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup( m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());

            //Act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            //Assert
            Assert.IsType<string>(result);
        }
        /// <summary>
        /// ///////////////
        /// </summary>
        [Fact]
        public void EmailExists_ReturnsAnEmptyStringIfEmailIsNew()
        {
            //arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m =>m.Get(It.IsAny<QueryOptions<Customer>>()));
            int expectedLength = 0;

            //act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            //assert
            Assert.Equal(expectedLength, result.Length);
        }

        [Fact]
        public void EmailExists_ReturnsAnEmptyStringIfEmailIsMissing()
        {
            //arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m =>m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());
            int expectedLength = 0;

            //act
            var result = Check.EmailExists(rep.Object, null);

            //Assert
            Assert.Equal(expectedLength, result.Length);
        }
        [Fact]
        public void EmailExists_ReturnsAnErrorMessageIfEmailExists()
        {
            //arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());
            bool isGraterThanZero = true;

            //act
            var result = Check.EmailExists(rep.Object, "fack@email.com");

            //Assert
            Assert.Equal(isGraterThanZero, result.Length > 0);
        }
    }
}
