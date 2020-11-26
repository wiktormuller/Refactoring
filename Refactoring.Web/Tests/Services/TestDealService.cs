using FluentAssertions;
using Refactoring.Web.Services;
using System;
using Xunit;

namespace Refactoring.Web.Tests.Services
{
    public class TestDealService
    {
        [Fact]
        public void Given_MorningDateTime_GenerateDeal_Returns_AmRate()
        {
            //Arrange
            var systemUnderTest = new DealService();
            var morningTime = new DateTime(2020, 10, 10, 10, 10, 10);
            var expectedResult = DealService.AmRate;

            //Act
            var generatedDealRate = systemUnderTest.GenerateDeal(morningTime);

            //Assert
            generatedDealRate.Should().Be(expectedResult);
        }

        [Fact]
        public void Given_EveningDateTime_GenerateDeal_Returns_PmRate()
        {
            //Arrange
            var systemUnderTest = new DealService();
            var eveningTime = new DateTime(2020, 10, 10, 20, 10, 10);
            var expectedResult = DealService.PmRate;

            //Act
            var generatedDealRate = systemUnderTest.GenerateDeal(eveningTime);

            //Assert
            generatedDealRate.Should().Be(expectedResult);
        }
    }
}
