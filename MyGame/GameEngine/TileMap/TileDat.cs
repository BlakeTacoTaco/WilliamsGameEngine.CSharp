using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame.GameEngine.TileMap
{
    static internal class TileDat
    {
        private static Texture[] textures;
        private static bool[] collisions;
        private static Vector2f[] spriteOffsets;
        private const int tileAmount = 4;

        public static void Initialize()
        {
            textures = new Texture[tileAmount]
            {
                Game.GetTexture("../../../Resources/mouse test.png"),
                Game.GetTexture("../../../Resources/high quality grass.png"),
                Game.GetTexture("../../../Resources/water.png"),
                Game.GetTexture("../../../Resources/dirtWall.png")
            };
            collisions = new bool[tileAmount]
            {
                true,
                false,
                false,
                true
            };
            spriteOffsets = new Vector2f[tileAmount]
            {
                new Vector2f(0, 0),
                new Vector2f(0, 0),
                new Vector2f(0, 0),
                new Vector2f(0,-8)
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
        public static Vector2f GetOffset(int id)
        {
            if (id == -1) { return new Vector2f(0,0); }
            if (id >= tileAmount) { return new Vector2f(0, 0); }
            if (id < -1) { return new Vector2f(0, 0); }
            return spriteOffsets[id];
        }
    }
}
