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
    /// Class to be use to apply changes to the gameboard.
    /// </summary>
    public interface IGameboardManager
    {
        /// <summary>
        /// Places ship on a map with assumption that it has been validated.
        /// </summary>
        /// <param name="ship">Ship to place.</param>
        /// <param name="gameboard">Gameboard to place a ship on.</param>
        void PlaceShipOnGameboard(Ship ship, Gameboard gameboard);
        /// <summary>
        /// Reviels the suqare bas on user input, if the square haven't been revieled yet.
        /// </summary>
        /// <param name="userInput">User row input.</param>
        /// <param name="gameboard">Gameboard to reviele the square on.</param>
        /// <returns>Boolean value indicating if the square have beed revielde already.</returns>
        bool RevielSquare(string userInput, Gameboard gameboard);
    }
}
