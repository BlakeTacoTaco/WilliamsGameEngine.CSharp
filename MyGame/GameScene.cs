using GameEngine;
using MyGame.GameEngine;
using System;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            /*for (int i = 0; i < 600; i++)
            {
                AddGameObject(new FireFly());
            }
            for(int i = 0; i < 1000; i++)
            {
                AddGameObject(new OutlineRectangle());
            }*/
            Sprite sprite = new Sprite(4,5);
            for(int i = 0; i < sprite.pixels.Length; i++)
            {
                for(int j = 0; j < sprite.pixels[i].Length; j++)
                {
                    sprite.pixels[i][j] = new Pixel(i * 255 / sprite.pixels.Length, j * 255 / sprite.pixels[i].Length, 0);
                }
            }
            AddGameObject(sprite);
        }
    }
}