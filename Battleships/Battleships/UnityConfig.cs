using BattleshipCore;
using BattleshipCore.Interfaces;
using BattleshipCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Battleships
{
    internal class UnityConfig
    {
        public static UnityContainer GetUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IBattleshipGame, BattleshipGame>();
            container.RegisterType<IGameRenderer, ConsoleRenderer>();
            container.RegisterType<IShipGenerator, ShipGenerator>();
            container.RegisterType<IRandomGenerator, BaseRandomGenerator>();
            container.RegisterType<IValidator<string>, UserInputValidator>();
            container.RegisterType<IGameboardManager, GameboardManager> ();

            return container; 
        }
    }
}
