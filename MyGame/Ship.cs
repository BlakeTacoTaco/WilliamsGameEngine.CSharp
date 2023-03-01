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
        private SpriteRenderer _sprite;

        //movement related stuff
        private float velocity = 0;
        private float acceleration = 1800;
        private float maxVelocity = 900;
        private float rotSpeed = 360;
        private float angle = 0;
        private float edgeBuffer = MyGame.edgeBuffer;
        private float friction = 6f;
        private float delta = 0;
        private float recoil = 600;

        // Constructors
        public Ship()
        {
            Sprite[] frameArray = new Sprite[11];
            for(int i = 0; i < frameArray.Length; i++)
            {
                frameArray[i] = new Sprite();
                frameArray[i].Texture = Game.GetTexture("../../../Resources/ship" + (i + 1) + ".png");
            }

            List<List<int>> animations = new List<List<int>>() { new List<int>() { } };
            for(int i = 0; i < 5; i++)
            {
                animations[0].Add(10);
            }
            for (int i = 0; i < 10 ; i++)
            {
                animations[0].Add(i);
                //animations[0].Add(i);
                //animations[0].Add(i);
                //animations[0].Add(i);
            }

            _sprite = new SpriteRenderer(frameArray, new Vector2f(0, 0), 9, new Vector2f(4, 4), new Vector2f(16,16), animations,0.4f);
            _sprite.position = new Vector2f(100, 100);
        }
        // functions overridden from GameObject:
        public override void Draw()
        {
            _sprite.Draw(delta);
        }
        public override void Update(Time elapsed)
        {
            Vector2f pos = _sprite.position;
            delta = elapsed.AsSeconds();
            bool input = false; //checks if any velocity inputs were detected

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { velocity += delta * acceleration; input = true; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { velocity -= delta * acceleration /2 ; input = true; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { angle -= delta * rotSpeed; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { angle += delta * rotSpeed; }

            //makes angle go back down if you do a bunch of sequential turns
            angle %= 360;

            //max speed
            if (velocity > maxVelocity) { velocity = maxVelocity; }
            if (velocity < -maxVelocity / 2) { velocity = -maxVelocity / 2; }

            //drag
            if (friction != 0 && !input) 
            { 
                if (velocity != 0) { velocity -= friction * delta * velocity; }

                //if velocity is super low it sets it to 0
                if (Math.Abs(velocity) <= 1) { velocity = 0; }
            }

            //deciding velocity based on speed and angle
            float radAngle = angle / 57.2957795f;
            Vector2f finalVelocity = new Vector2f((float)Math.Cos(radAngle) * velocity, (float)Math.Sin(radAngle) * velocity);

            //aplies the velocity
            pos -= finalVelocity * delta;

            //keeps player in bounds
            if (pos.X < 0 - edgeBuffer) { pos.X = MyGame.WindowWidth + edgeBuffer; }
            if (pos.X > MyGame.WindowWidth + edgeBuffer) { pos.X = -edgeBuffer; }
            if (pos.Y < 0 - edgeBuffer) { pos.Y = MyGame.WindowHeight + edgeBuffer; }
            if (pos.Y > MyGame.WindowHeight + edgeBuffer) { pos.Y = -edgeBuffer; }

            //final aplication
            _sprite.position = pos;
            _sprite.angle = angle - 90;



            //shooting
            bool shot = false;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Space)) 
            {
                shot = _sprite.PlayAnimation(0, false);
                if (shot)
                {
                    Missile missile = new Missile(angle + 180, 600, pos);
                    velocity -= recoil;
                    Game.CurrentScene.AddGameObject(missile);
                }
            }
        }
    }
}
