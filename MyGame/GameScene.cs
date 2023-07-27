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
            for(int i =0; i < 10000; i++)
            {
                StillSprite still = new StillSprite();
                still._position = new Vector2f (Game.Random.Next(20000) - 10000, Game.Random.Next(20000) - 10000);
                root.AddChild(still);
            }
            Camera camera = new Camera();
            root.AddChild(camera);
            debug._position = new Vector2f(-800, -450);
            camera.AddChild(debug);
        }
    }
}