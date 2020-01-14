using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
   
    public class Battleship
    {
        public int ShipNumber { get; set; }

        public Boolean IsSunk { get; set; }

        public List<Coordinate> Deployment  { get; set; }
     
        public Battleship( int shipNumber = 0)
        {
            ShipNumber =  shipNumber;
            IsSunk = false;
            Deployment = new List<Coordinate>();
        }

        public void SetupDeployment(List<Coordinate> deployment )
        {
            if (deployment?.Count > 0)
                Deployment = deployment;
        }

        public void SetupDeployment(List<(int, int)> deployment)
        {
            if (deployment?.Count > 0)
            {
                foreach ( (int x, int y) in deployment)
                {
                    Deployment.Add(new Coordinate(x, y));
                }
            }
         }
        
        public bool IsValidDeployment()
        {
            if (ShipNumber == 0) return false;

            if (Deployment.Count == 0) return false;

            int x = Deployment[0].X;
            int y = Deployment[0].Y;
            int minX = x;
            int maxX = x;
            int minY = y;
            int maxY = y;

            foreach (Coordinate location in Deployment)
            {
                if (minX > location.X) minX = location.X;
                if (maxX < location.X) maxX = location.X;
                if (minY > location.Y) minY = location.Y;
                if (maxY < location.Y) maxY = location.Y;
            }

            // Make sure the ships is 1-by-n sized, must be aligned either vertically or horizontally.
            if (maxX == minX && (maxY - minY + 1) == Deployment.Count) return true;
            if (maxY == minY && (maxX - minX + 1) == Deployment.Count) return true;

            return false;
        }
    }
}