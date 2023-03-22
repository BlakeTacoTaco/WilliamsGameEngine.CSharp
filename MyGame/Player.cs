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
            _sprite.Origin = new Vector2f(8,8);
            scene.AddUiElement(inventory);
            SetCollisionCheckEnabled(true);
        }
        public override void Draw()
        {
            _sprite.Position = Game._Camera.ToLocalPos(position);
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
            Game._Camera.position = position - new Vector2f(800, 450);

            //inventory
            if (BetterKeyboard.IsKeyJustReleased(Keyboard.Key.I))
            {
                inventory.ToggleOpen();
            }
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
    }
}
