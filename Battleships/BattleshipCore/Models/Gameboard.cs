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
        public SquareTypes[,] Board { get; set; } = new SquareTypes[10,10];
        public string Message { get; set; }
        public int ShipsRemaining { get; set; }
    }
}
