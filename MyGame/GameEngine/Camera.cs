using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame.GameEngine
{
    internal class Camera : GameObject //draws objects relative to where it is
    {
        //the amount the camera is zoomed in
        internal Vector2f zoom = new Vector2f(1, 1);
        //angle in degrees the camera is rotated
        internal float angle = 0;
        public Camera() { position = new Vector2f(0, 0); }
        public Camera(Vector2f position)
        {
            this.position = position;
        }
        public void RelativeDraw(SSprite sprite) //draws the sprite relative to this camera
        {
            sprite.sprite.Position = position + sprite.gPosition;
        }
    }
}
