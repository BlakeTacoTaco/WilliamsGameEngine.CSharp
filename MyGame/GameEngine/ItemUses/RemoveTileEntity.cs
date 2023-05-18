using System;
using GameEngine;
using MyGame.GameEngine.TileMap;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.GameEngine.TileEntites;

namespace MyGame.GameEngine.ItemUses
{
    public class RemoveTileEntity : ItemUse
    {
        public void Use()
        {
            if (Game._Mouse.IsLeftJustReleased())
            {
                TileEntity entity = Game.CurrentScene.tileMap.GetHighestTileEntity(Game.GetGlobalMousePos());
                if(entity != null)
                {
                    Game.CurrentScene.tileMap.RemoveTileEntity(entity);
                }
            }
        }
    }
}
