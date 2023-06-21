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
        List<float> lastFrames = new List<float>();
        public FpsDisplay()
        {
            text.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");
            for (int i = 0; i < 30; i++)
            {
                lastFrames.Add(0);
            }
        }
        public override void Update(Time elapsed)
        {
            base.Update(elapsed);
            lastFrames.RemoveAt(0);
            lastFrames.Add(1f / elapsed.AsSeconds());

            float total = 0;
            for(int i = 0; i < lastFrames.Count; i++)
            {
                total += lastFrames[i];
            }

            text.DisplayedString = "fps:" + Convert.ToString(Math.Round(total / lastFrames.Count));
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(text);
        }
    }
}
