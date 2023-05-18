using GameEngine;
using MyGame.GameEngine;
using MyGame.GameEngine.Inventory;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations.UI.TackleFishing
{
    internal class TackleButton : Button
    {
        TackleFishMenu parent;
        public TackleButton(Vector2f scale, Vector2f position, TackleFishMenu parent)
        {
            this.parent = parent;
            _sprite = new Sprite();
            _sprite.Texture = Game.GetTexture("../../../Resources/samon.png");
            _sprite.Scale = scale;
            _sprite.Position = position;
            _sprite.Origin = new Vector2f(8, 8);
        }
        public override void ReleaseLeft()
        {
            parent.DealDamage();
            this.SetPosition(parent.GetNewButtonPos(this.GetPosition(),100));
        }
    }
}
