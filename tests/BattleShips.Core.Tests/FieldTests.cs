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

    }
}
