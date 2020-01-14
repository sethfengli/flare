using System;
using System.Collections.Generic;

namespace BattleshipStateTracker
{
    public class BoardCell
    {
        public int ShipNumber { get; set; }
        public CoordinateState State { get; set; }
        public BoardCell(int shipNumber, CoordinateState state)
        {
            ShipNumber = shipNumber;
            State = state;
        }

        public override string ToString()
        {
            return ($"BoardCell : {{ ShipNumber = {ShipNumber}, State = {State} }}");
        }
    }
}
