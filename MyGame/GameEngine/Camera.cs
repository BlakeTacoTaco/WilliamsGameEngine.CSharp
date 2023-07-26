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
        private View _view;
        public override Vector2f _position
        {
            get { return _view.Center; }
            set { _view.Center = value; }
        }
        public Vector2f _zoom
        {
            get { return new Vector2f(_view.Size.X / 1600, _view.Size.Y / 900); }
            set { _view.Size = new Vector2f(value.X * 1600, value.Y * 900); }
        }
        public float _rotation
        {
            get { return _view.Rotation; }
            set { _view.Rotation = value; }
        }
        public FloatRect _viewport
        {
            get { return _view.Viewport; }
            set { _view.Viewport = value; }
        }
        public Camera(View view)
        {
            this._view = view;
        }
        public override void Update(Time elapsed)
        {
            Game.RenderWindow.SetView(_view);
        }
        //when the the main loop causes the draw function to be called
        public override void Draw(Camera camera)
        {
            foreach (Node child in _children)
            {
                child.Draw(this);
            }
        }
    }
}
