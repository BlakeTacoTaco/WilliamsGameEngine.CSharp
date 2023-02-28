using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Missile : GameObject
    {
        private float existedFor = 0;
        private Vector2f velocity;
        private readonly Sprite _sprite = new Sprite();
        private float edgeBuffer = MyGame.edgeBuffer;
        private Vector2f position;
        private float friction = 0;

        public Missile(float angle, float velocity, Vector2f position)
        {
            this.position = position;
            _sprite.Scale = new Vector2f(4, 4);
            _sprite.Texture = Game.GetTexture("../../../Resources/missile.png");
            _sprite.Origin = new Vector2f(2, 8);
            _sprite.Rotation = angle + 90;
            this.velocity = new Vector2f((float)Math.Cos(angle / 57.2957795f) * velocity, (float)Math.Sin(angle / 57.2957795f) * velocity);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        private void Detonate()
        {
            Explosion explosion = new Explosion(position);
            Game.CurrentScene.AddGameObject(explosion);
            this.MakeDead();
        }
        public override void Update(Time elapsed)
        {
            float delta = elapsed.AsSeconds();

            //keeps missile in bounds
            if (position.X < 0 - edgeBuffer) { position.X = MyGame.WindowWidth + edgeBuffer; }
            if (position.X > MyGame.WindowWidth + edgeBuffer) { position.X = -edgeBuffer; }
            if (position.Y < 0 - edgeBuffer) { position.Y = MyGame.WindowHeight + edgeBuffer; }
            if (position.Y > MyGame.WindowHeight + edgeBuffer) { position.Y = -edgeBuffer; }

            //makes it detonate after it exists for long enough
            existedFor += delta;
            if(existedFor > 0.75) { Detonate(); }

            //friction
            if (friction != 0)
            {
                velocity -= friction * delta * velocity; 

                //if velocity is super low it sets it to 0
                if (Math.Abs(velocity.X) <= 1) { velocity.X = 0; }
                if (Math.Abs(velocity.Y) <= 1) { velocity.Y = 0; }
            }

            position += velocity * delta;
            _sprite.Position = position;
        }
    }
}
