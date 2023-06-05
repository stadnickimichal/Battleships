using BattleshipCore.CustomExceptions;
using BattleshipCore.Enums;
using BattleshipCore.Interfaces;
using BattleshipCore.Models;
using BattleshipCore.Utils;
using Microsoft.VisualStudio.CodeCoverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Test.Utils
{
    public class ShipGeneratorTest
    {
        public ShipGenerator _shipGenerator;
        private GameboardManager _gameboardManager;

        public ShipGeneratorTest() 
        {
            _shipGenerator = new ShipGenerator();
            _gameboardManager = new GameboardManager();
        }
        [Fact]
        public void GenerateShip_Generate3Ships_Success()
        {
            //run test 10 times to make sure we don't get false positive
            for (int i = 0; i < 10; i++)
            {
                var gameBoard = new Gameboard();
                Ship ship1 = null, ship2 = null, ship3 = null;
                try
                {
                    ship1 = _shipGenerator.GenerateShip(gameBoard, ShipTypes.Battleship);
                    _gameboardManager.PlaceShipOnGameboard(ship1, gameBoard);
                    ship2 = _shipGenerator.GenerateShip(gameBoard, ShipTypes.Destroyer);
                    _gameboardManager.PlaceShipOnGameboard(ship2, gameBoard);
                    ship3 = _shipGenerator.GenerateShip(gameBoard, ShipTypes.Destroyer);

                }
                catch (UnableToGenerateShipException ex)
                {
                    Assert.Fail("Exided number of attempts.");
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }

                var squaresTakenByShips = ship1.GetShipSquares();
                squaresTakenByShips.AddRange(ship2.GetShipSquares());
                squaresTakenByShips.AddRange(ship3.GetShipSquares());
                //check if there are no duplicates what would mean that ships are overlapping
                Assert.True(!squaresTakenByShips.GroupBy(cr => new { cr.column, cr.row }).Where(g => g.Count() > 1).Any());
                Assert.True(squaresTakenByShips.Count() == (int)ShipTypes.Battleship + (int)ShipTypes.Destroyer + (int)ShipTypes.Destroyer);
            }
        }
    }
}
