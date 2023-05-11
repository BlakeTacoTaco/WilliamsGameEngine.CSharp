using GameEngine;
using Microsoft.VisualBasic.FileIO;
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

namespace MyGame.Implementations
{
    internal class FloorItem : KinematicBody
    {
        public Item _item;
        private Sprite _sprite;
        private Sprite _shadow;
        public Player player = null;
        private FloatRect _Detection;
        private const float range = 900f;
        private const float speed = 2000f;
        public FloorItem(Item item, Vector2f position)
        {
            _item = item;
            _sprite = new Sprite();
            _sprite.Texture = ItemDat.GetTexture(item.ID);
            _sprite.Scale = new Vector2f(2, 2);
            this.position = position;
            _Detection = new FloatRect(position, new Vector2f(range, range));
            _sprite.Origin = new Vector2f(8, 8);
            _shadow = new Sprite();
            _shadow.Texture = ItemDat.GetTexture(item.ID);
            _shadow.Position = position + new Vector2f(4, 4);
            _shadow.Scale = new Vector2f(2, 2);
            _shadow.Origin = new Vector2f(8, 8);
            _shadow.Color = Color.Black;
            friction = 500;
        }
        public override void Draw()
        {
            _shadow.Position = Game._Camera.ToLocalPos(position + new Vector2f(2, 2));
            Game.RenderWindow.Draw(_shadow);
            _sprite.Position = Game._Camera.ToLocalPos(position);
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            if (player != null)
            {
                //adds item to inventory if gets close enough
                FloatRect bounds = _sprite.GetLocalBounds();
                bounds.Top = position.Y;
                bounds.Left = position.X;
                bounds.Left -= bounds.Width;
                bounds.Top -= bounds.Height;
                if (player.GetCollisionRect().Intersects(bounds))
                {
                    player.GiveItem(_item);
                    if (_item.amount <= 0)
                    {
                        MakeDead();
                        return;
                    }
                }

                //moves item towards player
                Vector2f tempVelocity = new Vector2f(0, 0);
                Vector2f distanceToPlayer = new Vector2f(player.position.X - position.X, player.position.Y - position.Y) + new Vector2f(8 * 4, 8 * 4);

                if (distanceToPlayer.X > 0) { tempVelocity.X = 1; }
                else { tempVelocity.X = -1; }
                if (distanceToPlayer.Y > 0) { tempVelocity.Y = 1; }
                else { tempVelocity.Y = -1; }

                velocity += tempVelocity * elapsed.AsSeconds() * speed;
                friction = 500;
            }
            else { friction = 2000; }
            Move(elapsed);


            player = null;
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject is Player) { player = (Player)otherGameObject; }
            else { base.HandleCollision(otherGameObject); }
        }
        public override FloatRect GetCollisionRect()
        {
            Vector2f localCorner = new Vector2f(position.X - range / 2, position.Y - range / 2);
            _Detection.Top = localCorner.Y;
            _Detection.Left = localCorner.X;
            return _Detection;
        }
    }
}
