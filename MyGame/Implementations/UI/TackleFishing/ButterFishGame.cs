using GameEngine;
using MyGame.GameEngine.Inventory;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations.UI.TackleFishing
{
    internal class ButterFishGame : TackleButton
    {
        private Vector2f velocity;
        private float friction = 5;
        public ButterFishGame(Vector2f scale, Vector2f position, TackleFishMenu parent)
        {
            this.parent = parent;
            _sprite = new Sprite();
            _sprite.Texture = Game.GetTexture("../../../Resources/ButterFish.png");
            _sprite.Scale = scale;
            _sprite.Position = position;
            _sprite.Origin = new Vector2f(8, 8);
            velocity = new Vector2f(100, 100);
        }
        public override Item Catch()
        {
            return new Item(5, 1);
        }
        public override void ReleaseLeft()
        {
            parent.DealDamage();
            float dist = 20;
            float neg1 = velocity.X / Math.Abs(velocity.X);
            float neg2 = velocity.Y / Math.Abs(velocity.Y);
            Vector2f output = GetPosition() + new Vector2f(neg1 * dist, neg2 * dist);
            output = parent.KeepInBounds(output);
            SetPosition(output);
            velocity += new Vector2f(neg1 * dist, neg2 * dist);
        }
        public override void Update(Time elapsed)
        {
            float delta = elapsed.AsSeconds();

            //friction
            if (Math.Abs(velocity.X) < 1) { velocity.X = 0; }
            else if (velocity.X > 0) { velocity.X += -friction * delta; }
            else if (velocity.X < 0) { velocity.X += friction * delta; }

            if (Math.Abs(velocity.Y) < 1) { velocity.Y = 0; }
            else if (velocity.Y > 0) { velocity.Y += -friction * delta; }
            else if (velocity.Y < 0) { velocity.Y += friction * delta; }

            //velocity
            SetPosition(GetPosition() + velocity * delta);

            SetPosition(parent.KeepInBounds(GetPosition()));
        }
        public override void TouchTop() { velocity.Y *= -1; }
        public override void TouchRight() { velocity.X *= -1; }
        public override void TouchLeft() { velocity.X *= -1; }
        public override void TouchBottom() { velocity.Y *= -1; }
    }
}
