using GameEngine;
using SFML.System;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            MouseObj mouseObj = new MouseObj();
            AddGameObject(mouseObj);

            TestKinematic testKinematic = new TestKinematic();
            AddGameObject(testKinematic);
        }
    }
}