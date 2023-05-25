using GameEngine;
using MyGame.GameEngine.TileEntites;
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
        public Vector2f scale = new Vector2f(4,4);                       //scale of tilemap
        public Vector2f position = new Vector2f(0, 0);                   //position of global tilemap
        public List<TileEntity> tileEntities = new List<TileEntity> { }; //store tileEntities
        private Vector2f[][] positions;
        public Tile[][] tiles;
        private int chunkSize;
        public Chunk(int chunkSize, Vector2f position)
        {
            this.position = position;
            this.chunkSize = chunkSize;
            tiles = new Tile[chunkSize][];                   //stores each tile
            positions = new Vector2f[chunkSize][];      //stores positions of each tile
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile[chunkSize];
                positions[i] = new Vector2f[chunkSize];
                for (int j = 0; j < tiles.Length; j++)
                {
                    tiles[i][j] = new Tile(scale);

                    SetTile(i, j, 1);
                    if(i * 16 * scale.X + position.X <= 16 * 14 * scale.X || j * 16 * scale.Y + position.Y <= 16 * 9 * scale.Y)
                    {
                        SetTile(i, j, 3);
                    }

                    positions[i][j] = new Vector2f (i * 16 * scale.X, j * 16 * scale.Y) + position;
                }
            }
            UpdatePositions();
        }
        public Chunk(int chunkSize, Vector2f position, Tile[][] tiles)
        {
            this.position = position;
            this.chunkSize = chunkSize;
            this.tiles = tiles;        
            positions = new Vector2f[chunkSize][];      
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile[chunkSize];
                positions[i] = new Vector2f[chunkSize];
                for (int j = 0; j < tiles.Length; j++)
                {
                    tiles[i][j] = new Tile(scale);

                    SetTile(i, j, 3);

                    positions[i][j] = new Vector2f(i * 16 * scale.X, j * 16 * scale.Y) + position;
                }
            }
            UpdatePositions();
        }
        public Tile GetTile(int x, int y)
        {
            return tiles[x][y];
        }
        public void SetTile(int x, int y, int id)
        {
            if (id != -1)
            {
                tiles[x][y]._sprite.Texture = TileDat.GetTexture(id);
                tiles[x][y]._sprite.TextureRect = new IntRect(new Vector2i(0, 0), (Vector2i)tiles[x][y]._sprite.Texture.Size);
                tiles[x][y]._type = id;
                positions[x][y] = new Vector2f(x * 16 * scale.X, y * 16 * scale.Y) + position + (TileDat.GetOffset(tiles[x][y]._type) * scale.X);
            }
            else { tiles[x][y]._type = -1; }
        }
        public override void Draw()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j]._type != -1)
                    {
                        tiles[i][j]._sprite.Position = Game._Camera.ToLocalPos(positions[i][j]);
                        tiles[i][j].Draw();
                    }
                }
            }
        }
        public override void Update(Time elapsed) { }
        public void UpdatePositions()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                for(int j = 0; j < positions[i].Length; j++)
                {
                    positions[i][j] = new Vector2f(i * 16 * scale.X, j * 16 * scale.Y) + position + (TileDat.GetOffset(tiles[i][j]._type) * scale.X);
                }
            }
        }
        public void AddTileEntity(TileEntity tileEntity)
        {
            tileEntities.Add(tileEntity);
        }
        public void RemoveTileEntity(TileEntity tileEntity)
        {
            tileEntities.Remove(tileEntity);
            tileEntity.MakeDead();
        }
        public TileEntity GetHighestTileEntity(Vector2f position)
        {
            for(int i = 0; i < tileEntities.Count; i++)
            {
                if (tileEntities[i].GetCollisionRect().Contains(position.X, position.Y))
                {
                    return tileEntities[i];
                }
            }
            return null;
        }
        public override Vector2f GetPosition()
        {
            return position;
        }
        public override void SetPosition(Vector2f position)
        {
            this.position = position;
            UpdatePositions();
        }
    }
}
