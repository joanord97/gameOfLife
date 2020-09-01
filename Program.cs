using System;
using GameLogic;

namespace gameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(200,50);

            game.run();

            


        }
    }
}
