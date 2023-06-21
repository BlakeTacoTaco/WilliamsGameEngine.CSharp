using GameEngine;
using MyGame.GameEngine;
using SFML.System;
using System;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            for (int i = 0; i < 100000; i++)
            {
                SSprite sprite = new SSprite();
                sprite.sprite.Texture = Game.GetTexture("../../../Resources/samon.png");
                sprite.position = new Vector2f(Game.Random.Next(1600) - 800, Game.Random.Next(900) - 450);
                AddGameObject(sprite);
            }
            camera.zoom = new Vector2f(4, 4);
            AddGameObject(new CameraController());

            AddGameObject(new FpsDisplay());
        }
    }
}