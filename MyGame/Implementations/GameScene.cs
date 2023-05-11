using GameEngine;
using MyGame.GameEngine.TileMap;
using SFML.System;
using SFML.Graphics;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.General_UI;

namespace MyGame.Implementations
{
    class GameScene : Scene
    {
        public GameScene()
        {
            TestKinematic testKinematic = new TestKinematic();
            AddGameObject(testKinematic);

            ButtonInventory inventory = new ButtonInventory(this, new Vector2f(10, 10), true);
            Player player = new Player(inventory, this);
            AddGameObject(player);
            for (int i = 0; i < 50; i++)
            {
                FloorItem flooritem = new FloorItem(new Item(Game.Random.Next(3), 99), new Vector2f(Game.Random.Next(2000), Game.Random.Next(2000)));
                AddGameObject(flooritem);
            }
            Table table = new Table(this);
            table.position = new Vector2f(16 * 4 * 32, 16 * 4 * 32);
            tileMap.AddTileEntity(table, this);

            Chest chest = new Chest(this);
            chest.position = new Vector2f(16 * 4 * 34, 16 * 4 * 32);
            tileMap.AddTileEntity(chest, this);

            DebugInfo debug = new DebugInfo();
            AddUiElement(debug);
        }
    }
}