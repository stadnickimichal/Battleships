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
        public BattleshipGame(IGameRenderer renderer, IShipGenerator shipGenerator, IValidator<string> userInputValidator, 
            IGameboardManager gameboardManager)
        {
            _renderer = renderer;
            _shipGenerator = shipGenerator;
            _userInputValidator = userInputValidator;
            _gameboardManager = gameboardManager;
        }

        public Gameboard Gameboard;
        private GameSettings _settings;
        private List<Ship> _shipList = new List<Ship>();
        private IGameRenderer _renderer;
        private IShipGenerator _shipGenerator;
        private IValidator<string> _userInputValidator;
        private IGameboardManager _gameboardManager;

        public void Start(GameSettings settings)
        {
            IsGameOn = true;
            _settings = settings;
            Gameboard = InitializeGameboard();
            Gameboard.ShipsRemaining = _shipList.Count();
            _renderer.Render(Gameboard);
        }

        public bool MakeAMove(string userInput)
        {
            if (!IsGameOn)
            {
                throw new NotInicializedGameException();
            }

            var validationResult = _userInputValidator.Validate(userInput);
            if (!validationResult.IsValid)
            {
                Gameboard.Message = validationResult.ValidationMessage;
                _renderer.Render(Gameboard);
                return true;
            }

            if (_gameboardManager.RevielSquare(userInput, Gameboard))
            {
                Gameboard.Message = "You have already shoot at this coordinates, plase chose some other place!";
                _renderer.Render(Gameboard);
                return true;
            }
            else
            {
                (int column, int row) = Gameboard.GetSquareIndexesBaseOnUserInput(userInput);
                if ((Gameboard.Board[row, column] & SquareTypes.Ship) == SquareTypes.Ship)
                {
                    Gameboard.Message = "Hit";
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
                        if (ship.GetShipSquares().All(s => (Gameboard.Board[s.row, s.column] & SquareTypes.Revealed) == SquareTypes.Revealed))
                        {
                            ship.Sunk = true;
                            Gameboard.ShipsRemaining = _shipList.Count(s => !s.Sunk);
                            Gameboard.Message = "Hit and sink";
                            break;
                        }
                    }
                }
                else
                {
                    Gameboard.Message = "Miss";
                }

                if(_shipList.All(s => s.Sunk))
                {
                    Gameboard.Message = "You Have Won!!";
                    _renderer.Render(Gameboard);
                    IsGameOn = false;
                    return false;
                }
                else
                {
                    _renderer.Render(Gameboard);
                    return true;
                }
            }
        }

        private Gameboard InitializeGameboard()
        {
            Gameboard = new Gameboard();
            foreach (var item in _settings.ShipstToPlaceOnGameboard)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    var ship = _shipGenerator.GenerateShip(Gameboard, item.Key);
                    PlaceShipOnGameboard(ship);
                    _shipList.Add(ship);
                }
            }
            return Gameboard;
        }

        private void PlaceShipOnGameboard(Ship ship)
        {
            for (int i = 0; i < ship.GetLength(); i++)
            {
                if (ship.IsVertical)
                {
                    Gameboard.Board[ship.FirstSquareRowIndex + i, ship.FirstSquareColumnIndex] = SquareTypes.Ship | SquareTypes.Unrevealed;
                }
                else
                {
                    Gameboard.Board[ship.FirstSquareRowIndex, ship.FirstSquareColumnIndex + i] = SquareTypes.Ship | SquareTypes.Unrevealed;
                }
            }
        }
    }
}