using GameEngine;
using MyGame.GameEngine;
using System;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            /*
            for (int i = 0; i < 600; i++)
            {
                AddGameObject(new FireFly());
            }
            //*/
            /*
            for(int i = 0; i < 1000; i++)
            {
                AddGameObject(new OutlineRectangle());
            }
            //*/
            ///*
            Sprite sprite = new Sprite("../../../Resources/DVD.ascw");
            //sprite.Save("../../../Resources/SpriteTest.ascw");
            AddGameObject(sprite);
            //*/
            /*
            Sprite sprite = new Sprite();
            AddGameObject(sprite);
            //*/
            /*
            TextBox textline = new TextBox("Hello World!\nuh oh\nGoodbye World");
            textline.SetColor(new Pixel(255, 0, 0, 0, 0, 255));
            AddGameObject(textline);
            OutlineRectangle rectangle = new OutlineRectangle(new Pixel(150, 0, 150, '@'), new Pixel(30, 30, 30, 0, 0, 0, '#'),new SFML.System.Vector2f(4,5),new SFML.System.Vector2f(2, 2));
            AddGameObject(rectangle);
            //*/
        }
    }
}