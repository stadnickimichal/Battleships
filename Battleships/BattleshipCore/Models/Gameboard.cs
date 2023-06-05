using BattleshipCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Models
{
    public class Gameboard
    {
        public Gameboard() 
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Board[i, j] = SquareTypes.Unrevealed;
                }
            }
            Message = "";
        }
        public SquareTypes[,] Board { get; set; } = new SquareTypes[10,10];
        public string Message { get; set; }
        public int ShipsRemaining { get; set; }
        public (int column, int row) GetSquareIndexesBaseOnUserInput(string userInput)
        {
            int columnPartLength = 1;
            int columnPartBeginingIndex = 0;
            int rowPartBeginingIndex = 1;
            int charToIntShift = 65;

            var column = char.ToUpper(char.Parse(userInput.Substring(columnPartBeginingIndex, columnPartLength))) - charToIntShift;
            var row = int.Parse(userInput.Substring(rowPartBeginingIndex)) - 1;
            return (column, row);
        }
    }
}
