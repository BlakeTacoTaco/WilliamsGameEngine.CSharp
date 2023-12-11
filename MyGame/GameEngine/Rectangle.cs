using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;

namespace MyGame.GameEngine
{
    internal class Rectangle : GameObject
    {
        internal Pixel color;
        internal Vector2f size;
        public Vector2f position;
        public Vector2f velocity = new Vector2f(0, 0);
        private float acceleration = 20;
        private float maxSpeed = 2;
        public Rectangle(Pixel color, Vector2f size, Vector2f position)
        {
            this.color = color;
            this.size = size;
            this.position = position;
        }
        public Rectangle()
        {
            position = new Vector2f(Game.Random.Next(MyGame.WindowWidth), Game.Random.Next(MyGame.WindowHeight));
            color = new DualPixel(Game.Random.Next(255), Game.Random.Next(255), Game.Random.Next(255), Game.randomCharacters[Game.Random.Next(10)], Game.randomCharacters[Game.Random.Next(10)]);
            size = new Vector2f(Game.Random.Next(8) + 3, Game.Random.Next(8) + 3);
        }
        public override void Update(Time elapsed)
        {
            velocity.X += (Game.Random.Next(3) - 1) * elapsed.AsSeconds() * acceleration;
            velocity.Y += (Game.Random.Next(3) - 1) * elapsed.AsSeconds() * acceleration;
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
            if (position.X >= MyGame.WindowWidth) { position.X = MyGame.WindowWidth; velocity.X *= -1; }
            if (position.Y < 0) { position.Y = 0; velocity.Y *= -1; }
            if (position.Y >= MyGame.WindowHeight) { position.Y = MyGame.WindowHeight; velocity.Y *= -1; }
        }
        public override void Draw()
        {
            for (int i = 0; i < Math.Round(size.Y); i++)
            {
                for (int j = 0; j < Math.Round(size.X); j++)
                {
                    Game.screen.setPixel(j + (int)Math.Round(position.X), i + (int)Math.Round(position.Y), color);
                }
            }
        }
    }
}
