using GameEngine;
using SFML.Graphics;
using MyGame.GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using System.Transactions;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.General_UI;

namespace MyGame
{
    internal class Player : GameObject
    {
        private Inventory inventory;
        private Sprite _sprite = new Sprite();
        private float speed = 1000;
        public Vector2f position;
        public Player(Inventory inventory, Scene scene)
        {
            this.inventory = inventory;
            _sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
            _sprite.Scale = new Vector2f(4, 4);
            scene.AddUiElement(inventory);
            SetCollisionCheckEnabled(true);
            AssignTag("tilecollision");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed) 
        {
            float delta = elapsed.AsSeconds();

            //movement
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { position.Y -= speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { position.Y += speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { position.X -= speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { position.X += speed * delta; }
            //camera movement
            Game._Camera.position = position - new Vector2f(808, 458);

            //inventory
            if (BetterKeyboard.IsKeyJustReleased(Keyboard.Key.E))
            {
                inventory.ToggleOpen();
            }
            _sprite.Position = Game._Camera.ToLocalPos(position);
        }
        public override FloatRect GetCollisionRect()
        {
            FloatRect box = _sprite.GetGlobalBounds();
            box.Left = position.X;
            box.Top = position.Y;
            return box;
        }
        public void GiveItem(Item item)
        {
            inventory.AddItem(item);
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if(otherGameObject.HasTag("impass"))
            {
                FloatRect box = otherGameObject.GetCollisionRect();
                Vector2f pos = otherGameObject.GetPosition();
                Vector2f dist = new Vector2f(pos.X - position.X - 8 * 4, pos.Y- position.Y - 8 * 4);
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
               //else if (pos.X + box.Width > position.X)
               //{
               //    position.X = box.Width + pos.X;
               //}
               //
               //if (pos.Y > position.Y)
               //{
               //    position.Y = pos.Y - 16 * 4;
               //}
               //else if (pos.Y + box.Height > position.Y)
               //{
               //    position.Y = box.Height + pos.Y;
               //}
                Game._Camera.position = position - new Vector2f(808, 458);
            }
        }
        public override Vector2f GetPosition()
        {
            return position;
        }
    }
}
