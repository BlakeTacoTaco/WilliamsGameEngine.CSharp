using GameEngine;
using MyGame.GameEngine;
using System;
using System.IO;
using System.Threading;

namespace MyGame
{
    static class MyGame
    {
        ///*
        public const int WindowWidth = 66;
        public const int WindowHeight = 31;
        //*/
        /*
        public const int WindowWidth = 117;
        public const int WindowHeight = 62;
        //*/
        private static void Main(string[] args)
        {
            // Initialize the game.
            Game.Initialize(WindowWidth, WindowHeight);

            // Create our scene.
            GameScene scene = new GameScene();
            Game.SetScene(scene);

            // Run the game loop.
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            Game.Run();
        }
    }
}