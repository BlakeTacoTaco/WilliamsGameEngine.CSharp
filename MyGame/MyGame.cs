using GameEngine;

namespace MyGame
{
    static class MyGame
    {
        public const int WindowWidth = 1600;
        public const int WindowHeight = 900;

        private const string WindowTitle = "Rare Fishe Market";

        private static void Main(string[] args)
        {
            // Initialize the game.
            Game.Initialize(WindowWidth, WindowHeight, WindowTitle);

            // Create our scene.
            GameScene scene = new GameScene();
            Game.SetScene(scene);

            // Run the game loop.
            Game.Run();
        }
    }
}