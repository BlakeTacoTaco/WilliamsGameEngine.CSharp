using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
        public Pixel(int r, int g, int b, int br, int bg, int bb)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.br = br;
            this.bg = bg;
            this.bb = bb;
            this.c = ' ';
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
        public string Save()
        {
            string output = "";
            output += Convert.ToString(r * 256 * 256 + g * 256 + b, 16);
            while(output.Length < 6)
            {
                output = "0" + output;
            }
            string output2 = Convert.ToString(br * 256 * 256 + bg * 256 + bb, 16);

            while (output2.Length < 6)
            {
                output2 = "0" + output2;
            }
            return output + '\t' + output2 + '\t' + c;
        }
        public Pixel(string encoded)
        {
            string[] splits = encoded.Split('\t');
            r = HexSnipReader(splits[0].Substring(0, 2));
            g = HexSnipReader(splits[0].Substring(2, 2));
            b = HexSnipReader(splits[0].Substring(4, 2));

            br = HexSnipReader(splits[1].Substring(0, 2));
            bg = HexSnipReader(splits[1].Substring(2, 2));
            bb = HexSnipReader(splits[1].Substring(4, 2));

            c = splits[2][0];
        }
        public int HexSnipReader(string hex)
        {
            int output = 0;
            for(int i = 0; i < hex.Length; i++)
            {
                output *= 16;
                int c = (int)hex[i];
                if(c >= (int)'0' && c <= (int)'9')
                {
                    output += c - (int)'0';
                }
                else
                {
                    output += c - (int)'a' + 10;
                }
            }
            Console.WriteLine(hex + ", " + output);
            return output;
        }
    }
}
