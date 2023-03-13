using GameEngine;
using MyGame.GameEngine.TileMap;
using SFML.System;
using SFML.Graphics;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {

            Chunk chunk = new Chunk(new Texture[] {
                Game.GetTexture("../../../Resources/mouse test.png"),
                Game.GetTexture("../../../Resources/high quality grass.png")    
            });
            AddGameObject(chunk);

            MouseObj mouseObj = new MouseObj();
            AddGameObject(mouseObj);

            TestKinematic testKinematic = new TestKinematic();
            AddGameObject(testKinematic);

            Player player = new Player();
            AddGameObject(player);
        }
    }
}