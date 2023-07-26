using GameEngine;
using MyGame.GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Net.Http.Headers;

namespace MyGame
{
    class GameScene : Scene
    {
        public GameScene()
        {
            root.AddChild(new CameraController(_camera));
        }
    }
}