using GameEngine;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.TileEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Chest : UsableTileEntity
    {
        private Inventory inventory;
        public override void Use(Player player)
        {
            
        }
        public Chest(Scene scene)
        {
            inventory = new ButtonInventory(scene);
        }
    }
}
