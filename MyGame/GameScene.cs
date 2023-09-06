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
                Wobbler wob = new Wobbler();
                wob._localPos = new Vector2f (Game.Random.Next(20000) - 10000, Game.Random.Next(20000) - 10000);
                for(int j = 0; j < 10; j++)
                {
                    StillSprite still = new StillSprite();
                    still._localPos = new Vector2f(Game.Random.Next(200) - 100, Game.Random.Next(200) - 100);
                    wob.AddChild(still);
                }
                root.AddChild(wob);
            }
            Camera camera = new Camera();
            root.AddChild(camera);
            debug._localPos = new Vector2f(-800, -450);
            camera.AddChild(debug);
        }
    }
}