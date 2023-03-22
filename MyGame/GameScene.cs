using GameEngine;
using MyGame.GameEngine.TileMap;
using SFML.System;
using SFML.Graphics;
using MyGame.GameEngine;
using MyGame.GameEngine.Inventory;

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

            TestKinematic testKinematic = new TestKinematic();
            AddGameObject(testKinematic);

            Inventory inventory = new Inventory(this);
            Player player = new Player(inventory, this);
            AddGameObject(player);

            FloorItem flooritem = new FloorItem(new Item(0, 3), new Vector2f(400, 400));
            AddGameObject(flooritem);
        }
    }
}