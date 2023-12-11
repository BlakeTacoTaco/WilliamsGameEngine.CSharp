using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class DualPixel : Pixel
    {
        public char c2;
        public DualPixel()
        {
            r = 0;
            g = 0;
            b = 0;
            br = 0;
            bg = 0;
            bb = 0;
            c = ' ';
            c2 = ' ';
        }
        public DualPixel(int r, int g, int b, char c, char c2)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            br = 0;
            bg = 0;
            bb = 0;
            this.c = c;
            this.c2 = c2;
        }
        public DualPixel(int r, int g, int b, int br, int bg, int bb, char c, char c2)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.br = br;
            this.bg = bg;
            this.bb = bb;
            this.c = c;
            this.c2 = c2;
        }
        public DualPixel(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            br = r;
            bg = g;
            bb = b;
            c = ' ';
            c2 = ' ';
        }
        public override string ToString()
        {
            if (Game.SquarePixels)
            {
                return "\x1b[48;2;" + br + ";" + bg + ";" + bb + "m" + "\x1b[38;2;" + r + ";" + g + ";" + b + "m" + c + c2;
            }
            return "\x1b[48;2;" + br + ";" + bg + ";" + bb + "m" + "\x1b[38;2;" + r + ";" + g + ";" + b + "m" + c;
        }
    }
}
