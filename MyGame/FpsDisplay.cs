using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class FpsDisplay : GameObject
    {
        Text text = new Text();
        public FpsDisplay()
        {
            text.Font = Game.GetFont("");
        }
        public override void Update(Time elapsed)
        {
            base.Update(elapsed);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(text);
        }
    }
}
