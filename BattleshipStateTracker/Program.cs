using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BattleshipStateTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            player.ReportPlayBoardState();

            player.CreateBoard();
            player.ReportPlayBoardState();
            player.AddShipsFrom = new AddShipsToBoardForLostTest();
            player.ReportPlayBoardState();
            player.AddShipsToBoard();
            player.ReportPlayBoardState();

            var result = player.PlayBoard.IsBoardReadyToPlay;

            if (result)
            {
                // [2-5,4]  should be shipNumber 4
                result = player.TakeAnAttack(2, 4) == 4
                         && player.TakeAnAttack(3, 4) == 4
                         && player.TakeAnAttack(4, 4) == 4
                         && player.TakeAnAttack(5, 4) == 4;
            }
            
            player.ReportPlayBoardState();

            if (player.IsLostGame())
                player.ReportTool.WriteLine("Game over!!!");

            player.ReportTool.WriteLine("--------------------");
            player.ReportTool.WriteLine("####################");
            player.ReportTool.WriteLine("Start new Game");

            player.PlayBoard.CreateBattleshipBoard();
            var shipNumber = 100;
            Battleship ship = new Battleship(shipNumber);
            ship.SetupDeployment(new List<(int, int)> { (7, 3), (8, 3), (9, 3) });
            player.ReportPlayBoardState();
            player.PlayBoard.AddAShipToBoard(ship);
            player.ReportPlayBoardState();
            player.PlayBoard.IsBoardReadyToPlay = true;
            player.ReportPlayBoardState();

            player.PlayBoard.BoardCellAttacked(new Coordinate(1, 3));
            player.PlayBoard.BoardCellAttacked(new Coordinate(7, 3));
            player.PlayBoard.BoardCellAttacked(new Coordinate(9, 9));
            player.PlayBoard.BoardCellAttacked(new Coordinate(8, 3));
            player.PlayBoard.BoardCellAttacked(new Coordinate(9, 3));

            player.ReportPlayBoardState();

            if (player.IsLostGame())
                player.ReportTool.WriteLine("Game over!!!");


        }
    }
}
