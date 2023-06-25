using GameEngine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class StillSprite : GameObject
    {
        Sprite sprite;
        public StillSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }
        public override void Draw()
        {
            Game.CurrentScene.camera.Draw(sprite);
        }
    }
}
