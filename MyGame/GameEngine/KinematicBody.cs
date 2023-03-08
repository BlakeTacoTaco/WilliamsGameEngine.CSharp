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
        internal float friction = 20;
        public void Move(Time elapsed)//moves the object and applies friction
        {
            float delta = elapsed.AsSeconds();
            
            //friction
            velocity -= new Vector2f(friction * delta,friction * delta);
            
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
