using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class StillSprite : Node2D
    {
        Sprite sprite = new Sprite();
        public StillSprite()
        {
            sprite.Texture = Game.GetTexture("../../../Resources/samon.png");
            sprite.Scale = new Vector2f(4, 4);
        }
        //draws this sprite
        public override void DrawSelf(Camera camera)
        {
            sprite.Position = _GlobalPos;
            camera.DrawThis(sprite);
        }
    }
}
