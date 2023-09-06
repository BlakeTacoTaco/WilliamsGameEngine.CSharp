using MyGame.GameEngine;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


namespace GameEngine
{
    // This class represents objects with a position and/or shape
    internal class Node2D : Node
    {
        virtual public Vector2f _localPos { get; set; }

        virtual public Vector2f _OffsetPos { get; set; }

        virtual public Vector2f _GlobalPos
        { 
            get { return _localPos + _OffsetPos; }
            set { _localPos = value - _OffsetPos; }
        }


        // This function lets you specify a rectangle for collision checks.
        public virtual FloatRect GetCollisionRect()
        {
            return new FloatRect();
        }

        //makes sure to update global pos
        public override void Update(Time elapsed)
        {
            base.Update(elapsed);
        }

        // What happens when this object collides with another object.
        public virtual void HandleCollision(Node2D otherGameObject)
        {
        }
    }
}