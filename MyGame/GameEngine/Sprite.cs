using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class Sprite : GameObject
    {
        public Pixel[][] pixels;
        public Vector2f position;
        public Sprite()
        {
            Pixel black = new Pixel();
            Pixel purple = new Pixel(255,0,255);
            pixels = new Pixel[][]{
                new Pixel[]{purple, black},
                new Pixel[]{black, purple }
            };
        }
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
            position = new Vector2f(0, 0);
        }
        public Sprite(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string[] dimensions  = reader.ReadLine().Split('\t');
            pixels = new Pixel[Convert.ToInt16(dimensions[1])][];
            int x = Convert.ToInt16(dimensions[0]);
            for(int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Pixel[x];
                for(int j = 0; j < pixels[i].Length; j++)
                {
                    pixels[i][j] = new Pixel(reader.ReadLine());
                }
            }
            position = new Vector2f(0, 0);
            reader.Close();
        }
        public override void Draw()
        {
            for(int i = 0; i < pixels.Length; i++)
            {
                for(int j = 0; j < pixels[i].Length; j++)
                {
                    Game.screen.setPixel((int)Math.Round(position.X) + j, (int)Math.Round(position.Y) + i, pixels[i][j]);
                }
            }
        }
        public override void Update(Time elapsed)
        {
            
        }
        public void Save(string filelocation)
        {
            StreamWriter writer = new StreamWriter(filelocation);
            writer.WriteLine(pixels[0].Length + "\t" + pixels.Length);
            for(int i = 0; i < pixels.Length; i++)
            {
                for(int j = 0; j < pixels[i].Length; j++)
                {
                    writer.WriteLine(pixels[i][j].Save());
                }
            }
            writer.Close();
        }
    }
}
