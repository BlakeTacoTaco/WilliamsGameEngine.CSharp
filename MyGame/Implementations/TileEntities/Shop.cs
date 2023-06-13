using GameEngine;
using MyGame.GameEngine.TileEntites;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations.TileEntities
{
    internal class Shop : UsableTileEntity
    {
        public Shop(Scene scene)
        {
            position = new Vector2f(200, 100);
            Initialize(scene);
            sprite = new Sprite();
            sprite.Texture = Game.GetTexture("../../../Resources/2x2table.png");
            sprite.Scale = new Vector2f(4, 4);
            tileDimensions = new Vector2i(2, 2);
            topMargin = 4;
        }
        public override void Use(Player player)
        {

        }
        public override void OutCollision(Player player)
        {
            
        }
    }
}
