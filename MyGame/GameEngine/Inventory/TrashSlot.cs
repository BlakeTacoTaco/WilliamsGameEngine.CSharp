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
    internal class TrashSlot : Button
    {
        ButtonInventory _parent;
        public TrashSlot(ButtonInventory parent)
        {
            _parent = parent;
            _sprite = new Sprite();
            _sprite.Texture = Game.GetTexture("../../../Resources/trash slot.png");
        }
        public override void ReleaseLeft()
        {
            if (_parent.open)
            {
                Game._Mouse.SetItem(new Item(-1, 0));
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
