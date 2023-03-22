using GameEngine;
using MyGame.GameEngine;
using MyGame.GameEngine.Inventory;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class FloorItem : KinematicBody
    {
        public Item _item;
        public Sprite _sprite;
        public Player player = null;
        private FloatRect _Detection;
        private const float range = 180;
        private const float speed = 20;
        public FloorItem(Item item, Vector2f position)
        {
            _item = item;
            _sprite = new Sprite();
            _sprite.Texture = ItemDat.GetTexture(item.ID);
            _sprite.Scale = new Vector2f(2, 2);
            this.position = position;
            _Detection = new FloatRect(position, new Vector2f(range, range));
            _sprite.Origin = new Vector2f(8, 8);
            friction = 100;
        }
        public override void Draw()
        {
            _sprite.Position = Game._Camera.ToLocalPos(position);
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            if (player != null)
            {
                velocity += (player.position - position) * elapsed.AsSeconds() * speed;
                //velocity.X += (range / (player.position.X - position.X)) * elapsed.AsSeconds() * speed;
            }
            Move(elapsed);
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if(otherGameObject is Player)
            {
                player = (Player)otherGameObject;
            }
            else
            {
                player = null;
            }
        }
        public override FloatRect GetCollisionRect()
        {
            Vector2f localCorner = Game._Camera.ToLocalPos(new Vector2f (position.Y - range / 2, position.X - range / 2));
            _Detection.Top = localCorner.Y;
            _Detection.Left = localCorner.X;
            return _Detection;
        }
    }
}
