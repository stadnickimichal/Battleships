using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.CustomExceptions
{
    public class UnableToGenerateShipException : Exception
    {
        public UnableToGenerateShipException() : base("Unable to generate ship")
        {
        }
    }
}
