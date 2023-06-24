using GameEngine;
using MyGame.GameEngine;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    //lets the user control the camera
    //for debug purpouses only
    internal class CameraController : GameObject
    {
        public override void Update(Time elapsed)
        {
            float zoomSens = 10f;

            //rotation
            if (Keyboard.IsKeyPressed(Keyboard.Key.E)) { Game.CurrentScene.camera.angle += elapsed.AsSeconds() * 120; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q)) { Game.CurrentScene.camera.angle -= elapsed.AsSeconds() * 120; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) { Game.CurrentScene.camera.angle = 0; }

            //position
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { Game.CurrentScene.camera.position.Y -= elapsed.AsSeconds() * 1000 / Game.CurrentScene.camera.zoom.Y; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { Game.CurrentScene.camera.position.X -= elapsed.AsSeconds() * 1000 / Game.CurrentScene.camera.zoom.X; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { Game.CurrentScene.camera.position.Y += elapsed.AsSeconds() * 1000 / Game.CurrentScene.camera.zoom.Y; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { Game.CurrentScene.camera.position.X += elapsed.AsSeconds() * 1000 / Game.CurrentScene.camera.zoom.X; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) { Game.CurrentScene.camera.position = new Vector2f(0,0); }

            //zoom
            if (Keyboard.IsKeyPressed(Keyboard.Key.R)) { Game.CurrentScene.camera.zoom.X *= 1 + elapsed.AsSeconds() * 2; Game.CurrentScene.camera.zoom.Y *= 1 + elapsed.AsSeconds() * 2; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F)) { Game.CurrentScene.camera.zoom.X *= 1 - elapsed.AsSeconds() * 2; Game.CurrentScene.camera.zoom.Y *= 1 - elapsed.AsSeconds() * 2; }
            if (Game.CurrentScene.camera.zoom.X <= 0.01f) { Game.CurrentScene.camera.zoom.X = 0.01f; }
            if (Game.CurrentScene.camera.zoom.Y <= 0.01f) { Game.CurrentScene.camera.zoom.Y = 0.01f; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) { Game.CurrentScene.camera.zoom = new Vector2f(1, 1); }
        }
    }
}
