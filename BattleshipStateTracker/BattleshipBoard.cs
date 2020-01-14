using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
    public class BattleshipBoard
    {
        private const int boardSize = 10;
        public BoardCell[,] Board { get; set; }
        public Dictionary<int, Battleship> BattleShips { get; set; }
        public IReport ReportTo { get; set; }

        public bool IsBoardReadyToPlay { get; set; }

        public BattleshipBoard()
        {
            BattleShips = new Dictionary<int, Battleship>();
            Board = new BoardCell[boardSize, boardSize];
            IsBoardReadyToPlay = false;
            InitializeBoard();
            ReportTo = new Report();            // setup default export as console output
        }
        public CoordinateState GetBoardCellState(Coordinate location)
        {
            return Board[location.X, location.Y].State;

        }

        public Boolean ToCheckShipIsSunk(Battleship ship)
        {
            if (!ship.IsSunk && ship.Deployment?.Count > 0)
            {
                foreach (Coordinate location in ship.Deployment)
                {
                    if (GetBoardCellState(location) != CoordinateState.HIT)
                        return false; // ship.IsSunk didn't change
                }
                ship.IsSunk = true; // A battleship is sunk if it has been hit on all the squares it occupies
            }
            return ship.IsSunk;     // A ghost ship occupies zero squares, it was sunk!
        }

        public void ReportCellState(string type, Coordinate location)
        {
            BoardCell cell = Board[location.X, location.Y];
            if (cell.ShipNumber > 0)
                ReportTo.WriteLine($" Type: {type} {location} {cell}");
            else
                ReportTo.WriteLine($" Type: {type} {location} ");         // shipNumber <=0 should be treated as exception
        }
        public int BoardCellAttacked(Coordinate location)
        {
            if (location.X < 0 || location.X > boardSize || location.Y < 0 || location.Y > boardSize)
            {
                ReportTo.WriteLine($" Type: Wrong Position {location} ");
                return -1;
            }

            BoardCell cell = Board[location.X, location.Y];
            Battleship ship; 

            switch (cell.State)
            {
                case CoordinateState.OCCUPIED:
                    cell.State = CoordinateState.HIT;
                    ship = BattleShips[cell.ShipNumber];
                    ToCheckShipIsSunk(ship);
                    ReportCellState("Hit", location);
                    break;
                case CoordinateState.HIT:
                    ship = BattleShips[cell.ShipNumber];
                    if (ship.IsSunk)
                        ReportCellState("Missed", location);  // Ship was sunk. 
                    else
                        ReportCellState("Hit", location);     // Hit the previous place, but ship isn't sunk. 
                    break;
                default:
                    ReportCellState("Missed", location);
                    break;
            }               
            return cell.ShipNumber;
        }

        public void InitializeBoard()
        {
            for (var x = 0; x < boardSize; x++)
            {
                for (var y = 0; y < boardSize; y++)
                {
                    Board[x, y] = new BoardCell(0, CoordinateState.INITIAL);
                }
            }
        }
         
        public void ReportBoardState()
        {
            for (var x = 0; x < boardSize; x++)
            {
                for (var y = 0; y < boardSize; y++)
                {
                    BoardCell cell = Board[x, y];

                    ReportTo.Write($"{cell.State.ToString().Substring(0,3)} ");
                }
                ReportTo.WriteLine("");
            }
        }

        public Boolean AddAShipToBoard(Battleship ship)
        {
            if (IsBoardReadyToPlay || ship.ShipNumber <= 0) return false;

            if (BattleShips.ContainsKey(ship.ShipNumber))
            {
                ReportTo.WriteLine("ShipNumber is already in board.");
                return false;
            }

            if (ship.Deployment.Count > boardSize || ship.Deployment.Count <= 0)
            {
                ReportTo.WriteLine("Ship must fit entirely on the board");
                return false;
            }
            
            if (!ship.IsValidDeployment())
            {
                ReportTo.WriteLine("The ship should be 1-by-n sized");
                return false;
            }
            
            // check if ship position had been occupied by another ship
            foreach ( Coordinate location in ship.Deployment)
                if (Board[location.X, location.Y].State == CoordinateState.OCCUPIED)
                {
                    ReportTo.WriteLine("Ship can't overlap another ship. {location}");
                    return false;
                }

            // put ship on the board
            foreach (Coordinate location in ship.Deployment)
            {
                BoardCell cell = Board[location.X, location.Y];
                cell.State = CoordinateState.OCCUPIED;
                cell.ShipNumber = ship.ShipNumber;
             }

            BattleShips.Add(ship.ShipNumber, ship);
            return true;
        }

        public Boolean SetupAndCheckBoard(int[,] importBoard)
        {

            if (importBoard.GetLength(0) != boardSize || importBoard.GetLength(1) != boardSize)
                return false;

            BattleShips = new Dictionary<int, Battleship>();

            // Add battleShips from importBoard
            for (var x = 0; x < boardSize; x++)
            {
                for (var y = 0; y < boardSize; y++)
                {
                    int shipNumber = importBoard[x, y];
                    if (shipNumber > 0)
                    {
                        if (!BattleShips.ContainsKey(shipNumber))
                            BattleShips.Add(shipNumber, new Battleship(shipNumber));
                        BattleShips[shipNumber].Deployment.Add(new Coordinate(x, y));
                        BoardCell cell = Board[x, y];
                        cell.ShipNumber = shipNumber;
                        cell.State = CoordinateState.OCCUPIED;
                    }
                }
            }

            // check if battleShips are all at valid deployment poistion.
            foreach (Battleship ship in BattleShips.Values)
            {
                if (!ship.IsValidDeployment())
                    return false;
            }

            IsBoardReadyToPlay = true;
            
            return true;
        }

        public bool AllShipsSunk()
        {
            foreach(int shipNumber in BattleShips.Keys)
            {
                if (!BattleShips[shipNumber].IsSunk)
                    return false;
            }
            return true;
        } 

    }
}
