using GameEngine;
using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.General_UI
{
    internal class HorizontalProgressBar : GameObject
    {
        internal NinePatch background;
        internal Sprite bar;
        internal float progress;
        internal int margin;
        public HorizontalProgressBar(Vector2f position, float progress, int margin, Texture bar, Texture background, Vector2f scale)
        {
            this.bar = new Sprite();
            this.bar.Texture = bar;
            this.bar.Position = position + new Vector2f((int)margin * scale.X, (int)margin * scale.Y);
            this.bar.Scale = scale;
            SetProgress(progress);

            this.background = new NinePatch(background, margin, margin, margin, margin, position, new Vector2f(bar.Size.X * scale.X, bar.Size.Y * scale.Y) + (Vector2f)new Vector2i(margin * 2 * (int)scale.X, margin * 2 * (int)scale.Y), scale);

            this.margin = margin;
        }
        public override void Draw()
        {
            background.Draw();
            Game.RenderWindow.Draw(bar);
        }
        public void SetProgress(float progress)
        {
            if(progress < 0) { progress = 0; }
            if(progress > 1) { progress = 1; }
            this.progress = progress;
            Vector2i finalSize = (Vector2i)bar.Texture.Size;
            finalSize.X = (int)(progress * (float)finalSize.X);
            bar.TextureRect = new IntRect(0,0,finalSize.X,finalSize.Y);
        }
        public override void Update(Time elapsed)
        {
        }
        public override Vector2f GetPosition()
        {
            return background.GetPosition();
        }
        public override void SetPosition(Vector2f position)
        {
            background.SetPosition(position);
            this.bar.Position = position + new Vector2f((int)margin, (int)margin);
        }
    }
}
