using BattleshipCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Models
{
    public class Ship
    {
        public Ship(ShipTypes type) 
        {
            Type = type;
        }

        public int FirstSquareColumnIndex { get; set; }
        public int FirstSquareRowIndex { get; set; }
        public ShipTypes Type { get; set; }
        public bool IsVertical { get; set; }
        public bool Sunk { get; set; }
        public List<(int column, int row)> GetShipSquares()
        {
            var output = new List<(int column, int row)>();
            for (int i = 0; i < GetLength(); i++)
            {
                if (IsVertical)
                {
                    output.Add((FirstSquareColumnIndex, FirstSquareRowIndex + i));
                }
                else
                {
                    output.Add((FirstSquareColumnIndex + i, FirstSquareRowIndex));
                }
            }

            return output;
        }
        public int GetLength()
        {
            return (int)Type;
        }

        public bool IsSquarePartOfShip(int column, int row)
        {
            return GetShipSquares().Contains((column, row));
        }
    }
}
