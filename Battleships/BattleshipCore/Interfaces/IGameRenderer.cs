using BattleshipCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Interfaces
{
    /// <summary>
    /// Renders a game on some output devise.
    /// </summary>
    public interface IGameRenderer
    {
        /// <summary>
        /// Render gameboard.
        /// </summary>
        /// <param name="board">Gameboard to render.</param>
        void Render(Gameboard board);
    }
}
