using BattleshipCore.Interfaces;
using BattleshipCore.Models;
using BattleshipCore.Enums;
using System.Drawing;
using BattleshipCore.CustomExceptions;
using BattleshipCore.Utils;
using BattleshipCore.Lookups;
using System.Runtime;

namespace BattleshipCore
{
    public class BattleshipGame : IBattleshipGame
    {
        public bool IsGameOn { get; set; }
        public BattleshipGame(IGameRenderer renderer, IShipGenerator shipGenerator, IValidator<string> userInputValidator)
        {
            _renderer = renderer;
            _shipGenerator= shipGenerator;
            _userInputValidator = userInputValidator;
        }

        private Gameboard _gameboard;
        private GameSettings _settings;
        private List<Ship> _shipList = new List<Ship>();
        private IGameRenderer _renderer;
        private IShipGenerator _shipGenerator;
        private IValidator<string> _userInputValidator;

        public void Start(GameSettings settings)
        {
            IsGameOn = true;
            _settings = settings;
            _gameboard = InitializeGameboard();
            _gameboard.ShipsRemaining = _shipList.Count();
            _renderer.Render(_gameboard);
        }

        public bool MakeAMove(string userMove)
        {
            if (!IsGameOn)
            {
                throw new NotInicializedGameException();
            }

            var validationResult = _userInputValidator.Validate(userMove);
            if (!validationResult.IsValid)
            {
                _gameboard.Message = validationResult.ValidationMessage;
                _renderer.Render(_gameboard);
                return true;
            }

            (int column, int row) = GetSquareIndexes(userMove);
            if ((_gameboard.Board[row, column] & SquareTypes.Revealed) == SquareTypes.Revealed)
            {
                _gameboard.Message = "You have already shoot at this coordinates, plase chose some other place!";
                _renderer.Render(_gameboard);
                return true;
            }
            else
            {
                _gameboard.Board[row, column] = _gameboard.Board[row, column] | SquareTypes.Revealed;
                if ((_gameboard.Board[row, column] & SquareTypes.Ship) == SquareTypes.Ship)
                {
                    _gameboard.Message = "Hit";
                    Ship shipHit;
                    foreach (var ship in _shipList.Where(s => !s.Sunk))
                    {
                        if(ship.IsSquarePartOfShip(column, row))
                        {
                            shipHit = ship;
                        }
                        else
                        {
                            continue;
                        }
                        if (ship.GetShipSquares().All(s => (_gameboard.Board[s.row, s.column] & SquareTypes.Revealed) == SquareTypes.Revealed))
                        {
                            ship.Sunk = true;
                            _gameboard.ShipsRemaining = _shipList.Count(s => !s.Sunk);
                            _gameboard.Message = "Hit and sink";
                            break;
                        }
                    }
                }
                else
                {
                    _gameboard.Message = "Miss";
                }

                if(_shipList.All(s => s.Sunk))
                {
                    _gameboard.Message = "You Have Won!!";
                    _renderer.Render(_gameboard);
                    IsGameOn = false;
                    return false;
                }
                else
                {
                    _renderer.Render(_gameboard);
                    return true;
                }
            }
        }

        private (int column, int row) GetSquareIndexes(string userMove)
        {
            int columnPartLength = 1;
            int columnPartBeginingIndex = 0;
            int rowPartBeginingIndex = 1;
            int charToIntShift = 65;

            var column = char.ToUpper(char.Parse(userMove.Substring(columnPartBeginingIndex, columnPartLength))) - charToIntShift;
            var row = int.Parse(userMove.Substring(rowPartBeginingIndex)) - 1;
            return (column, row);
        }

        private Gameboard InitializeGameboard()
        {
            _gameboard = new Gameboard();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _gameboard.Board[i, j] = SquareTypes.Unrevealed;
                }
            }
            foreach (var item in _settings.ShipstToPlaceOnGameboard)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    var ship = _shipGenerator.GenerateShip(_gameboard, item.Key);
                    PlaceShipOnGameboard(ship);
                    _shipList.Add(ship);
                }
            }
            return _gameboard;
        }

        private void PlaceShipOnGameboard(Ship ship)
        {
            for (int i = 0; i < ship.GetLength(); i++)
            {
                if (ship.IsVertical)
                {
                    _gameboard.Board[ship.FirstSquareRowIndex + i, ship.FirstSquareColumnIndex] = SquareTypes.Ship | SquareTypes.Unrevealed;
                }
                else
                {
                    _gameboard.Board[ship.FirstSquareRowIndex, ship.FirstSquareColumnIndex + i] = SquareTypes.Ship | SquareTypes.Unrevealed;
                }
            }
        }
    }
}