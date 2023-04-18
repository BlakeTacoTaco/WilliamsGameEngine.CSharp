using MyGame.GameEngine.ItemUses;
using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Item_Uses
{
    public class PlaceTile : ItemUse
    {
        private int tileType;
        public PlaceTile(int type) 
        {
            tileType = type;
        }
        public void Use()
        {
            if (Game.CurrentScene.tileMap.GetTile(Game.CurrentScene.tileMap.ToTilePos(Game.GetGlobalMousePos())) == null) { return; }
            if (Game.CurrentScene.tileMap.GetTile(Game.CurrentScene.tileMap.ToTilePos(Game.GetGlobalMousePos()))._type == tileType) { return; }
            Game.CurrentScene.tileMap.SetTile(Game.CurrentScene.tileMap.ToTilePos(Game.GetGlobalMousePos()), tileType);
            Game._Mouse.SetItem(new Inventory.Item(Game._Mouse.item.ID, Game._Mouse.item.amount - 1));
        }
    }
}
