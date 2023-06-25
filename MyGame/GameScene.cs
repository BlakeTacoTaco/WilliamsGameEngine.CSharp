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
            for (int i = 0; i < 100000; i++)
            {
                Sprite sprite = new Sprite();
                sprite.Texture = Game.GetTexture("../../../Resources/samon.png");
                sprite.Position = new Vector2f((Game.Random.Next(1600) - 800) * 10, (Game.Random.Next(1600) - 800) * 10);
                StillSprite still = new StillSprite(sprite);
                AddGameObject(still);
            }
            AddGameObject(new CameraController(camera));
        }
    }
}