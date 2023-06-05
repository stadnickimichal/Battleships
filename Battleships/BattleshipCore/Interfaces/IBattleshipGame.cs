using BattleshipCore.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Interfaces
{
    public interface IBattleshipGame
    {
        /// <summary>
        /// Indicate if the game has been started.
        /// </summary>
        public bool IsGameOn { get; set; }
        /// <summary>
        /// Initalize the game.
        /// </summary>
        /// <param name="settings">Settings to apply to game.</param>
        void Start(GameSettings settings);
        /// <summary>
        /// Make on move base on user input shooting at on square.
        /// </summary>
        /// <param name="userMove">Row user input as a string.</param>
        /// <returns>Boolean value indicating if the game has ended.</returns>
        bool MakeAMove(string userMove);
    }
}
