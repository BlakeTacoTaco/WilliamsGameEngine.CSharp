using GameEngine;
using MyGame.GameEngine.General_UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations.UI.TackleFishing
{
    internal class TackleLetter : GameObject
    {
        private Sprite sprite = new Sprite();
        public TackleLetter()
        {
            sprite.Texture = Game.GetTexture("../../../Resources/Attack.png");
            sprite.Scale = new Vector2f(4, 4);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(sprite);
        }
        public override void Update(Time elapsed)
        {
            
        }
        public override Vector2f GetPosition()
        {
            return sprite.Position;
        }
        public override void SetPosition(Vector2f position)
        {
            sprite.Position = position;
        }
        public override FloatRect GetCollisionRect()
        {
            return sprite.GetGlobalBounds();
        }
    }
}
