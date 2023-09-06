using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Wobbler : Node2D
    {
        public override void UpdateSelf(Time elapsed)
        {
            _localPos += new Vector2f((Game.Random.Next(10) - 5) * elapsed.AsSeconds(), (Game.Random.Next(10) - 5) * elapsed.AsSeconds()) * 100;
        }
    }
}
