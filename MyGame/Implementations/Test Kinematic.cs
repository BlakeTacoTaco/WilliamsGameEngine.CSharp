using MyGame.GameEngine;
using SFML.Graphics;
using SFML.System;
using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations
{
    internal class TestKinematic : KinematicBody
    {
        Sprite _sprite = new Sprite();
        public TestKinematic()
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
            AssignTag("tilecollision");
            _sprite.Scale = new Vector2f(4, 4);
            velocity = new Vector2f(500, 500);
            friction = 200;
        }
        public override void Draw()
        {
            _sprite.Position = Game._Camera.ToLocalPos(position);
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            Move(elapsed);
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            base.HandleCollision(otherGameObject);
        }
        public override FloatRect GetCollisionRect()
        {
            FloatRect box = _sprite.GetGlobalBounds();
            box.Left = position.X;
            box.Top = position.Y;
            return box;
        }
    }
}
