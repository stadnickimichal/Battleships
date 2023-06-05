using BattleshipCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Utils
{
    public class BaseRandomGenerator : IRandomGenerator
    {
        private Random _randomGenerator;
        public BaseRandomGenerator(Random randomGenerator) 
        {
            _randomGenerator = randomGenerator;
        }
        public bool RandomBoolean()
        {
            return _randomGenerator.Next(2) == 1;
        }

        public int RandomNumber(int rageStart, int rangeEnd)
        {
            return _randomGenerator.Next(rageStart, rangeEnd);
        }
    }
}
