﻿using GameEngine;
using SFML.Graphics;
using MyGame.GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace MyGame
{
    internal class Player : KinematicBody
    {
        Sprite _sprite = new Sprite();
        float speed = 200;
        public Player()
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
            friction = 50;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed) 
        {
            float delta = elapsed.AsSeconds();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { velocity.Y -= speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { velocity.Y += speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { velocity.X -= speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { velocity.X += speed * delta; }

            Move(elapsed);

            _sprite.Position = Game.Camera.ToLocalPos(position);
            Game.Camera.position = position - new Vector2f(800, 450);
        }
    }
}
