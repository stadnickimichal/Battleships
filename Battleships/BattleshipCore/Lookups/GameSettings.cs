using BattleshipCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Lookups
{
    public class GameSettings
    {
        public GameSettings(Dictionary<ShipTypes, int> settings) 
        {
            ShipstToPlaceOnGameboard = settings;
        }
        public Dictionary<ShipTypes, int> ShipstToPlaceOnGameboard { get; set; }
    }
}
