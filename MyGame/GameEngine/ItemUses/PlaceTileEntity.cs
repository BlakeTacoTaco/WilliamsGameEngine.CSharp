using GameEngine;
using MyGame.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.ItemUses
{
    internal class PlaceTileEntity : ItemUse
    {
        public void Use()
        {
            if (!Game._Mouse.IsLeftPressed()) { return; }
            if (Game._Mouse.inputEaten) { return; }
            Table table = new Table(Game.CurrentScene);
            table.position = Game.CurrentScene.tileMap.SnapToTile(Game.GetGlobalMousePos());
            if (Game.CurrentScene.tileMap.AddTileEntity(table, Game.CurrentScene))
            {
                Game._Mouse.SetItem(new Inventory.Item(Game._Mouse.item.ID, Game._Mouse.item.amount - 1));
            }
        }
    }
}
