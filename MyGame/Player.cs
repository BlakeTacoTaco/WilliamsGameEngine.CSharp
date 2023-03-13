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

namespace MyGame
{
    internal class Player : GameObject
    {
        Sprite _sprite = new Sprite();
        float speed = 200;
        Vector2f position;
        public Player()
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
        }
        public override void Draw()
        {
            _sprite.Position = Game.Camera.ToLocalPos(position);
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed) 
        {
            float delta = elapsed.AsSeconds();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { position.Y -= speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { position.Y += speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { position.X -= speed * delta; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { position.X += speed * delta; }

            Game.Camera.position = position - new Vector2f(800, 450);
        }
    }
}
