using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
    public class Player
    {
        public string Name { get; set; }
        public Player Opponent { get; set; }
        public BattleshipBoard PlayBoard { get; set; }

        public IAddShipsToBoard AddShipsFrom { get; set; }

        public IReport ReportTo { get; set; }

        public Player()
        {
            Name = "Code Challenge";
            Opponent = null;        
            ReportTo =new Report();
            CreateBoard();
            AddShipsFrom = new AddShipToBoardSample();
        }

        public void CreateBoard()
        {
            PlayBoard = new BattleshipBoard();
            PlayBoard.ReportTo = ReportTo;
        }

        public bool AddShipsToBoard()
        {
           return PlayBoard.SetupAndCheckBoard(AddShipsFrom.AddShips());
        }

        public int TakeAnAttack (Coordinate position)
        {
            return PlayBoard.BoardCellAttacked(position);
        }

        public int  TakeAnAttack(int x, int y)
        {
            var position = new Coordinate(x, y);
            return PlayBoard.BoardCellAttacked(position);
        }

        public bool IsLostGame()
        {
            return ( PlayBoard.IsBoardReadyToPlay && PlayBoard.AllShipsSunk() );
        }

        public void ReportPlayBoardState()
        {
            if (PlayBoard.IsBoardReadyToPlay)
                PlayBoard.ReportBoardState();
            else
                ReportTo.WriteLine("Playboard is not ready yet.");
        }

    }
}
