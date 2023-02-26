using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Ship : GameObject
    {
        private readonly Sprite _sprite = new Sprite();

        //movement related stuff
        private float velocity = 0;
        private float acceleration = 20;
        private float maxVelocity = 15;
        private float rotSpeed = 360;
        private float angle = 0;
        private float edgeBuffer = 60;
        private float friction = 2f;

        // Constructors
        public Ship()
        {
            _sprite.Texture = Game.GetTexture("Resources/ship.png");
            _sprite.Position = new Vector2f(100, 100);
            _sprite.Origin = new Vector2f(42, 60);
        }
        // functions overridden from GameObject:
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            Vector2f pos = _sprite.Position;
            float delta = elapsed.AsSeconds();
            bool input = false; //checks if any movement inputs were detected

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { velocity += delta * acceleration; input = true; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { velocity -= delta * acceleration; input = true; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { angle -= delta * rotSpeed; input = true; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { angle += delta * rotSpeed; input = true; }

            //max speed
            if (velocity > maxVelocity) { velocity = maxVelocity; }

            //drag
            if (friction != 0 && !input) 
            { 
                if (velocity > 0) { velocity -= friction * delta * maxVelocity; }
                if (velocity < 0) { velocity += friction * delta * maxVelocity; }

                //if velocity is super low it sets it to 0
                if (Math.Abs(velocity) <= 1) { velocity = 0; }
            }


            //deciding velocity based on speed and angle
            float radAngle = angle / 57.2957795f;
            Vector2f finalVelocity = new Vector2f((float)Math.Cos(radAngle) * velocity, (float)Math.Sin(radAngle) * velocity);

            //aplies the velocity
            pos -= finalVelocity;

            //keeps player in bounds
            if (pos.X < 0 - edgeBuffer) { pos.X = MyGame.WindowWidth + edgeBuffer; }
            if (pos.X > MyGame.WindowWidth + edgeBuffer) { pos.X = -edgeBuffer; }
            if (pos.Y < 0 - edgeBuffer) { pos.Y = MyGame.WindowHeight + edgeBuffer; }
            if (pos.Y > MyGame.WindowHeight + edgeBuffer) { pos.Y = -edgeBuffer; }


            _sprite.Position = pos;
            _sprite.Rotation = angle - 180;
        }
    }
}
