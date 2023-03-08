﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyGame
{
    internal class MouseObj : GameObject
    {
        Sprite _sprite = new Sprite();
        public MouseObj()
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            _sprite.Position = Game.GetMousePos();
        }
    }
}