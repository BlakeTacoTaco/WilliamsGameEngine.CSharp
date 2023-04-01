using GameEngine;
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
            if (Math.Abs(velocity.X) < 20) { velocity.X = 0; }
            else if (velocity.X > 0) { velocity.X += -friction * delta; }
            else if (velocity.X < 0) { velocity.X += friction * delta; }

            if (Math.Abs(velocity.Y) < 20) { velocity.Y = 0; }
            else if (velocity.Y > 0) { velocity.Y += -friction * delta; }
            else if (velocity.Y < 0) { velocity.Y += friction * delta; }

            //velocity
            position += velocity * delta;
        }
        public override void Draw()//meant to be overidden
        {
            base.Draw();
        }
        public override void Update(Time elapsed)//meant to be overidden
        {
            
        }
    }
}
