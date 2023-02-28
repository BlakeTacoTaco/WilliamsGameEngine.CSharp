using GameEngine;
using SFML.System;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            Ship ship = new Ship();
            AddGameObject(ship);

            Meteor meteor = new Meteor(new Vector2f(10, 40), new Vector2f(600, 600));
            AddGameObject(meteor);
        }
    }
}