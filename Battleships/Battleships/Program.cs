using BattleshipCore.CustomExceptions;
using BattleshipCore.Enums;
using BattleshipCore.Interfaces;
using BattleshipCore.Lookups;
using Battleships;
using Unity;

var container = UnityConfig.GetUnityContainer();
var game = container.Resolve<IBattleshipGame>();
try
{
    var settings = new GameSettings(new Dictionary<ShipTypes, int>()
    {
        { ShipTypes.Battleship, 1 },
        { ShipTypes.Destroyer, 2}
    });
    game.Start(settings);
}
catch (UnableToGenerateShipException ex)
{
    Console.WriteLine($"Unable to place all ships on the map, try agine or try to change the game settings. Stack trace:\n{ex.StackTrace}");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Error while game initialization.\nError message: {ResolveError(ex)}.\nStack trace:\n{ex.StackTrace}");
    return;
}

bool hasGameEnded;
do
{
    Console.WriteLine("Chose your target and press Enter: ");
    var userMove = Console.ReadLine();
    try
    {
        hasGameEnded = game.MakeAMove(userMove);
    }
    catch(NotInicializedGameException ex)
    {
        Console.WriteLine($"Game not initialized before calling MakeAMove() method. Remember to cal Start() method first. \nStack trace:\n{ex.StackTrace}");
        return;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while move execution.\nError message: {ResolveError(ex)}.\nStack trace:\n{ex.StackTrace}");
        return;
    }
}
while (hasGameEnded);

Console.WriteLine("Game has ended");

string ResolveError(Exception error)
{
    var output = "";
    var currentError = error;
    while(currentError != null)
    {
        output += currentError.Message + ".\n";
        currentError = currentError.InnerException;
    }

    return output;
}