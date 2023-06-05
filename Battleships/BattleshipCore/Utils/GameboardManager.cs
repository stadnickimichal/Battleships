using BattleshipCore.Enums;
using BattleshipCore.Interfaces;
using BattleshipCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Utils
{
    public class GameboardManager : IGameboardManager
    {
        public bool RevielSquare(string userInput, Gameboard gameboard)
        {
            (int column, int row) = gameboard.GetSquareIndexesBaseOnUserInput(userInput);
            if ((gameboard.Board[row, column] & SquareTypes.Revealed) == SquareTypes.Revealed)
            {
                return true;
            }
            else
            {
                gameboard.Board[row, column] = gameboard.Board[row, column] | SquareTypes.Revealed;
                return false;
            }
        }

        public void PlaceShipOnGameboard(Ship ship, Gameboard gameboard)
        {
            for (int i = 0; i < ship.GetLength(); i++)
            {
                if (ship.IsVertical)
                {
                    gameboard.Board[ship.FirstSquareRowIndex + i, ship.FirstSquareColumnIndex] = SquareTypes.Ship | SquareTypes.Unrevealed;
                }
                else
                {
                    gameboard.Board[ship.FirstSquareRowIndex, ship.FirstSquareColumnIndex + i] = SquareTypes.Ship | SquareTypes.Unrevealed;
                }
            }
        }
    }
}
