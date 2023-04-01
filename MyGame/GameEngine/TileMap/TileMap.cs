using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace MyGame.GameEngine.TileMap
{
    internal class TileMap : GameObject
    {
        private Chunk[][] loadedChunks;
        private Vector2f GlobalPosition;
        private int loadSize;
        private Texture[] tileTypes;

        public TileMap()
        {
            tileTypes = new Texture[] {
                Game.GetTexture("../../../Resources/mouse test.png"),
                Game.GetTexture("../../../Resources/high quality grass.png")
            };
            GlobalPosition = new Vector2f(0, 0);
            loadSize = 3;
            loadedChunks = new Chunk[loadSize][];
            for (int i = 0; i< loadedChunks.Length; i++) 
            {
                loadedChunks[i] = new Chunk[loadSize];
                for(int j = 0; j < loadedChunks[i].Length; j++)
                {
                    loadedChunks[i][j] = new Chunk(tileTypes);
                    loadedChunks[i][j].position = new Vector2f(i * 16 * 16 * 4, j * 16 * 16 * 4);
                    loadedChunks[i][j].UpdatePositions();
                }
            }
        }
        public override void Update(Time elapsed) { }
        public override void Draw()
        {
            for (int i = 0; i < loadedChunks.Length; i++)
            {
                for(int j= 0; j < loadedChunks[i].Length; j++)
                {
                    loadedChunks[i][j].Draw();
                }
            }
        }
        public Vector2f SnapToTile(Vector2f original)
        {
            Vector2f offseted = original + GlobalPosition;
            return new Vector2f((((int)original.X) / (16 * 4)) * 16 * 4 , (((int)original.Y) / (16 * 4)) * 16 * 4);
        }
        public void SetTile(Vector2i position, int tileId)
        {
            Vector2i chunkPos = new Vector2i(position.X / 16, position.Y / 16);
            if (loadedChunks[chunkPos.X][chunkPos.Y] != null)
            {
                loadedChunks[chunkPos.X][chunkPos.Y].SetTile(position.X / 16, position.Y / 16, tileId);
            }
        }
    }
}
