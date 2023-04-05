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
        //this object is a rectangle that can be scaled without the border being messed up
        //its good for UI that all has the same background style but different dimensions
        //it works by cutting the texture into nine peices and only scaling the sides on one axis, the middle on both axis, and not scaling the corners at all

        //would look something like this
        /*
           -----------------------           ------------------------------------
           |      |       |      |           |      |                    |      |
           |      |       |      |           |      |                    |      |
           |------|-------|------|           |------|--------------------|------|
           |      |       |      |           |      |                    |      |
           |      |       |      |    -->    |      |                    |      |
           |      |       |      |           |      |                    |      |
           |------|-------|------|           |      |                    |      |
           |      |       |      |           |      |                    |      |
           |      |       |      |           |      |                    |      |
           -----------------------           |      |                    |      |
                                             |------|--------------------|------|
            see how the margins are the same |      |                    |      |
            thickness? (in practice you      |      |                    |      |
            wouldn't see the margins)        ------------------------------------
         */
        private Sprite[] patches;
        public Vector2f position;
        private Vector2f size;            //what the dimensions of the rectangle are (these are multiplied by the scale)
        public Vector2f scale;            //scale of rectangle (for controling how thick the margins are)
        private int topMargin;            //how wide the margins are on all sides of the rectangle
        private int bottomMargin; 
        private int leftMargin;
        private int rightMargin;
        private Vector2f[] localPositions; //stores the position of each patch relative to the corner of the object
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

            localPositions = new Vector2f[9];

            for (int i = 0; i < patches.Length; i++)
            {
                patches[i] = new Sprite();
                patches[i].Texture = texture;
                patches[i].Scale = scale;
            }


            //cut textures into peices
            patches[0].TextureRect = new IntRect(0, 0, leftMargin, topMargin);
            patches[1].TextureRect = new IntRect(leftMargin, 0,(int)texture.Size.X - rightMargin - leftMargin, topMargin);
            patches[2].TextureRect = new IntRect((int)texture.Size.X - rightMargin, 0, rightMargin, topMargin);

            patches[3].TextureRect = new IntRect(0, topMargin, leftMargin, (int)texture.Size.Y - topMargin - bottomMargin);
            patches[4].TextureRect = new IntRect(leftMargin, topMargin, (int)texture.Size.X - rightMargin - leftMargin, (int)texture.Size.Y - topMargin - bottomMargin);
            patches[5].TextureRect = new IntRect((int)texture.Size.X - rightMargin, topMargin, rightMargin, (int)texture.Size.Y - topMargin - bottomMargin);

            patches[6].TextureRect = new IntRect(0, (int)texture.Size.Y - bottomMargin, leftMargin, bottomMargin);
            patches[7].TextureRect = new IntRect(leftMargin, (int)texture.Size.Y - bottomMargin, (int)texture.Size.X - rightMargin - leftMargin, bottomMargin);
            patches[8].TextureRect = new IntRect((int)texture.Size.X - rightMargin, (int)texture.Size.Y - bottomMargin, rightMargin, bottomMargin);

            SetSize(size);
        }
        public override void Draw()
        {
            //draws all the patches
            for (int i =  0; i < patches.Length; i++)
            {
                patches[i].Position = localPositions[i] + position;
                Game.RenderWindow.Draw(patches[i]);
            }
        }
        public override void Update(Time elapsed) 
        { 
            //sets size based on mouse position for debug purpouses
            //SetSize(Game.GetMousePos() / 4);
        }
        public void SetSize(Vector2f size)
        {
            this.size = size;

            //puts sprites in appropriate locations
            localPositions[0] = new Vector2f(0, 0);
            localPositions[1] = new Vector2f(leftMargin, 0);
            localPositions[2] = new Vector2f(this.size.X - rightMargin, 0);

            localPositions[3] = new Vector2f(0, topMargin);
            localPositions[4] = new Vector2f(leftMargin, topMargin);
            localPositions[5] = new Vector2f(this.size.X - rightMargin, topMargin);

            localPositions[6] = new Vector2f(0, this.size.Y - bottomMargin);
            localPositions[7] = new Vector2f(leftMargin, this.size.Y - bottomMargin);
            localPositions[8] = new Vector2f(this.size.X - rightMargin, this.size.Y - bottomMargin);

            //scales sprites
            Vector2f scales = new Vector2f((float)(this.size.X - rightMargin - rightMargin) / (float)(patches[0].Texture.Size.X - rightMargin - leftMargin) * scale.X, (float)(this.size.Y - bottomMargin - topMargin) / (float)(patches[0].Texture.Size.Y - topMargin - bottomMargin) * scale.Y);
            
            patches[0].Scale = scale;
            patches[1].Scale = new Vector2f(scales.X, scale.Y);
            patches[2].Scale = scale;

            patches[3].Scale = new Vector2f(scale.X, scales.Y);
            patches[4].Scale = scales;
            patches[5].Scale = new Vector2f(scale.X, scales.Y);

            patches[6].Scale = scale;
            patches[7].Scale = new Vector2f(scales.X, scale.Y);
            patches[8].Scale = scale;

            //adjusts local positions based on scale
            for (int i = 0; i < localPositions.Length; i++)
            {
                localPositions[i] = new Vector2f(localPositions[i].X * scale.X, localPositions[i].Y * scale.Y);
            }
        }
        public override Vector2f GetPosition()
        {
            return position;
        }
    }
}
