using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal static class VectorMath
    {
        //rotates the original vector counterclockwise around 0,0
        public static Vector2f RotateVector(Vector2f original, float degrees)
        {
            float radians = degrees * (float)Math.PI / 180;
            float temp_x = original.X * (float)Math.Cos(radians) - (original.Y) * (float)Math.Sin(radians);
            original.Y = (original.X * (float)Math.Sin(radians) + (original.Y) * (float)Math.Cos(radians));
            original.X = temp_x;//avoids using the new x in the equation for the z
            return original;
        }
    }
}
