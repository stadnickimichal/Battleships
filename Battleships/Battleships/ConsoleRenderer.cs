using BattleshipCore.Interfaces;
using BattleshipCore.Models;
using BattleshipCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Battleships
{
    internal class ConsoleRenderer : IGameRenderer
    {
        private int ColumnDesignationsLength = 2;
        private int ScoreLabelRowIndex = 4;
        private int ScoreLabelSpacingLenght = 4;
        private List<string> ColumnDesignations = new List<string>()
        {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J"
        };
        public void Render(Gameboard board)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("Let's play Battleships!");
            Console.WriteLine(GetColumnDesignationsWithSpacing());
            for (int i=0; i<10; i++)
            {
                string rowDesignation = GetRowDesignationWithSpacing(i);
                WriteWithColor(rowDesignation, ConsoleColor.Black);
                for (int j=0; j<10; j++)
                {
                    var square = board.Board[i,j ];
                    if (((square & SquareTypes.Ship) == SquareTypes.Ship) && (square & SquareTypes.Revealed) == SquareTypes.Revealed)
                    {
                        WriteWithColor("X ", ConsoleColor.Green);
                    }
                    else if ((square & SquareTypes.Revealed) == SquareTypes.Revealed)
                    {
                        WriteWithColor("O ", ConsoleColor.Yellow);
                    }
                    else if ((square & SquareTypes.Unrevealed) == SquareTypes.Unrevealed)
                    {
                        WriteWithColor("□ ", ConsoleColor.Blue);
                    }
                }

                if(ScoreLabelRowIndex == i)
                {
                    var scorelabel = ApplySpacing(ScoreLabelSpacingLenght);
                    scorelabel += $"Remaining Ships: {board.ShipsRemaining}";
                    WriteWithColor(scorelabel, ConsoleColor.Black);
                }
                Console.Write("\n");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(board.Message);
        }

        private string GetRowDesignationWithSpacing(int i)
        {
            return (i + 1 < 10) ? (i + 1).ToString() + " " : (i + 1).ToString();
        }

        private void WriteWithColor(string text, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Write(text);
        }

        private string GetColumnDesignationsWithSpacing()
        {
            string output = ApplySpacing(ColumnDesignationsLength);

            foreach (var item in ColumnDesignations)
            {
                output += item + " ";
            }
            return output;
        }

        private void ApplySpacing(string text, int spacingLength)
        {
            for (int i = 0; i < spacingLength; i++)
            {
                text += " ";
            }
        }
        private string ApplySpacing(int spacingLength)
        {
            string output = "";
            for (int i = 0; i < spacingLength; i++)
            {
                output += " ";
            }
            return output;
        }
    }
}
