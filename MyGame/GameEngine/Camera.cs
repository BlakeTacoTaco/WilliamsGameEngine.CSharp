using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public override void Update(Time elapsed)
        {
            
        }
        public void RelativeDraw(SSprite sprite) //draws the sprite relative to this camera
        {
            if (angle != 0)
            {
                sprite.sprite.Rotation = sprite.rotation - angle;
                sprite.sprite.Position = VectorMath.RotateVector(new Vector2f((sprite.position.X - position.X) * zoom.X, (sprite.position.Y - position.Y) * zoom.Y), -angle);
            }
            else
            {
                sprite.sprite.Rotation = sprite.rotation;
                sprite.sprite.Position = new Vector2f((sprite.position.X - position.X) * zoom.X, (sprite.position.Y - position.Y) * zoom.Y);
            }

            //set the scale of the sprite
            sprite.sprite.Scale = new Vector2f(sprite.scale.X * zoom.X, sprite.scale.Y * zoom.Y);

            //centers stuff
            sprite.sprite.Position += new Vector2f(800, 450);

            //draws the sprite
            Game.RenderWindow.Draw(sprite.sprite);
        }
    }
}
