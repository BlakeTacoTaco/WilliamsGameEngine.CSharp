using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame.GameEngine
{
    //this class is an abreviation of super sprite. its like a sprite but better and modified 
    internal class SSprite : GameObject
    {
        public Vector2f scale = new Vector2f(1, 1);          // the sprite's scale
        public Vector2f rotationCenter = new Vector2f(0, 0); //the sprite's rotation center
        public float    rotation = 0;                        // the sprite's global rotation
        public Sprite   sprite = new Sprite();               // the actual sprite

        public override void Draw()
        {
            Game.CurrentScene.camera.RelativeDraw(this);
            rotationCenter.X -= 1;
        }
        public override void Update(Time elapsed)
        {
            position.X += (Game.Random.Next(200) - 100) * elapsed.AsSeconds();
            position.Y += (Game.Random.Next(200) - 100) * elapsed.AsSeconds();
        }
    }
}
