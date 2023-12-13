using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class TextLine : GameObject
    {
        private Pixel[] pixels;
        public Vector2f position;
        private string Text;
        private Pixel color = new Pixel();
        public string text
        {
            get
            {
                return Text;
            }
            set
            {
                SetText(value);
            }
        }
        public TextLine(string text)
        {
            this.text = text;
            position = new Vector2f(0, 0);
        }
        public override void Update(Time elapsed)
        {
            
        }
        public override void Draw()
        {
            int roundedX = (int)Math.Round(position.X);
            int roundedY = (int)Math.Round(position.Y);
            for(int i = 0; i < pixels.Length; i++)
            {
                Game.screen.setPixel(roundedX + i, roundedY, pixels[i]);
            }
        }
        private void SetText(string text)
        {
            this.Text = text;
            pixels = new Pixel[(text.Length / 2) + text.Length % 2];
            for(int i = 0; i < pixels.Length; i++)
            {
                if(i * 2 + 1 >= text.Length)
                {
                    pixels[i] = new DualPixel(text[i * 2], ' ');
                    continue;
                }
                pixels[i] = new DualPixel(text[i * 2], text[i * 2 + 1]);
            }
        }
        public void SetColor(Pixel color) 
        {
            this.color = color; 
            for(int i = 0; i < pixels.Length; i++)
            {
                pixels[i].r = color.r;
                pixels[i].g = color.g;
                pixels[i].b = color.b;
                pixels[i].br = color.br;
                pixels[i].bg = color.bg;
                pixels[i].bb = color.bb;
            }
        }
        public Pixel GetColor() { return color; }
    }
}
