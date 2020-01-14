using System;
using Xunit;
using BattleshipStateTracker;
using System.Collections.Generic;

namespace BattleshipStateTracker
{
    public class UnitTestBattleshipsBoard
    {

        [Fact]
        public void AddAShipToBoardTest()
        {
            BattleshipBoard board = new BattleshipBoard();

            Battleship ship = new Battleship(6);
            ship.SetupDeployment(new List<(int, int)> { (1, 3), (1, 4), (1, 5), (1, 6) });
           
            var result = board.AddAShipToBoard(ship);
                
            Assert.True(result, "Add a ship to board ");
        }

        [Fact]
        public void AddAShipToBoardAdnSinkIt()       
        {
            BattleshipBoard board = new BattleshipBoard();
            var shipNumber = 100;
            Battleship ship = new Battleship(shipNumber);
            ship.SetupDeployment(new List<(int, int)> { (7, 3), (8, 3), (9, 3)});
            var result = board.AddAShipToBoard(ship);

            if (result)
            {
                board.IsBoardReadyToPlay = true;
                result = board.BoardCellAttacked(new Coordinate(7, 3)) == shipNumber
                        && board.BoardCellAttacked(new Coordinate(8, 3)) == shipNumber
                        && board.BoardCellAttacked(new Coordinate(9, 3)) == shipNumber;
            }

            if (result)
            {
                result = ship.IsSunk;
            }
                       
            Assert.True(result, "Add a ship to board  and Sink it");
        }
                
        [Fact]
        public void BoardImportSampleTest()
        {
            BattleshipBoard board = new BattleshipBoard();
            AddShipToBoardSample importBoard = new AddShipToBoardSample();

            var result = board.SetupAndCheckBoard(importBoard.AddShips());
            Assert.True(result, "Import board is valid");
        }

        [Fact]
        public void BoardImportFromJSONFile()
        {
            BattleshipBoard board = new BattleshipBoard();
            AddShipsFromJsonFile importBoard = new AddShipsFromJsonFile("board.json");

            var result = board.SetupAndCheckBoard(importBoard.AddShips());
            Assert.True(result, "Import board is valid");
        }


        [Fact]
        public void BoardCellAttackedTest()
        {
            BattleshipBoard board = new BattleshipBoard();
            AddShipToBoardSample importBoard = new AddShipToBoardSample();

            var result = board.SetupAndCheckBoard(importBoard.AddShips());

            if (result)
            {
                // Row 3, Column 4 should be shipNumber 4
                result = board.BoardCellAttacked(new Coordinate(2, 4)) == 4;
            }

            Assert.True(result, "Hit");
        }

        [Fact]
        public void CheckAllShipsSunkOnPlayBoardTest()
        {
            BattleshipBoard board = new BattleshipBoard();
            AddShipsToBoardForLostTest importBoard = new AddShipsToBoardForLostTest();

            var result = board.SetupAndCheckBoard(importBoard.AddShips());

            if (result)
            {
                // [2-5,4]  should be shipNumber 4
                result = board.BoardCellAttacked(new Coordinate(2, 4)) == 4
                         && board.BoardCellAttacked(new Coordinate(3, 4)) == 4
                         && board.BoardCellAttacked(new Coordinate(4, 4)) == 4
                         && board.BoardCellAttacked(new Coordinate(5, 4)) == 4;
            }

            if (result)
            {
                // Hit and lost
                result = board.CheckAllShipsSunkOnPlayBoard();
            }

            Assert.True(result, "Lost");
        }


    }
}
