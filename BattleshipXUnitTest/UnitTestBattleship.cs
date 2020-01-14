using System;
using Xunit;
using BattleshipStateTracker;
using System.Collections.Generic;

namespace BattleshipStateTracker
{
    public class UnitTestCoordinate
    {
        [Fact]
        public void CoordinateTest()
        {
            Coordinate location = new Coordinate(4, 2);

            var result = (location.X ==4 && location.Y == 2);
            Assert.True(result, location.ToString());
        }
    }

    public class UnitTestBoardCoordinate
    {
        [Fact]
        public void BoardCoordinateTest()
        {
            BoardCell cell = new BoardCell( 4, CoordinateState.OCCUPIED);

            var result = (cell.ShipNumber == 4 && cell.State == CoordinateState.OCCUPIED);
            Assert.True(result, cell.ToString());
        }
    }

    public class UnitTestBattleship
    {
        [Fact]
        public void BattleshipTest1()
        {
            Battleship ship = new Battleship(4);

            ship.SetupDeployment(new List<(int, int)> { (2, 3), (2, 5), (2, 6), (2, 7) });

            var result = (ship.ShipNumber == 4 && ship.IsValidDeployment());
            Assert.False(result, $"Battleship: ShipNumber={ship.ShipNumber} IsValidDeployment={result} ");
        }

        [Fact]
        public void BattleshipTest2()
        {

            Battleship ship = new Battleship(5);
            ship.SetupDeployment(new List<(int, int)> { (3, 1), (3, 2), (3, 3), (3, 4) });

            var result = (ship.ShipNumber == 5 && ship.IsValidDeployment());
            Assert.True(result, $"Battleship: ShipNumber={ship.ShipNumber} IsValidDeployment={result} ");

        }

        [Fact]
        public void BattleshipTest3()
        {

            Battleship ship = new Battleship(5);
            ship.SetupDeployment(new List<(int, int)> { (3, 1), (3, 2), (4, 3), (3, 4) });

            var result = (ship.ShipNumber == 5 && ship.IsValidDeployment());
            Assert.False(result, $"Battleship: ShipNumber={ship.ShipNumber} IsValidDeployment={result} ");

        }

    }
    
}
