using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame
{
    internal class Meteor : GameObject
    {
        private float edgeBuffer = MyGame.edgeBuffer;
        private int health = 10;
        private Vector2f velocity;
        private Vector2f position;
        private readonly Sprite _sprite = new Sprite();
        public Meteor(Vector2f position, Vector2f velocity)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/meteor.png");
            this.velocity = velocity;
            this.position = position;
            AssignTag("meteor");
            SetCollisionCheckEnabled(true);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            float delta = elapsed.AsSeconds();
            position += velocity * delta;
            _sprite.Position = position;

            //keeps meteor in bounds
            if (position.X < 0 - edgeBuffer) { position.X = MyGame.WindowWidth + edgeBuffer; }
            if (position.X > MyGame.WindowWidth + edgeBuffer) { position.X = -edgeBuffer; }
            if (position.Y < 0 - edgeBuffer) { position.Y = MyGame.WindowHeight + edgeBuffer; }
            if (position.Y > MyGame.WindowHeight + edgeBuffer) { position.Y = -edgeBuffer; }
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            return;
        }
    }
}
