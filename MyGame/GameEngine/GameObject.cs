using MyGame.GameEngine;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


namespace GameEngine
{
    // This class represents objects with a position and/or shape
    internal class GameObject : Node
    {
        virtual public Vector2f _position { get; set; }

        // This function lets you specify a rectangle for collision checks.
        public virtual FloatRect GetCollisionRect()
        {
            return new FloatRect();
        }

        // What happens when this object collides with another object.
        public virtual void HandleCollision(GameObject otherGameObject)
        {
        }
    }
}