using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame.GameEngine.TileMap
{
    internal class Tile : GameObject
    {
        public int _type;
        public Sprite _sprite = new Sprite();
        public Tile(Vector2f scale)
        {
            _sprite.Scale = scale;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            
        }
        public override Vector2f GetPosition()
        {
            return _sprite.Position;
        }
        public override void SetPosition(Vector2f position)
        {
            _sprite.Position = position;
        }
    }
}