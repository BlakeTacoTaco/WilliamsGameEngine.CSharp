using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace MyGame.GameEngine
{
    //this class is an abreviation of super sprite. its like a sprite but better and modified 
    internal class SSprite
    {
        public Vector2f gPosition = new Vector2f(0, 0);      // the sprite's global position
        public Vector2f scale = new Vector2f(1, 1);          // the sprite's scale
        public Vector2f rotationCenter = new Vector2f(0, 0); //the sprite's rotation center
        public float    rotation = 0;                        // the sprite's global rotation
        public Sprite   sprite = new Sprite();               // the actual sprite
    }
}
