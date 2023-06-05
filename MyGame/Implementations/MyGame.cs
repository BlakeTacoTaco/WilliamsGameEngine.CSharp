using GameEngine;

namespace MyGame.Implementations
{
    static class MyGameb
    {
        public const int WindowWidth = 1920;
        public const int WindowHeight = 1081;

        internal const string WindowTitle = "Rare Fishe Market";

        internal static void Main(string[] args)
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