using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;
using MyGame.Implementations;

namespace MyGame.GameEngine.TileEntites
{
    internal abstract class UsableTileEntity : TileEntity
    {
        bool inRange;
        bool lastInRange;
        internal float useDist = 16;
        Player player;
        public abstract void Use(Player player);
        public virtual FloatRect GetUseCollision()
        {
            FloatRect box = GetCollisionRect();
            box.Left = position.X - useDist;
            box.Top = position.Y - useDist;
            box.Width += (useDist * 2);
            box.Height += (useDist * 2);
            return box;
        }
        public virtual void InCollision(Player player)
        {
            player.useEntity = this;
            inRange = true;
            this.player = player;
        }
        public abstract void OutCollision(Player player);
        public override void Update(Time elapsed)
        {
            if (lastInRange && !inRange)
            {
                OutCollision(player);
            }
            lastInRange = inRange;
            inRange = false;
        }
    }
}
