using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
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
        private const int chunkSize = 100;
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
                    setTile(i, j, 1);
                }
            }
        }
        private void setTile(int x, int y, int id)
        {
            if (id != -1)
            {
                tiles[x][y]._sprite.Texture = tileTypes[id];
                tiles[x][y]._type = id;
            }
            else { tiles[x][y]._type = -1; }
        }
        public override void Draw()
        {
            for (int i = 0; i < tiles.Length; ++i)
            {
                for (int j = 0; j < tiles[i].Length; ++j)
                {
                    if (tiles[i][j]._type != -1)
                    {
                        tiles[i][j]._sprite.Position = Game._Camera.ToLocalPos(tiles[i][j]._sprite.Position = new Vector2f(i * 16 * scale.X, j * 16 * scale.Y) + position);
                        tiles[i][j].Draw();
                    }
                }
            }
        }
        public override void Update(Time elapsed)
        {
            if (Game._Mouse.IsMouseJustPressed())
            {
                for (int i = 0; i < tiles.Length; ++i)
                {
                    for (int j = 0; j < tiles[i].Length; ++j)
                    {
                        if (tiles[i][j]._type == 0) { setTile(i, j, 1); }
                        else if (tiles[i][j]._type == 1) { setTile(i, j, 0); }
                    }
                }
            }
        }
    }
}
