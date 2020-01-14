using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace BattleshipStateTracker
{
    public interface IAddShipsToBoard
    {
        int[,] AddShips();
    }

    public class AddShipToBoardSample : IAddShipsToBoard
    {
        public int[,] Board { get; set; }

        public int[,] AddShips()
        {
            Board = new int[,] {
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,3,3,3,0,0,0,0,0,0},
                    {0,0,0,0,4,0,0,0,0,0},
                    {0,0,0,0,4,0,0,0,0,0},
                    {0,2,0,0,4,0,0,0,0,0},
                    {0,2,0,0,4,0,0,0,0,0},
                    {0,2,0,0,0,5,5,5,5,0},
                    {0,2,0,0,0,0,0,0,0,0},
                    {0,0,0,7,7,7,7,0,0,0},
                    {0,0,9,9,9,9,0,0,0,0}
                };

            return Board;
        }
    }

    public class AddShipsToBoardForLostTest : IAddShipsToBoard
    {
        public int[,] Board { get; set; }

        public int[,] AddShips()
        {
            Board = new int[,] {
                     {0,0,0,0,0,0,0,0,0,0},
                     {0,0,0,0,0,0,0,0,0,0},
                     {0,0,0,0,4,0,0,0,0,0},
                     {0,0,0,0,4,0,0,0,0,0},
                     {0,0,0,0,4,0,0,0,0,0},
                     {0,0,0,0,4,0,0,0,0,0},
                     {0,0,0,0,0,0,0,0,0,0},
                     {0,0,0,0,0,0,0,0,0,0},
                     {0,0,0,0,0,0,0,0,0,0},
                     {0,0,0,0,0,0,0,0,0,0}
                 };
            return Board;
        }             
    }

    public class AddShipsFromJsonFile : IAddShipsToBoard
    {
        public int[,] Board { get; set; }

        private string workingDirectory = Environment.CurrentDirectory;

        public AddShipsFromJsonFile(string fileName)
        {
            var jsonString = File.ReadAllText($"{workingDirectory}/{fileName}");
            Board = JsonConvert.DeserializeObject<int[,]>(jsonString);
        }
        public int[,] AddShips()
        {
            return Board;
        }

    }
}
