using GameEngine;
using SFML.System;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            Ship ship = new Ship();
            AddGameObject(new Ship());
            AddGameObject(new Ship());

            for (int i = 0; i < 100; i++)
            {
                Meteor meteor = new Meteor(new Vector2f(Game.Random.Next((int)Game.RenderWindow.Size.X), Game.Random.Next((int)Game.RenderWindow.Size.X)), new Vector2f(Game.Random.Next(600) - 300, Game.Random.Next(600) - 300));
                AddGameObject(meteor);
            }
        }
    }
}