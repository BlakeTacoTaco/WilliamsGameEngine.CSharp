using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace MyGame.GameEngine
{
    internal abstract class Button : GameObject, MouseInterface
    {
        internal Sprite _sprite = new Sprite();
        internal bool hovered = false;
        internal bool lastHovered = false;
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            if (hovered)
            {
                lastHovered = hovered;
                hovered = false;
            }
        }
        public virtual void Hover()
        {
            hovered = true;
        }

        //left mouse
        public virtual void ReleaseLeft() { }
        public virtual void PressLeft() { }
        public virtual void HoldLeft() { }

        //right mouse
        public virtual void ReleaseRight() { }
        public virtual void PressRight() { }
        public virtual void HoldRight() { }

        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
    }
}
