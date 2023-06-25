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
    internal class Camera : GameObject
    {
        private View view;
        private FloatRect _bounds;
        public override Vector2f _position
        {
            get { return view.Center; }
            set { view.Center = value; }
        }
        public Vector2f _zoom
        {
            get { return new Vector2f(view.Size.X / 1600, view.Size.Y / 900); }
            set { view.Size = new Vector2f(value.X * 1600, value.Y * 900); }
        }
        public float _rotation
        {
            get { return view.Rotation; }
            set { view.Rotation = value; }
        }
        public Camera(View view)
        {
            this.view = view;
        }
        public override void Update(Time elapsed)
        {
            Game.RenderWindow.SetView(view);
            _bounds = new FloatRect(view.Center.X - (view.Size.X / 2), view.Center.Y - (view.Size.X / 2), view.Size.X, view.Size.X);
        }
        public void Draw(Sprite sprite)
        {
            if (sprite.GetGlobalBounds().Intersects(_bounds))
            {
                Game.RenderWindow.Draw(sprite);
            }
        }
    }
}
