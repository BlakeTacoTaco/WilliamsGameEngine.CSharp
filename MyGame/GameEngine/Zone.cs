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
    class Zone : Scene
    {
        public FloatRect cameraBounds;

        internal override void DrawGameObjects()
        {
            if (Game._Camera.position.X < cameraBounds.Left) { Game._Camera.position.X = 0; }
            if (Game._Camera.position.Y < cameraBounds.Top) { Game._Camera.position.Y = 0; }
            if (Game._Camera.position.X + Game.RenderWindow.Size.X > cameraBounds.Left + cameraBounds.Width) { Game._Camera.position.X = cameraBounds.Left + cameraBounds.Width - Game.RenderWindow.Size.X; }
            if (Game._Camera.position.Y + Game.RenderWindow.Size.Y > cameraBounds.Top + cameraBounds.Height) { Game._Camera.position.Y = cameraBounds.Top  + cameraBounds.Height - Game.RenderWindow.Size.Y; }
            base.DrawGameObjects();
        }
    }
}
