using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Enums
{
    /// <summary>
    /// Possible valiues that can be assignet to on square of the gameboard.
    /// </summary>
    [Flags]
    public enum SquareTypes
    {
        Revealed = 1,
        Unrevealed = 2,
        Empty = 3,
        Ship = 4
    }
}
