using BattleshipCore.Interfaces;
using BattleshipCore.Models;
using BattleshipCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipCore.CustomExceptions;

namespace BattleshipCore.Utils
{
    public class ShipGenerator : IShipGenerator
    {
        private Random _randomGenerator;
        private int NumberOfMaximumAttempts = 10;

        public ShipGenerator()
        {
            this._randomGenerator = new Random();
        }

        public Ship GenerateShip(Gameboard gameboard, ShipTypes type)
        {
            var ship = new Ship(type);
            var tryCounter = 0;
            while (!TryPlaceShipOnBoard(ship, gameboard) && (tryCounter++ < NumberOfMaximumAttempts)) ;
            if(tryCounter >= NumberOfMaximumAttempts)
            {
                throw new UnableToGenerateShipException();
            }
            return ship;
        }

        private bool TryPlaceShipOnBoard(Ship ship, Gameboard gameboard)
        {
            var isVertical = _randomGenerator.Next(2) == 1;
            var maxColumnIndex = isVertical ? gameboard.Board.GetLength(0) - 1 : gameboard.Board.GetLength(0) - 1 - ship.GetLength();
            var maxRowIndex = isVertical ? gameboard.Board.GetLength(0) - 1 - ship.GetLength() : gameboard.Board.GetLength(0) - 1;

            int initColumnIndex = _randomGenerator.Next(0, maxColumnIndex);
            int initRowIndex = _randomGenerator.Next(0, maxRowIndex);

            for (int i = 0; i < ship.GetLength(); i++)
            {
                if (isVertical)
                {
                    var square = gameboard.Board[initRowIndex + i, initColumnIndex];
                    if((square & SquareTypes.Ship) == SquareTypes.Ship)
                    {
                        return false;
                    }
                }
                else
                {
                    var square = gameboard.Board[initRowIndex, initColumnIndex + i];
                    if ((square & SquareTypes.Ship) == SquareTypes.Ship)
                    {
                        return false;
                    }
                }
            }

            ship.IsVertical = isVertical;
            ship.FirstSquareColumnIndex = initColumnIndex;
            ship.FirstSquareRowIndex = initRowIndex;
            return true;
        }
    }
}
