using GameEngine;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.TileEntites;
using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations
{
    internal class Chest : UsableTileEntity
    {
        private ButtonInventory inventory;
        bool open = false;
        public override void Use(Player player)
        {
            open = !open;

            if (open) { player.OpenMenu(inventory); }
            else { player.CloseMenu(inventory); }
        }
        public Chest(Scene scene)
        {
            inventory = new ButtonInventory(scene, new Vector2f(10, 20 + 4 * 20 * 4), false);
            sprite = new Sprite();
            sprite.Texture = Game.GetTexture("../../../Resources/chest.png");
            sprite.Scale = new Vector2f(4, 4);
            tileDimensions = new Vector2i(1, 1);
            useDist = 16;
            topMargin = 6;
            Initialize(scene);
        }
        public override void OutCollision(Player player)
        {
            player.CloseMenu(inventory);
        }
    }
}
