using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class TextBox : GameObject
    {
        private TextLine[] lines;
        public string text { get; private set; }
        public Vector2f position {
            get
            {
                return lines[0].position;
            } 
            set
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].position = new Vector2f(position.X, position.Y + i);
                }
            }

        }
        public Vector2i size;
        public TextBox (string text)
        {
            SetText(text);
        }
        public override void Update(Time elapsed)
        {

        }
        public override void Draw()
        {
            for(int i = 0; i < lines.Length; i++)
            {
                lines[i].Draw();
            }
        }
        public void SetText(string text)
        {
            this.text = text;
            string[] strings = text.Split('\n');
            size.Y = strings.Length;
            size.X = 0;
            lines = new TextLine[strings.Length];
            for(int i = 0; i < strings.Length; i++)
            {
                if (size.X < strings[i].Length) { size.X = strings[i].Length; }
                lines[i] = new TextLine(strings[i]);
                lines[i].position = new Vector2f(position.X, position.Y + i);
            }
        }
        public void SetColor(Pixel color)
        {
            for(int i = 0; i < lines.Length; i++)
            {
                lines[i].SetColor(color);
            }
        }
    }
}
