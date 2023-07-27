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
        public bool _willCull = false;
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
        public readonly List<Drawable> drawQueue = new List<Drawable>();
        public Camera(View view)
        {
            this._view = view;
        }
        public Camera()
        {
            _view = new View();
            _zoom = new Vector2f(1, 1);
            _position = new Vector2f(0, 0);
            _rotation = 0;
        }
        //instead of drawing all of its children with the default camera this draws them with itself
        public override void Draw(Camera camera)
        {
            Game.CurrentScene.SubmitCamera(this);
            foreach (Node child in _children)
            {
                child.Draw(this);
            }
        }
        //draws everything that is using this camera
        public void DrawFrom()
        {

            //gets size of the camera
            Vector2f size = _view.Size;
            if (size.X > size.Y)
            {
                size.Y = size.X;
            }
            else
            {
                size.X = size.Y;
            }

            //makes size slightly longer because the dist from one corner to another on a unit square is root 2 (1.414)
            size *= 1.414f;

            FloatRect CamRegion = new FloatRect(_position - (size / 2), size);

            //sets the view of the renderwindow to apply camera settings
            Game.RenderWindow.SetView(_view);

            //draws all the drawables in the queue
            foreach (Drawable drawable in drawQueue)
            {
                //checks if it even is on screen before drawing it
                if (_willCull)
                {
                    if (drawable is Sprite)
                    {
                        Sprite sprite = (Sprite)drawable;
                        if (sprite.GetGlobalBounds().Intersects(CamRegion))
                        {
                            Game.RenderWindow.Draw(drawable);
                        }
                    }
                    else
                    {
                        Game.RenderWindow.Draw(drawable);
                    }
                }
                else
                {
                    Game.RenderWindow.Draw(drawable);
                }
            }
            drawQueue.Clear();
        }
        //lets objects submit their sprites for drawing
        public void DrawThis(Drawable drawable)
        {
            drawQueue.Add(drawable);
        }
    }
}
