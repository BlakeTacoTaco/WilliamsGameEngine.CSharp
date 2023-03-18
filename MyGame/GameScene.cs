﻿using GameEngine;
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

            Player player = new Player();
            AddGameObject(player);

            TestButton testButton = new TestButton();
            AddUiElement(testButton);

            Inventory inventory = new Inventory(this);
            AddUiElement(inventory);
        }
    }
}