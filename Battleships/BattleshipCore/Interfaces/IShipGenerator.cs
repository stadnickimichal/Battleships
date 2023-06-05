using BattleshipCore.Enums;
using BattleshipCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Interfaces
{
    /// <summary>
    /// Class for generating ships to place on gameboard.
    /// </summary>
    public interface IShipGenerator
    {
        /// <summary>
        /// Generate ship on random, valid position.
        /// </summary>
        /// <param name="gameboard">Board to place a ship on.</param>
        /// <param name="type">Type of ship to be generated.</param>
        /// <returns></returns>
        Ship GenerateShip(Gameboard gameboard, ShipTypes type);

    }
}
