using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Explosion : GameObject
    {
        private float delta = 0;
        private SpriteRenderer _sprite;
        public Explosion(Vector2f position)
        {
            Sprite[] frames = new Sprite[9];
            for (int i = 1; i < frames.Length; i++)
            {
                frames[i] = new Sprite();
                frames[i].Texture = Game.GetTexture("../../../Resources/explosion0" + i + ".png");
            }

            List<List<int>> animations = new List<List<int>> { new List<int>()};
            for(int i = 0; i < frames.Length - 1; i++)
            {
                animations[0].Add(i);
            }

            _sprite = new SpriteRenderer(frames, position, 0, new Vector2f(1,1),new Vector2f(32,32), animations, 0.1f);

            _sprite.PlayAnimation(1,true);
        }
        public override void Draw()
        {
            _sprite.Draw(delta);
        }
        public override void Update(Time elapsed)
        {
            delta = elapsed.AsSeconds();
            if(!_sprite.playing) { this.MakeDead(); }
        }
    }
}
