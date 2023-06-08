using GameEngine;
using MyGame.GameEngine;
using MyGame.GameEngine.Inventory;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations.UI.TackleFishing
{
    internal class TackleButton : Button
    {
        internal TackleFishMenu parent;
        /*public TackleButton(Vector2f scale, Vector2f position, TackleFishMenu parent)
        {
            this.parent = parent;
            _sprite = new Sprite();
            _sprite.Texture = Game.GetTexture("../../../Resources/samon.png");
            _sprite.Scale = scale;
            _sprite.Position = position;
            _sprite.Origin = new Vector2f(8, 8);
        }*/
        public virtual Item Catch()
        {
            return new Item(2, 1);
        }
        public override void ReleaseLeft()
        {
            parent.DealDamage();
            float dist = 60;
            float neg1 = 1;
            float neg2 = 1;
            if (Game.Random.Next(2) == 1) { neg1 = -1; }
            if (Game.Random.Next(2) == 1) { neg2 = -1; }
            Vector2f output = GetPosition() + new Vector2f(neg1 * dist, neg2 * dist);
            output = parent.KeepInBounds(output);
            SetPosition(output);
        }
        public override void Update(Time elapsed)
        {
            _sprite.Rotation += (Game.Random.Next(3) - 1) * elapsed.AsSeconds();
            float dist = elapsed.AsSeconds() * 100;
            float neg1 = 1;
            float neg2 = 1;
            if (Game.Random.Next(2) == 1) { neg1 = -1; }
            if (Game.Random.Next(2) == 1) { neg2 = -1; }
            Vector2f output = GetPosition() + new Vector2f((float)Game.Random.NextDouble() * neg1 * dist, (float)Game.Random.NextDouble() * neg2 * dist);
            output = parent.KeepInBounds(output);
            SetPosition(output);
        }
        public virtual void TouchTop() { }
        public virtual void TouchRight() { }
        public virtual void TouchLeft() { }
        public virtual void TouchBottom() { }
    }
}
