using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class Screen
    {
        private Pixel[][] pixels;
        public Screen(int width, int height)
        {
            pixels = new Pixel[height][];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Pixel[width];
                for (int j = 0; j < pixels[i].Length; j++)
                {
                    pixels[i][j] = new Pixel(0,0,0);
                }
            }
        }
        public Pixel getPixel (int x, int y)
        {
            if (y < 0 || y >= pixels.Length) { return null; }
            if(x < 0 || x >= pixels[y].Length) { return null; }
            return pixels[y][x];
        }
        public void setPixel (int x, int y, Pixel pixel1)
        {
            if (y < 0 || y >= pixels.Length) { return; }
            if (x < 0 || x >= pixels[y].Length) { return; }
            pixels[y][x] = pixel1;
            /*
            Pixel pixel2 = pixels[y][x];
            pixel2.r = pixel1.r;
            pixel2.g = pixel1.g;
            pixel2.b = pixel1.b;
            pixel2.br = pixel1.br;
            pixel2.bg = pixel1.bg;
            pixel2.bb = pixel1.bb;
            pixel2.c = pixel1.c;
            */
        }
        public void DrawScreen()
        {
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < pixels.Length; i++)
            {
                string line = "";
                for (int j = 0; j < pixels[i].Length; j++)
                {
                    Pixel pix = pixels[i][j];
                    if (pix.r > 255) { pix.r = 255; }
                    if (pix.g > 255) { pix.g = 255; }
                    if (pix.b > 255) { pix.b = 255; }
                    if (pix.br > 255) { pix.br = 255; }
                    if (pix.bg > 255) { pix.bg = 255; }
                    if (pix.bb > 255) { pix.bb = 255; }
                    line += pix;
                    pixels[i][j] = new Pixel(0, 0, 0);
                }
                //line = "\x1b[48;2;" + 0 + ";" + 0 + ";" + 0 + "m" + "\x1b[38;2;" + 255 + ";" + 255 + ";" + 255 + "m|" + line + "\x1b[48;2;" + 0 + ";" + 0 + ";" + 0 + "m" + "\x1b[38;2;" + 255 + ";" + 255 + ";" + 255 + "m|";
                Console.WriteLine(line);
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
