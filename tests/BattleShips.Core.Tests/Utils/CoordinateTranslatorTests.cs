using Battleships.Core.Interfaces;
using Battleships.Core.Utils;
using FluentAssertions;
using System;
using System.Drawing;
using Xunit;

namespace BattleShips.Core.Tests.Utils
{
    public class CoordinateTranslatorTests
    {
        ICoordinateTranslator _coordinateTranslator;

        public CoordinateTranslatorTests()
        {
            _coordinateTranslator = new CoordinateTranslator();
        }

        [Theory]
        [InlineData("A1", 0, 0)]
        [InlineData("a2", 0, 1)]
        [InlineData("B3", 1, 2)]
        [InlineData("e6", 4, 5)]
        [InlineData("J10", 9, 9)]
        public void GetBoardCoordsFrom_WhenCalledWithValidCoordinates_ShouldReturnCorrectPoint(string textCoords, int expectedX, int expectedY)
        {
            var expectedPoint = new Point(expectedX, expectedY);

            var point = _coordinateTranslator.GetBoardCoordsFrom(textCoords);

            point.Should().Be(expectedPoint);
        }

        [Theory]
        [InlineData("")]
        [InlineData("A11")]
        [InlineData("Z1")]
        [InlineData("A")]
        [InlineData("1")]
        [InlineData("A-1")]
        [InlineData("1A")]
        [InlineData("aa")]
        [InlineData("I don't know what to put there")]
        [InlineData("@#$%^&^$%$@$%&***&&^^%%!!!")]
        public void GetBoardCoordsFrom_WhenCalledWithInvalidCoordinates_ShouldThrowArgumentException(string textCoords)
        {
            Action act = () => _coordinateTranslator.GetBoardCoordsFrom(textCoords);

            act.Should().Throw<ArgumentException>();

        }
    }
}
