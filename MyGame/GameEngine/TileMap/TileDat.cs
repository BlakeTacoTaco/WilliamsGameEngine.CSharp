using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;

namespace MyGame.GameEngine.TileMap
{
    static internal class TileDat
    {
        private static Texture[] textures;
        private static bool[] collisions;
        private const int tileAmount = 3;

        public static void Initialize()
        {
            textures = new Texture[tileAmount]
            {
                Game.GetTexture("../../../Resources/mouse test.png"),
                Game.GetTexture("../../../Resources/high quality grass.png"),
                Game.GetTexture("../../../Resources/water.png")
            };
            collisions = new bool[tileAmount]
            {
                true,
                false,
                false
            };
            
        }
        public static Texture GetTexture(int id)
        {
            if (id == -1) { return Game.GetTexture("../../../Resources/nothing.png"); }
            if (id >= tileAmount) { return Game.GetTexture("../../../Resources/null.png"); }
            if (id < -1) { return Game.GetTexture("../../../Resources/null.png"); }
            return textures[id];
        }
        public static bool HasCollisions(int id)
        {
            if (id == -1) { return false; }
            if (id >= tileAmount) { return false; }
            if (id < -1) { return false; }
            return collisions[id];
        }
    }
}
