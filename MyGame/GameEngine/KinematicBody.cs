using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class KinematicBody : GameObject
    {
        public Vector2f position = new Vector2f();
        internal Vector2f velocity = new Vector2f();
        internal float friction = 1;
        public void Move(Time elapsed)//moves the object and applies friction
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
            position += velocity * delta;
        }
        public override void Draw()//meant to be overidden
        {

        }
        public override void Update(Time elapsed)//meant to be overidden
        {
            
        }
        public override Vector2f GetPosition()
        {
            return position;
        }
        public override void SetPosition(Vector2f position)
        {
            this.position = position;
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("impass"))
            {
                Vector2f position = GetPosition();
                FloatRect box = otherGameObject.GetCollisionRect();
                Vector2f pos = otherGameObject.GetPosition();
                Vector2f dist = new Vector2f(pos.X - position.X - 8 * 4, pos.Y - position.Y - 8 * 4);
                dist += new Vector2f(box.Width, box.Height) / 2;

                if (Math.Abs(dist.X) < Math.Abs(dist.Y))
                {
                    if (dist.Y > 0)
                    {
                        position.Y = pos.Y - 16 * 4;
                        velocity.Y = 0;
                    }
                    else
                    {
                        position.Y = pos.Y + box.Height;
                        velocity.Y = 0;
                    }
                }
                else
                {
                    if (dist.X > 0)
                    {
                        position.X = pos.X - 16 * 4;
                        velocity.X = 0;
                    }
                    else
                    {
                        position.X = pos.X + box.Width;
                        velocity.X = 0;
                    }
                }
                SetPosition(position);
            }
        }
    }
}
