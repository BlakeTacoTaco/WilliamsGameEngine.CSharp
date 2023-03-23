using System;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace MyGame.GameEngine.General_UI
{
    internal class NinePatch : GameObject
    {
        private Sprite[] patches;
        public Vector2f position;
        private Vector2f size;
        private Vector2f scale;
        private int topMargin;
        private int bottomMargin;
        private int leftMargin;
        private int rightMargin;
        private Vector2f scaleCoef;
        public NinePatch(Texture texture, int topMargin, int bottomMargin, int leftMargin, int rightMargin, Vector2f position, Vector2f size, Vector2f scale)
        {
            this.position = position;
            this.topMargin = topMargin;
            this.bottomMargin = bottomMargin;
            this.leftMargin = leftMargin;
            this.rightMargin = rightMargin;
            this.size = new Vector2f(4,4);
            this.scale = scale;
            patches = new Sprite[9];

            for (int i = 0; i < patches.Length; i++)
            {
                patches[i] = new Sprite();
                patches[i].Texture = texture;
                patches[i].Scale = scale;
            }

            scaleCoef.X = ((float)texture.Size.X + (float)rightMargin + (float)leftMargin) / (float)texture.Size.X;
            scaleCoef.Y = ((float)texture.Size.Y + (float)bottomMargin + (float)topMargin) / (float)texture.Size.Y;
            Console.WriteLine(scaleCoef);

            //cut textures into appropriate peices
            patches[0].TextureRect = new IntRect(0, 0, leftMargin, topMargin);
            patches[1].TextureRect = new IntRect(leftMargin, 0,(int)texture.Size.X - rightMargin - leftMargin, topMargin);
            patches[2].TextureRect = new IntRect((int)texture.Size.X - rightMargin, 0, rightMargin, topMargin);

            patches[3].TextureRect = new IntRect(0, topMargin, leftMargin, (int)texture.Size.Y - topMargin - bottomMargin);
            patches[4].TextureRect = new IntRect(leftMargin, topMargin, (int)texture.Size.X - rightMargin - leftMargin, (int)texture.Size.Y - topMargin - bottomMargin);
            patches[5].TextureRect = new IntRect((int)texture.Size.X - rightMargin, topMargin, rightMargin, (int)texture.Size.Y - topMargin - bottomMargin);

            patches[6].TextureRect = new IntRect(0, (int)texture.Size.Y - bottomMargin, leftMargin, bottomMargin);
            patches[7].TextureRect = new IntRect(leftMargin, (int)texture.Size.Y - bottomMargin, (int)texture.Size.X - rightMargin - leftMargin, bottomMargin);
            patches[8].TextureRect = new IntRect((int)texture.Size.X - rightMargin, (int)texture.Size.Y - bottomMargin, rightMargin, bottomMargin);
        }
        public override void Draw()
        {
            //puts sprites in appropriate locations
            patches[0].Position = new Vector2f(0,0);
            patches[1].Position = new Vector2f((leftMargin * scale.X), 0);
            patches[2].Position = new Vector2f(((patches[0].Texture.Size.X - rightMargin) * size.X), 0);

            patches[3].Position = new Vector2f(0, (topMargin * scale.Y));
            patches[4].Position = new Vector2f((rightMargin * scale.X), (topMargin * scale.Y));
            patches[5].Position = new Vector2f(((patches[0].Texture.Size.X - rightMargin) * size.X), (topMargin * scale.Y));

            patches[6].Position = new Vector2f(0, ((patches[0].Texture.Size.X - rightMargin) * size.Y));
            patches[7].Position = new Vector2f((rightMargin * scale.X), ((patches[0].Texture.Size.X - rightMargin) * size.Y));
            patches[8].Position = new Vector2f(((patches[0].Texture.Size.X - rightMargin) * size.X), ((patches[0].Texture.Size.X - rightMargin) * size.Y));

            //scales sprites properly
            patches[1].Scale = new Vector2f(size.X * scaleCoef.X, scale.Y);

            patches[3].Scale = new Vector2f(scale.X, size.Y * scaleCoef.Y);
            patches[4].Scale = new Vector2f(size.X * scaleCoef.X, size.Y * scaleCoef.Y);
            patches[5].Scale = new Vector2f(scale.X, size.Y * scaleCoef.Y);

            patches[7].Scale = new Vector2f(size.X * scaleCoef.X, scale.Y);

            for (int i =  0; i < patches.Length; i++)
            {
                //patches[i].Position = new Vector2f(scale.X * patches[i].Position.X, scale.Y * patches[i].Position.Y);
                patches[i].Position += position;
                Game.RenderWindow.Draw(patches[i]);
            }
        }
        public override void Update(Time elapsed)
        {
            size += new Vector2f(elapsed.AsSeconds(), elapsed.AsSeconds());
        }
    }
}
