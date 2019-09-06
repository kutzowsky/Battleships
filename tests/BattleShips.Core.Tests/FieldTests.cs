using Battleships.Core;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using FluentAssertions;
using System;
using Xunit;

namespace BattleShips.Core.Tests
{
    public class FieldTests
    {
        IField _field;

        public FieldTests()
        {
            _field = new Field();
        }

        [Fact]
        public void Field_WhenInitialized_ShouldHaveEmptyState()
        {
            _field.State.Should().Be(FieldState.EMPTY);
        }

        [Fact]
        public void Field_WhenInitialized_ShouldHaveEmptyShipId()
        {
            _field.ShipId.Should().BeEmpty();
        }

        [Fact]
        public void PlaceShip_WhenCalled_ShouldChangeFieldStateToShip()
        {
            _field.PlaceShip(Guid.NewGuid());

            _field.State.Should().Be(FieldState.SHIP);
        }

        [Fact]
        public void PlaceShip_WhenCalled_ShouldSetProperShipId()
        {
            var shipId = Guid.NewGuid();

            _field.PlaceShip(shipId);

            _field.ShipId.Should().Be(shipId);
        }


        [Theory]
        [InlineData(FieldState.EMPTY, FieldState.MISS)]
        [InlineData(FieldState.SHIP, FieldState.HIT)]
        public void Shoot_WhenCalled_ShouldReturnNewFieldState(FieldState initialState, FieldState expectedState)
        {
            var field = new Field(initialState);

            var returnedState = field.Shoot();

            returnedState.Should().Be(expectedState);
        }

        [Theory]
        [InlineData(FieldState.EMPTY, FieldState.MISS)]
        [InlineData(FieldState.SHIP, FieldState.HIT)]
        public void Shoot_WhenCalled_ShouldProperlyChangeFieldState(FieldState initialState, FieldState expectedState)
        {
            var field = new Field(initialState);

            var returnedState = field.Shoot();

            field.State.Should().Be(expectedState);
        }

        [Theory]
        [InlineData(FieldState.EMPTY)]
        [InlineData(FieldState.SHIP)]
        public void Shoot_WhenCalledOnAnAlreatyShotField_ShouldThrowInvalidOperationException(FieldState initialState)
        {
            var field = new Field(initialState);

            field.Shoot();

            Action act = () => field.Shoot();

            act.Should().Throw<InvalidOperationException>();
        }

    }
}
