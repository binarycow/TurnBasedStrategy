using System;

namespace StrategyGame
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var game = new StrategyGame();
            game.Run();
        }
    }
}
