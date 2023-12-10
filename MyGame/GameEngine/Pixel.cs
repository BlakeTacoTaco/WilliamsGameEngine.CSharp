using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class Pixel
    {
        /*
            "\x1b[48;5;" + s + "m" - set background color by index in table (0-255)

            "\x1b[38;5;" + s + "m" - set foreground color by index in table (0-255)

            "\x1b[48;2;" + r + ";" + g + ";" + b + "m" - set background by r,g,b values

            "\x1b[38;2;" + r + ";" + g + ";" + b + "m" - set foreground by r,g,b values
         */
        public int r;
        public int g;
        public int b;
        public int br;
        public int bg;
        public int bb;
        public char c;
        public Pixel()
        {
            r = 0;
            g = 0;
            b = 0;
            br = 0;
            bg = 0;
            bb = 0;
            c = ' ';
        }
        public Pixel(int r, int g, int b, char c)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            br = 0;
            bg = 0;
            bb = 0;
            this.c = c;
        }
        public Pixel(int r, int g, int b, int br, int bg, int bb, char c)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.br = br;
            this.bg = bg;
            this.bb = bb;
            this.c = c;
        }
        public Pixel(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            br = r;
            bg = g;
            bb = b;
            c = ' ';
        }
        public override string ToString()
        {
            if (Game.SquarePixels)
            {
                return "\x1b[48;2;" + br + ";" + bg + ";" + bb + "m" + "\x1b[38;2;" + r + ";" + g + ";" + b + "m" + c + c;
            }

            return "\x1b[48;2;" + br + ";" + bg + ";" + bb + "m" + "\x1b[38;2;" + r + ";" + g + ";" + b + "m" + c;
        }
    }
}
