using MyGame.GameEngine;
using GameEngine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations
{
    internal class TestButton : Button
    {
        public TestButton()
        {
            _sprite = new Sprite();
            _sprite.Texture = Game.GetTexture("../../../Resources/high quality grass.png");
            _sprite.Scale = new SFML.System.Vector2f(4, 4);
        }
        public void draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void ReleaseLeft()
        {
            Game._Mouse.inputEaten = true;
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
    }
}
