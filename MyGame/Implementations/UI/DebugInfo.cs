using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations
{
    internal class DebugInfo : GameObject
    {
        internal Text text;
        int listIndex = 0;
        float[] list = new float[60];
        public DebugInfo()
        {
            text = new Text();
            text.FillColor = Color.White;
            text.OutlineColor = Color.Black;
            text.OutlineThickness = 4;
            text.CharacterSize = 30;
            text.Position = new Vector2f(1920 - 180, 10);
            text.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(text);
        }
        public override void SetPosition(Vector2f position)
        {
            throw new NotImplementedException();
        }
        public override Vector2f GetPosition()
        {
            throw new NotImplementedException();
        }
        public override void Update(Time elapsed)
        {
            listIndex++;
            if (listIndex >= list.Length) { listIndex = 0; }
            list[listIndex] = 1 / elapsed.AsSeconds();
            float average = 0;
            for (int i = 0; i < list.Length; i++)
            {
                average += list[i];
            }
            average /= list.Length;
            text.DisplayedString = "FPS: " + Math.Round(average);
        }
    }
}
