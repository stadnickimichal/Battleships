using BattleshipCore.Enums;
using BattleshipCore.Lookups;
using BattleshipCore.Test.MockedClasses;
using BattleshipCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore.Test
{
    public class BattleshipGameTest
    {
        BattleshipGame _battleshipGame;
        public BattleshipGameTest()
        {
            var renderer = new MockedGameRenderer();
            var shipGenerator = new ShipGenerator();
            var userInputValidator = new UserInputValidator();
            var gameboardManager = new GameboardManager();
            _battleshipGame = new BattleshipGame(renderer, shipGenerator, userInputValidator, gameboardManager);
        }

        [Fact]
        public void BattleshipGame_PlayGameWithOnUI_Success()
        {
            //run test 10 times to make sure we don't get false positive
            for (int k = 0; k < 10; k++)
            {
                var settings = new GameSettings(new Dictionary<ShipTypes, int>()
                {
                    { ShipTypes.Battleship, 1 },
                    { ShipTypes.Destroyer, 2}
                });
                _battleshipGame.Start(settings);

                Queue<string> movesToMake = GetAllPossibleMoves();
                while (_battleshipGame.MakeAMove(movesToMake.Dequeue())) ;

                int shipAndRevieledCounter = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (((_battleshipGame.Gameboard.Board[i, j] & SquareTypes.Ship) == SquareTypes.Ship)
                            && (_battleshipGame.Gameboard.Board[i, j] & SquareTypes.Revealed) == SquareTypes.Revealed)
                        {
                            shipAndRevieledCounter++;
                        }
                    }
                }

                int shipSquareCount = 0;
                foreach (var item in settings.ShipstToPlaceOnGameboard)
                {
                    shipSquareCount += (int)item.Key * item.Value;
                }

                Assert.Equal(shipAndRevieledCounter, shipSquareCount);
                Assert.Equal(_battleshipGame.Gameboard.ShipsRemaining, 0);
                Assert.True(!_battleshipGame.IsGameOn);
            }
        }

        private Queue<string> GetAllPossibleMoves()
        {
            var output  = new Queue<string>();
            for (int i = 0; i < 10; i++)
            {
                for(int j = 1; j <= 10; j++)
                {
                    var column = (char)(i + 65);
                    var positionOnBoard = column.ToString() + j;
                    output.Enqueue(positionOnBoard);
                }
            }
            return output;
        }
    }
}
