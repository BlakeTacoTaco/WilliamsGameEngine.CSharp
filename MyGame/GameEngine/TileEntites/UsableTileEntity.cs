using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace MyGame.GameEngine.TileEntites
{
    internal abstract class UsableTileEntity : TileEntity
    {
        private float useDist = 16;
        public Player player;
        public abstract void Use(Player player);
        public virtual FloatRect GetUseCollision()
        {
            FloatRect box = sprite.GetLocalBounds();
            box.Left = position.X - useDist;
            box.Top = position.Y - useDist;
            box.Width += useDist * 2;
            box.Left += useDist * 2;
            return box;
        }
    }
}
