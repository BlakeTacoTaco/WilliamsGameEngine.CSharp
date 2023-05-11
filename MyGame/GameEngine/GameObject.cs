using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    // This class represents every object in your game, such as the player, enemies, and so on.
    abstract class GameObject
    {
        private bool _isCollisionCheckEnabled;

        private bool _isDead;

        // Using a set prevents duplicates.
        private readonly HashSet<string> _tags = new HashSet<string>();

        // Tags let you annotate your objects so you can identify them later
        // (such as "player").
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

        public virtual void MakeDead()
        {
            _isDead = true;
        }
        public virtual void UnDead()
        {
            _isDead = false;
        }

        // Update is called every frame. Use this to prepare to draw (move, perform AI, etc.).
        public abstract void Update(Time elapsed);

        // Draw is called once per frame. Use this to draw your object to the screen.
        public virtual void Draw()
        {
        }

        // This flag indicates whether this game object should be checked for collisions.
        // The more game objects in the scene that need to be checked, the longer it takes.
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

        // Use this to specify what happens when this object collides with another object.
        public virtual void HandleCollision(GameObject otherGameObject)
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
                    }
                    else
                    {
                        position.Y = pos.Y + box.Height;
                    }
                }
                else
                {
                    if (dist.X > 0)
                    {
                        position.X = pos.X - 16 * 4;
                    }
                    else
                    {
                        position.X = pos.X + box.Width;
                    }
                }
                SetPosition(position);
            }
        }
        public abstract Vector2f GetPosition();
        public abstract void SetPosition(Vector2f position);
    }
}