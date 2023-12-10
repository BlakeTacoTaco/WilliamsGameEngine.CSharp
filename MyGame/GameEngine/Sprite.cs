using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class Sprite : GameObject
    {
        public Pixel[][] pixels;
        public Vector2f position;
        public Sprite(int x, int y)
        {
            pixels = new Pixel[y][];
            for(int i  = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Pixel[x];
                for(int j = 0; j < pixels[i].Length; j++)
                {
                    pixels[i][j] = new Pixel();
                }
            }
            position = new Vector2f();
        }
        public override void Draw()
        {
            for(int i = 0; i < pixels.Length; i++)
            {
                for(int j = 0; j < pixels[i].Length; j++)
                {
                    Game.screen.setPixel((int)Math.Round(position.X) + j, (int)Math.Round(position.Y) + i, pixels[j][i]);
                }
            }
        }
        public override void Update(Time elapsed)
        {
            
        }
    }
}
