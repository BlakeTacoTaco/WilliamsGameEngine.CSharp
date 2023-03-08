﻿using MyGame.GameEngine;
using SFML.Graphics;
using SFML.System;
using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class TestKinematic : KinematicBody
    {
        Sprite _sprite = new Sprite();
        public TestKinematic()
        {
            this._sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
            this._sprite.Scale = new Vector2f(4,4);
            velocity = new Vector2f(500, 2);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            Move(elapsed);
            _sprite.Position = position;
        }
    }
}