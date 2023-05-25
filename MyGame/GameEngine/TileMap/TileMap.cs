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
using System.Globalization;
using System.IO;
using MyGame.Implementations;

namespace MyGame.GameEngine.TileMap
{
    internal class TileMap : GameObject
    {
        private Chunk[][] loadedChunks;
        private Vector2f GlobalPosition;
        private int loadSize;
        private int chunkSize = 16;
        public FloatRect tileRect = new FloatRect(0, 0, 16 * 4, 16 * 4);

        public TileMap()
        {
            GlobalPosition = new Vector2f(0, 0);
            loadSize = 10;
            loadedChunks = new Chunk[loadSize][];
            for (int i = 0; i< loadedChunks.Length; i++) 
            {
                loadedChunks[i] = new Chunk[loadSize];
                for(int j = 0; j < loadedChunks[i].Length; j++)
                {
                    loadedChunks[i][j] = new Chunk(chunkSize, new Vector2f(i * 16 * chunkSize * 4, j * 16 * chunkSize * 4));
                }
            }
        }
        public TileMap(string path) //load a tilemap from memory
        {
            StreamReader reader = new StreamReader(path);
            loadSize = Convert.ToInt32(reader.ReadLine());
            GlobalPosition = new Vector2f(0, 0);
            loadedChunks = new Chunk[loadSize][];
            for (int i = 0; i < loadedChunks.Length; i++)
            {
                loadedChunks[i] = new Chunk[loadSize];
                for (int j = 0; j < loadedChunks[i].Length; j++)
                {
                    loadedChunks[i][j] = new Chunk(chunkSize, new Vector2f(i * 16 * chunkSize * 4, j * 16 * chunkSize * 4));
                    for (int i2 = 0; i2 < 16; i2++)
                    {
                        string[] x = reader.ReadLine().Split(",");
                        int[] x2 = new int[16];
                        for(int j2 = 0; j2 < x.Length; j2++)
                        {
                            x2[j2] = Convert.ToInt32(x[j2]);
                            loadedChunks[i][j].SetTile(i2, j2, x2[j2]);
                        }
                    }
                }
            }
            reader.Close();
        }
        public void SaveTo(string path)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(loadSize);
            for (int i = 0; i < loadedChunks.Length; i++)
            {
                for (int j = 0; j < loadedChunks[i].Length; j++)
                {
                    for (int i2 = 0; i2 < 16; i2++)
                    {
                        for (int j2 = 0; j2 < 16; j2++)
                        {
                            //adds human readabilty but is removable without breaking anything
                            if(loadedChunks[i][j].GetTile(i2, j2)._type != -1 && loadedChunks[i][j].GetTile(i2, j2)._type < 10) { writer.Write(" "); }

                            //writes data and commas to file
                            writer.Write(loadedChunks[i][j].GetTile(i2, j2)._type);
                            if(j2 != 15) { writer.Write(","); }
                            else { writer.WriteLine(); }

                            //writes colored text to console (causes a lot of lag)
                            //Console.BackgroundColor = (ConsoleColor)(loadedChunks[i][j].GetTile(i2, j2)._type + 1);
                            //Console.Write("  ");
                            //if (j2 == 15) { Console.BackgroundColor = ConsoleColor.Black; Console.Write("\n"); }
                            //if (i2 == 15 && j2 == 15) { Console.WriteLine(); }
                        }
                    }
                }
            }
            writer.Close();
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
            return (TileDat.HasCollisions(tile._type));
        }
        public override void Update(Time elapsed) { }
        public override void Draw()
        {
            float chunkSize = 16 * 16 * 4;
            Vector2f halfChunk = new Vector2f(chunkSize / 2, chunkSize / 2);
            Vector2i cameraTopCorner = ToChunkPos(Game._Camera.position - halfChunk) + new Vector2i(0, 0);
            Vector2i cameraBottomCorner = ToChunkPos(Game._Camera.position + new Vector2f(1920, 1080)) + new Vector2i(1,1);
            if (cameraBottomCorner.X > loadedChunks.Length) { cameraBottomCorner.X = loadedChunks.Length; }
            if (cameraBottomCorner.Y > loadedChunks[0].Length) { cameraBottomCorner.Y = loadedChunks[0].Length; }
            if (cameraTopCorner.X < 0) { cameraTopCorner.X = 0; }
            if (cameraTopCorner.Y < 0) { cameraTopCorner.Y = 0; }
            for (int i = cameraTopCorner.X; i < cameraBottomCorner.X; i++)
            {
                for(int j= cameraTopCorner.Y; j < cameraBottomCorner.Y; j++)
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
        public TileEntity GetHighestTileEntity(Vector2f position)
        {
            Vector2i chunkPos = ToChunkPos(position);
            Vector2i offset = new Vector2i(0, 0);
            if (position.X % (chunkSize * 16 * 4) >  chunkSize * 8 * 4)
            {
                offset.X = 1;
            }
            else
            {
                offset.X = -1;
            }
            if (position.Y % (chunkSize * 16 * 4) > chunkSize * 8 * 4)
            {
                offset.Y = 1;
            }
            else
            {
                offset.Y = -1;
            }
            TileEntity temp;

            if (chunkPos.Y < loadedChunks.Length && chunkPos.Y < loadedChunks[0].Length && chunkPos.X >= 0 && chunkPos.Y >= 0)
            {
                temp = loadedChunks[chunkPos.X][chunkPos.Y].GetHighestTileEntity(position);
                if (temp != null) { return temp; }
            }
            if (chunkPos.X + offset.X < loadedChunks.Length && chunkPos.Y < loadedChunks[0].Length   &&   chunkPos.X + offset.X >= 0  && chunkPos.Y >= 0)
            {
                temp = loadedChunks[chunkPos.X + offset.X][chunkPos.Y].GetHighestTileEntity(position);
                if (temp != null) { return temp; }
            }
            if (chunkPos.Y < loadedChunks.Length && chunkPos.Y + offset.Y < loadedChunks[0].Length && chunkPos.X >= 0 && chunkPos.Y + offset.Y >= 0)
            {
                temp = loadedChunks[chunkPos.X][chunkPos.Y + offset.Y].GetHighestTileEntity(position);
                if (temp != null) { return temp; }
            }
            if (chunkPos.X + offset.X < loadedChunks.Length && chunkPos.Y  + offset.Y < loadedChunks[0].Length && chunkPos.X + offset.X >= 0 && chunkPos.Y  + offset.Y >= 0)
            {
                temp = loadedChunks[chunkPos.X + offset.X][chunkPos.Y + offset.Y].GetHighestTileEntity(position);
                return temp;
            }
            return null;
        }
        public void RemoveTileEntity(TileEntity tileEntity)
        {
            Vector2i chunkPos = ToChunkPos(tileEntity.position);
            if (chunkPos.X >= 0 && chunkPos.X < loadedChunks.Length)
            {
                if (chunkPos.Y >= 0 && chunkPos.Y < loadedChunks[chunkPos.X].Length)
                {
                    loadedChunks[chunkPos.X][chunkPos.Y].RemoveTileEntity(tileEntity);
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
