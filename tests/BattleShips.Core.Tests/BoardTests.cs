using Battleships.Core;
using FluentAssertions;
using Xunit;

namespace BattleShips.Core.Tests
{
    public class BoardTests
    {
        [Fact]
        public void BoardClass_ShouldExist()
        {
            var board = new Board();

            board.Should().NotBeNull();
        }
    }
}
