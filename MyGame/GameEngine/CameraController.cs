using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    //lets the user control the camera
    //for debug purpouses only
    internal class CameraController : Node2D
    {

        internal Camera _camera;
        public CameraController(Camera camera)
        {
            this._camera = camera;
        }
        public override void UpdateSelf(Time elapsed)
        {

            Vector2f movement = new Vector2f(0, 0);
            Vector2f zoom = _camera._zoom;
            float rotation = _camera._rot;

            //rotation
            if (Keyboard.IsKeyPressed(Keyboard.Key.E)) { rotation += elapsed.AsSeconds() * 120; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q)) { rotation -= elapsed.AsSeconds() * 120; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) { rotation = 0; }

            //position
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { movement.Y -= elapsed.AsSeconds() * 1000 * zoom.Y; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { movement.X -= elapsed.AsSeconds() * 1000 * zoom.X; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { movement.Y += elapsed.AsSeconds() * 1000 * zoom.Y; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { movement.X += elapsed.AsSeconds() * 1000 * zoom.X; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) { _camera._localPos = new Vector2f(0, 0); }

            //zoom
            if (Keyboard.IsKeyPressed(Keyboard.Key.R)) { zoom.X /= 1 + elapsed.AsSeconds() * 2; zoom.Y /= 1 + elapsed.AsSeconds() * 2; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F)) { zoom.X *= 1 + elapsed.AsSeconds() * 2; zoom.Y *= 1 + elapsed.AsSeconds() * 2; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) { zoom = new Vector2f(1, 1); }

            //toggle culling
            if (Keyboard.IsKeyPressed(Keyboard.Key.T)) { _camera._willCull = true;  }
            if (Keyboard.IsKeyPressed(Keyboard.Key.G)) { _camera._willCull = false; }

            //set everything
            _camera._zoom = zoom;
            _camera._localPos += movement;
            _camera._rot = rotation;
        }
    }
}
