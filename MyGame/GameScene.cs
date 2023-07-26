using GameEngine;
using MyGame.GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Net.Http.Headers;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            root.AddChild(new CameraController(_camera));
            for(int i =0; i < 1000; i++)
            {
                StillSprite still = new StillSprite();
                still._position = new Vector2f (Game.Random.Next(20000) - 10000, Game.Random.Next(20000) - 10000);
                root.AddChild(still);
            }
            Camera camera = new Camera();
            for (int i = 0; i < 1000; i++)
            {
                StillSprite still = new StillSprite();
                still._position = new Vector2f(Game.Random.Next(200), Game.Random.Next(200) - 100 + 100);
                camera.AddChild(still);
            }
            root.AddChild(camera);
        }
    }
}