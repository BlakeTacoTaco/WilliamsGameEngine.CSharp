using GameEngine;
using MyGame.GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class OutlineRectangle : Rectangle
    {
        internal Pixel edgeColor;
        public OutlineRectangle(Pixel color, Pixel edgeColor, Vector2f size, Vector2f position)
        {
            this.color = color;
            this.edgeColor = edgeColor;
            this.size = size;
            this.position = position;
        }
        public OutlineRectangle()
        {
            position = new Vector2f(Game.Random.Next(MyGame.WindowWidth), Game.Random.Next(MyGame.WindowHeight));
            color = new Pixel(Game.Random.Next(255), Game.Random.Next(255), Game.Random.Next(255), Game.randomCharacters[Game.Random.Next(10)]);
            edgeColor = new Pixel(color.r / 2, color.g / 2, color.b / 2, color.br / 3, color.bg / 3, color.bb / 3, '#');
            size = new Vector2f(Game.Random.Next(8) + 3, Game.Random.Next(8) + 3);
        }
        public override void Draw()
        {
            for (int i = 0; i < Math.Round(size.Y); i++)
            {
                for (int j = 0; j < Math.Round(size.X); j++)
                {
                    if (i == 0 || j == 0 || i == Math.Round(size.Y) - 1 || j == Math.Round(size.X) - 1)
                    {
                        Game.screen.setPixel(j + (int)Math.Round(position.X), i + (int)Math.Round(position.Y), edgeColor);
                        continue;
                    }
                    Game.screen.setPixel(j + (int)Math.Round(position.X), i + (int)Math.Round(position.Y), color);
                }
            }
        }
    }
}
