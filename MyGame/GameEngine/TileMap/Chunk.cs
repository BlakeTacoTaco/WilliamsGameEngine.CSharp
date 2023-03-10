using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.TileMap
{
    internal class Chunk : GameObject
    {
        private const int chunkSize = 16;
        public Vector2f position = new Vector2f(0, 0);
        public Sprite[][] tiles = new Sprite[chunkSize][];
        public Chunk()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Sprite[chunkSize];
                for (int j = 0; j < tiles.Length; j++)
                {
                    tiles[i][j] = new Sprite();
                    tiles[i][j].Texture = Game.GetTexture("../../../Resources/mouse test.png");
                }
            }
        }
        public override void Draw()
        {
            for (int i = 0; i < tiles.Length; ++i)
            {
                for (int j = 0; j < tiles[i].Length; ++j)
                {
                    tiles[i][j].Position = Game.Camera.ToLocalPos(tiles[i][j].Position = new Vector2f(i * 16, j * 16) + position);
                    Game.RenderWindow.Draw(tiles[i][j]);
                }
            }
        }
        public override void Update(Time elapsed)
        {

        }
    }
}
