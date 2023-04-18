using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.TileMap
{
    internal class Collider : GameObject
    {
        FloatRect collider;
        public Collider(FloatRect collider)
        {
            this.collider = collider;
            AssignTag("impass");
        }
        public override FloatRect GetCollisionRect()
        {
            return collider;
        }
        public override void Draw()
        {
            base.Draw();
        }
        public override void Update(Time elapsed)
        {
            throw new NotImplementedException();
        }
        public override Vector2f GetPosition()
        {
            return new Vector2f(collider.Left, collider.Top);
        }
        public override void SetPosition(Vector2f position)
        {
            collider.Top = position.Y;
            collider.Left = position.X;
        }
    }
}
