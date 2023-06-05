using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Interfaces
{
    /// <summary>
    /// Generates random values.
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// Generates random true or false value. Toss a coin.
        /// </summary>
        /// <returns>Random boolean value.</returns>
        bool RandomBoolean();
        /// <summary>
        /// Generate random number base within supplied range.
        /// </summary>
        /// <param name="rageStart">Begining of a range.</param>
        /// <param name="rangeEnd">Ending of a range.</param>
        /// <returns></returns>
        int RandomNumber(int rageStart, int rangeEnd);
    }
}
