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
        static bool[] lastFrame = new bool[(int)Keyboard.Key.KeyCount];
        static bool[] thisFrame = new bool[(int)Keyboard.Key.KeyCount];
        static public bool IsKeyJustReleased(Keyboard.Key key) { return (lastFrame[(int)key] && !thisFrame[(int)key]); }
        static public bool IsKeyJustPressed(Keyboard.Key key) { return (!lastFrame[(int)key] && thisFrame[(int)key]); }
        static public void Update()
        {
            lastFrame = thisFrame;
            thisFrame = new bool[(int)Keyboard.Key.KeyCount];
            for (int i = 0; i < (int)Keyboard.Key.KeyCount; i++)
            {
                thisFrame[i] = Keyboard.IsKeyPressed((Keyboard.Key)i);
            }
        }
    }
}
