using SFML.Graphics;
using SFML.System;
using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MyGame.GameEngine
{
    internal class Camera
    {
        public Vector2f position = new Vector2f();
        //draws game object relative to camera
        public Vector2f ToLocalPos(Vector2f pos)
        {
            return pos - position;
        }
    }
}
