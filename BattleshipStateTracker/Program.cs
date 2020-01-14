using Newtonsoft.Json;
using System;
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
                player.ReportTo.WriteLine("Game over!!!");           
        }
    }
}
