using System;

namespace myGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new RvGame())
                game.Run();
        }
    }
}
