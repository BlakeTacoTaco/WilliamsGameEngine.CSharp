using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    // This class represents every object in your game, such as the player, enemies, and so on.
    abstract class GameObject
    {
        virtual public Vector2f _position { get; set; }

        private bool _isCollisionCheckEnabled;

        private bool _isDead;

        // Using a set prevents duplicates.
        private readonly HashSet<string> _tags = new HashSet<string>();

        // Tags annotate your objects so you can identify them later
        public void AssignTag(string tag)
        {
            _tags.Add(tag);
        }

        public bool HasTag(string tag)
        {
            return _tags.Contains(tag);
        }

        // "Dead" game objects will be removed from the scene.
        public bool IsDead()
        {
            return _isDead;
        }

        public void MakeDead()
        {
            _isDead = true;
        }

        // Update is called every frame.
        public virtual void Update(Time elapsed) { }
        // Draw is called once per frame.
        public virtual void Draw() { }

        // This flag indicates whether this game object should be checked for collisions.
        public bool IsCollisionCheckEnabled()
        {
            return _isCollisionCheckEnabled;
        }

        public void SetCollisionCheckEnabled(bool isCollisionCheckEnabled)
        {
            _isCollisionCheckEnabled = isCollisionCheckEnabled;
        }

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