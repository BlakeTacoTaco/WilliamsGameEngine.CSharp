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
        public Vector2f scale = new Vector2f(4,4);
        private Texture[] tileTypes;
        private const int chunkSize = 16;
        public Vector2f position = new Vector2f(0, 0);
        public Tile[][] tiles = new Tile[chunkSize][];
        public Chunk(Texture[] tiletypes)
        {
            this.tileTypes = tiletypes;
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile[chunkSize];
                for (int j = 0; j < tiles.Length; j++)
                {
                    tiles[i][j] = new Tile(scale);
                    setTile(i, j, ((i / j) % 9) % 2);
                }
            }
        }
        private void setTile(int x, int y, int id)
        {
            tiles[x][y]._sprite.Texture = tileTypes[id];
            tiles[x][y]._type = id;
        }
        public override void Draw()
        {
            for (int i = 0; i < tiles.Length; ++i)
            {
                for (int j = 0; j < tiles[i].Length; ++j)
                {
                    tiles[i][j]._sprite.Position = Game.Camera.ToLocalPos(tiles[i][j]._sprite.Position = new Vector2f(i * 16 * scale.X, j * 16 * scale.Y) + position);
                    tiles[i][j].Draw();
                }
            }
        }
        public override void Update(Time elapsed)
        {

        }
    }
}
