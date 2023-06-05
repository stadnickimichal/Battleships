using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.CustomExceptions
{
    public class NotInicializedGameException : Exception
    {
        public NotInicializedGameException() : base("Game is has not started. Use method Start() to initialize the game")
        {

        }
    }
}
