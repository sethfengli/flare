using System;
using Xunit;
using BattleshipStateTracker;
using System.Collections.Generic;

namespace BattleshipStateTracker
{
    public class UnitTestPlayer
    {

        [Fact]
        public void NewPlayerTest()
        {
            Player player = new Player();

            var result = (player.Name == "Code Challenge" && player.Opponent is null);
            Assert.True(result, "Import board is valid");
        }

        [Fact]
        public void PlayBoardImportSampleTest()
        {
            Player player = new Player();

            player.CreateBoard();
            player.AddShipsToBoard();

            var result = player.PlayBoard.IsBoardReadyToPlay;
            Assert.True(result, "Import board from sample is valid");
        }

        [Fact]
        public void PlayBoardImportFromJSONFile()
        {
            Player player = new Player();

            player.CreateBoard();
            player.AddShipsFrom = new AddShipsFromJsonFile("board.json");
            player.AddShipsToBoard();

            var result = player.PlayBoard.IsBoardReadyToPlay;
            Assert.True(result, "Import board from file is valid");
        }
        
        [Fact]
        public void TakeAnAttackTest()
        {
            Player player = new Player();

            player.CreateBoard();
            player.AddShipsToBoard();

            var result = player.PlayBoard.IsBoardReadyToPlay;

            if (result)
            {
                // Row 3, Column 4 should be shipNumber 4
                result = player.TakeAnAttack(new Coordinate(2, 4)) == 4;
            }

            Assert.True(result, "Hit");
        }

        [Fact]
        public void IsLostGameTest()
        {
            Player player = new Player();

            player.CreateBoard();
            player.AddShipsFrom = new AddShipsToBoardForLostTest();
            player.AddShipsToBoard();

            var result = player.PlayBoard.IsBoardReadyToPlay;

            if (result)
            {
                // [2-5,4]  should be shipNumber 4
                result = player.TakeAnAttack(2, 4) == 4
                         && player.TakeAnAttack(3, 4) == 4
                         && player.TakeAnAttack(4, 4) == 4
                         && player.TakeAnAttack(5, 4) == 4;
            }

            if (result)
            {
                // Hit and lost
                result = player.IsLostGame();
            }

            Assert.True(result, "Lost");
        }



    }


}
