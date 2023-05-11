using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.General_UI
{
    static class BetterKeyboard
    {
        public static List<Keyboard.Key> activeKeys;
        static bool[] lastFrame = new bool[(int)Keyboard.Key.KeyCount];
        static bool[] thisFrame = new bool[(int)Keyboard.Key.KeyCount];
        static public bool IsKeyJustReleased(Keyboard.Key key) { return (lastFrame[(int)key] && !thisFrame[(int)key]); }
        static public bool IsKeyJustPressed(Keyboard.Key key) { return (!lastFrame[(int)key] && thisFrame[(int)key]); }
        static public void Update()
        {
            activeKeys = new List<Keyboard.Key> { };
            lastFrame = thisFrame;
            thisFrame = new bool[(int)Keyboard.Key.KeyCount];
            for (int i = 0; i < (int)Keyboard.Key.KeyCount; i++)
            {
                thisFrame[i] = Keyboard.IsKeyPressed((Keyboard.Key)i);
                if (thisFrame[i]) { activeKeys.Add((Keyboard.Key)i); }
                else if (lastFrame[i]) { activeKeys.Add((Keyboard.Key)i); }
            }
        }
        static public Keyboard.Key CharToKey(char input)
        {
            input = char.ToUpper(input);
            switch(input)
            {
                case 'A':
                    return Keyboard.Key.A;
                case 'B':
                    return Keyboard.Key.B;
                case 'C':
                    return Keyboard.Key.C;
                case 'D':
                    return Keyboard.Key.D;
                case 'E':
                    return Keyboard.Key.E;
                case 'F':
                    return Keyboard.Key.F;
                case 'G':
                    return Keyboard.Key.G;
                case 'H':
                    return Keyboard.Key.H;
                case 'I':
                    return Keyboard.Key.I;
                case 'J':
                    return Keyboard.Key.J;
                case 'K':
                    return Keyboard.Key.K;
                case 'L':
                    return Keyboard.Key.L;
                case 'M':
                    return Keyboard.Key.M;
                case 'N':
                    return Keyboard.Key.N;
                case 'O':
                    return Keyboard.Key.O;
                case 'P':
                    return Keyboard.Key.P;
                case 'Q':
                    return Keyboard.Key.Q;
                case 'R':
                    return Keyboard.Key.R;
                case 'S':
                    return Keyboard.Key.S;
                case 'T':
                    return Keyboard.Key.T;
                case 'U':
                    return Keyboard.Key.U;
                case 'V':
                    return Keyboard.Key.V;
                case 'W':
                    return Keyboard.Key.W;
                case 'X':
                    return Keyboard.Key.X;
                case 'Y':
                    return Keyboard.Key.Y;
                case 'Z':
                    return Keyboard.Key.Z;
                default:
                    return Keyboard.Key.Space;
            }
        }
    }
}
