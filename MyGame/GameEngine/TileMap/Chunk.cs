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
        private Texture[] tileTypes;                                     //all the textures (should probably make a TileDat class                                    //how wide/tall is the chunk
        public Vector2f position = new Vector2f(0, 0);                   //position of global tilemap
        public List<TileEntity> tileEntities = new List<TileEntity> { }; //store tileEntities
        private Vector2f[][] positions;
        public Tile[][] tiles;
        private int chunkSize;
        public Chunk(Texture[] tiletypes, int chunkSize)
        {
            this.chunkSize = chunkSize;
            tiles = new Tile[chunkSize][];                   //stores each tile
            positions = new Vector2f[chunkSize][];      //stores positions of each tile
            this.tileTypes = tiletypes;
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile[chunkSize];
                positions[i] = new Vector2f[chunkSize];
                for (int j = 0; j < tiles.Length; j++)
                {
                    tiles[i][j] = new Tile(scale);


                    if (Game.Random.Next((int)(5)) == 0)
                    {
                        SetTile(i, j, 0);
                    }
                    else { SetTile(i, j, 1); }

                    positions[i][j] = new Vector2f (i * 16 * scale.X, j * 16 * scale.Y) + position;
                }
            }
        }
        public Tile GetTile(int x, int y)
        {
            return tiles[x][y];
        }
        public void SetTile(int x, int y, int id)
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
                    positions[i][j] = new Vector2f(i * 16 * scale.X, j * 16 * scale.Y) + position;
                }
            }
        }
        public void AddTileEntity(TileEntity tileEntity)
        {
            tileEntities.Add(tileEntity);
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
