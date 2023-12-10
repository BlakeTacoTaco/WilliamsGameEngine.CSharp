using GameEngine;
using MyGame.GameEngine;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class FireFly: GameObject
    {
        private Pixel color;
        private Pixel glowColor;
        public Vector2f position;
        private Vector2f velocity;
        private float acceleration = 1;
        private float maxSpeed = 5;
        public FireFly()
        {
            position = new Vector2f(Game.Random.Next(MyGame.WindowWidth), Game.Random.Next(MyGame.WindowHeight));
            color = new Pixel((int)((position.X / MyGame.WindowWidth) * 64) + Game.Random.Next(30), Game.Random.Next(64), (int)((position.Y / MyGame.WindowHeight) * 64) + Game.Random.Next(30));
            glowColor = new Pixel(color.r, color.g, color.b);
        }
        public override void Update(Time elapsed)
        {
            velocity.X += ((Game.Random.Next(3) - 1) * elapsed.AsSeconds()) * acceleration;
            velocity.Y += ((Game.Random.Next(3) - 1) * elapsed.AsSeconds()) * acceleration;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                velocity.Y -= elapsed.AsSeconds() * 200;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                velocity.Y += elapsed.AsSeconds() * 200;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                velocity.X -= elapsed.AsSeconds() * 200;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                velocity.X += elapsed.AsSeconds() * 200;
            }
            if (velocity.X > maxSpeed) { velocity.X = maxSpeed; }
            if (velocity.X < -maxSpeed) { velocity.X = -maxSpeed; }
            if (velocity.Y > maxSpeed) { velocity.Y = maxSpeed; }
            if (velocity.Y < -maxSpeed) { velocity.Y = -maxSpeed; }
            position.X += velocity.X * elapsed.AsSeconds();
            position.Y += velocity.Y * elapsed.AsSeconds();
            if (position.X < 0) { position.X = 0; velocity.X *= -1; }
            if(position.X >= MyGame.WindowWidth) { position.X = MyGame.WindowWidth; velocity.X *= -1; }
            if (position.Y < 0) { position.Y = 0; velocity.Y *= -1; }
            if (position.Y >= MyGame.WindowHeight) { position.Y = MyGame.WindowHeight; velocity.Y *= -1; }
        }
        public override void Draw()
        {
            Game.screen.setPixel((int)position.X, (int)position.Y, color);
            Game.screen.setPixel((int)position.X + 1, (int)position.Y, glowColor);
            Game.screen.setPixel((int)position.X - 1, (int)position.Y, glowColor);
            Game.screen.setPixel((int)position.X, (int)position.Y + 1, glowColor);
            Game.screen.setPixel((int)position.X, (int)position.Y - 1, glowColor);

            Game.screen.setPixel((int)position.X + 1, (int)position.Y + 1, glowColor);
            Game.screen.setPixel((int)position.X - 1, (int)position.Y + 1, glowColor);
            Game.screen.setPixel((int)position.X + 1, (int)position.Y - 1, glowColor);
            Game.screen.setPixel((int)position.X - 1, (int)position.Y - 1, glowColor);
        }
    }
}
