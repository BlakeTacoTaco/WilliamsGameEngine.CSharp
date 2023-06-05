using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using GameEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.GameEngine;
using MyGame.GameEngine.TileEntites;

namespace MyGame.Implementations
{
    internal class FishBubbles : UsableTileEntity
    {
        SpriteRenderer spriteRenderer;
        float delta;
        float timeExisted = 0;
        public FishBubbles(Scene scene) 
        {
            Sprite[] frames = new Sprite[] { new Sprite(), new Sprite(), new Sprite(), new Sprite(), new Sprite(), new Sprite() };
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i].Texture = Game.GetTexture("../../../Resources/Bubbles.png");
                frames[i].TextureRect = new IntRect(new Vector2i(i * 16, 0), new Vector2i(16, 16));
                frames[i].Scale = new Vector2f(4, 4);
            }
            useDist = 16;
            spriteRenderer = new SpriteRenderer(frames,new Vector2f(0,0),0,new Vector2f(4,4),new Vector2f(0,0), new List<List<int>> { new List<int> { 0, 1, 2, 3, 4, 5 } },0.25f);
            spriteRenderer.loop = true;
            position = new Vector2f(0, 0);
            tileDimensions = new Vector2i(1, 1);
            spriteRenderer.PlayAnimation(0, true);
            Initialize(scene);
        }
        public override Vector2f GetPosition()
        {
            return position;
        }
        public override void SetPosition(Vector2f position)
        {
            this.position = position;
        }
        public override FloatRect GetCollisionRect()
        {
            FloatRect box = spriteRenderer.frames[0].GetGlobalBounds();
            box.Left = position.X;
            box.Top = position.Y;
            return box;
        }
        public override void Update(Time elapsed)
        {
            delta = elapsed.AsSeconds();
            timeExisted += delta;
        }
        public override void Draw()
        {
            spriteRenderer.position = Game._Camera.ToLocalPos(position);
            spriteRenderer.Draw(delta);
        }
        public override void Use(Player player)
        {
            player.OpenMenu(new TackleFishMenu(player));
        }
        public override void OutCollision(Player player)
        {
            
        }
    }
}
