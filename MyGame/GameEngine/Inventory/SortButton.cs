using System;
using SFML.Graphics;
using GameEngine;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Inventory
{
    internal class SortButton : Button
    {
        ButtonInventory _parent;
        public SortButton (ButtonInventory parent)
        {
            _parent = parent;
            _sprite = new Sprite();
            _sprite.Texture = Game.GetTexture("../../../Resources/sort button.png");
        }
        public override void ReleaseLeft()
        {
            if (_parent.open)
            {
                _parent.Sort();
                Game._Mouse.inputEaten = true;
            }
        }
        public override void Draw()
        {
            if (_parent.open)
            {
                base.Draw();
            }
        }
    }
}
