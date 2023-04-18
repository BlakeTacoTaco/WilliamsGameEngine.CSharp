using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using MyGame.GameEngine.TileEntites;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace MyGame.GameEngine.TileMap
{
    internal class TileMap : GameObject
    {
        private Chunk[][] loadedChunks;
        private Vector2f GlobalPosition;
        private int loadSize;
        private Texture[] tileTypes;
        private int chunkSize = 16;
        public FloatRect tileRect = new FloatRect(0, 0, 16 * 4, 16 * 4);
        private bool[] collisions;

        public TileMap()
        {
            tileTypes = new Texture[] {
                Game.GetTexture("../../../Resources/mouse test.png"),
                Game.GetTexture("../../../Resources/high quality grass.png")
            };
            collisions = new bool[]
            {
                false,
                true
            };
            GlobalPosition = new Vector2f(0, 0);
            loadSize = 3;
            loadedChunks = new Chunk[loadSize][];
            for (int i = 0; i< loadedChunks.Length; i++) 
            {
                loadedChunks[i] = new Chunk[loadSize];
                for(int j = 0; j < loadedChunks[i].Length; j++)
                {
                    loadedChunks[i][j] = new Chunk(tileTypes, chunkSize);
                    loadedChunks[i][j].position = new Vector2f(i * 16 * chunkSize * 4, j * 16 * chunkSize * 4);
                    loadedChunks[i][j].UpdatePositions();
                }
            }
        }
        public Tile GetTile(Vector2i position)
        {
            Vector2i chunkPos = new Vector2i(position.X / chunkSize, position.Y / chunkSize);
            if (position.X >= 0 && position.Y >= 0)
            {
                if (chunkPos.X >= 0 && chunkPos.X < loadedChunks.Length)
                {
                    if (chunkPos.Y >= 0 && chunkPos.Y < loadedChunks[chunkPos.X].Length)
                    {
                        return loadedChunks[chunkPos.X][chunkPos.Y].GetTile(position.X % 16, position.Y % 16);
                    }
                }
            }
            return null;
        }
        public bool HasCollisions(Vector2i position)
        {
            Tile tile = GetTile(position);
            if (tile == null) { return false; }
            if (tile._type == -1) { return false; }
            return !(collisions[tile._type]);
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
            Vector2f offseted = new Vector2f(0,0) + GlobalPosition;
            if(original.X < 0) { offseted.X -= 16 * 4; }
            if(original.Y < 0) { offseted.Y -= 16 * 4; }
            return new Vector2f((((int)original.X) / (16 * 4)) * 16 * 4 , (((int)original.Y) / (16 * 4)) * 16 * 4) + offseted;
        }
        public Vector2i ToTilePos(Vector2f original)
        {
            Vector2f dontbreak = new Vector2f(original.X, original.Y);
            dontbreak -= GlobalPosition;
            return (Vector2i)dontbreak / (16 * 4);
        }
        public Vector2i ToChunkPos(Vector2f original)
        {
            Vector2i tilepos = ToTilePos(original);
            return (tilepos / chunkSize);
        }
        public void SetTile(Vector2i position, int tileId)
        {
            Vector2i chunkPos = new Vector2i(position.X / chunkSize, position.Y / chunkSize);
            if (position.X >= 0 && position.Y >= 0)
            {
                if (chunkPos.X >= 0 && chunkPos.X < loadedChunks.Length)
                {
                    if (chunkPos.Y >= 0 && chunkPos.Y < loadedChunks[chunkPos.X].Length)
                    {
                        loadedChunks[chunkPos.X][chunkPos.Y].SetTile(position.X % 16, position.Y % 16, tileId);
                    }
                }
            }
        }
        public void AddTileEntity(TileEntity tileEntity, Scene scene)
        {
            Vector2i chunkPos = ToChunkPos(tileEntity.position);
            if (chunkPos.X >= 0 && chunkPos.X < loadedChunks.Length)
            {
                if (chunkPos.Y >= 0 && chunkPos.Y < loadedChunks[chunkPos.X].Length)
                {
                    loadedChunks[chunkPos.X][chunkPos.Y].AddTileEntity(tileEntity);
                    scene.AddGameObject(tileEntity);
                }
            }
        }
        public override Vector2f GetPosition()
        {
            return GlobalPosition;
        }
        public override void SetPosition(Vector2f position)
        {
            GlobalPosition = position;
            for (int i = 0; i < loadedChunks.Length; i++)
            {
                for (int j = 0; j < loadedChunks[i].Length; j++)
                {
                    loadedChunks[i][j].position = new Vector2f(i * 16 * chunkSize * 4, j * 16 * chunkSize * 4);
                    loadedChunks[i][j].UpdatePositions();
                }
            }
        }
        public void CollisionCheck(GameObject colider)
        {
            FloatRect box = colider.GetCollisionRect();
            Vector2f topCorner = new Vector2f(box.Left, box.Top);
            Vector2f bottomCorner = topCorner + new Vector2f(box.Width, box.Height);

            Vector2i tileTopCorner = ToTilePos(topCorner) + new Vector2i(-1,-1);
            Vector2i tileBottomCorner = ToTilePos(bottomCorner) + new Vector2i(1, 1);

            for (int i = tileTopCorner.X; i < tileBottomCorner.X; i++)
            {
                for (int j = tileTopCorner.Y; j < tileBottomCorner.Y; j++)
                {
                    if (HasCollisions(new Vector2i(i, j)))
                    {
                        FloatRect collision = tileRect;
                        collision.Top = j * 16 * 4;
                        collision.Left = i * 16 * 4;
                        Collider tileCol = new Collider(collision);
                        if (box.Intersects(collision))
                        colider.HandleCollision(tileCol);
                    }
                }
            }
        }
    }
}
