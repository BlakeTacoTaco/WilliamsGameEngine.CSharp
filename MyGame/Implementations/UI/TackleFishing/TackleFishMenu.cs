using GameEngine;
using MyGame.GameEngine.General_UI;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.TileEntites;
using MyGame.Implementations.UI.TackleFishing;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Implementations
{
    internal class TackleFishMenu : GameObject, Menu
    {
        /*
         this is the menu that pops up when you start tackiling a fish... litterally tackling them
        it works by having you click a constantly moving button to make projectiles that 'damage' the fish
        it was going to be a cps test but the carp tunnel was too bad.
         */
        internal TileEntity sourceBubbles;
        internal Player player;
        internal Vector2f position;
        internal float difficulty;
        public float progress;
        public bool inventoryRequired { get; }
        public bool inventoryDisabled { get; }
        public bool open { get; set; }
        internal NinePatch background;
        internal TackleButton button;
        internal HorizontalProgressBar bar;
        public bool eatKeyboardInputs { get; }
        public TackleFishMenu(Player player, TileEntity sourceBubbles)
        {
            this.sourceBubbles = sourceBubbles;
            this.player = player;
            eatKeyboardInputs = true;
            difficulty = 1;
            progress = 0.5f;
            inventoryRequired = false;
            inventoryDisabled = true;
            open = false;
            background = new NinePatch(Game.GetTexture("../../../Resources/item slot.png"), 2, 2, 2, 2, new Vector2f(400, 100), new Vector2f(1920 - 800, 400), new Vector2f(4, 4));

            bar = new HorizontalProgressBar(new Vector2f(background.position.X, background.position.Y + background.GetSize().Y - 4), 0.5f , 2, Game.GetTexture("../../../Resources/Progressbar.png"), Game.GetTexture("../../../Resources/item slot.png"),  new Vector2f(4,4));

            button = new ButterFishGame(new Vector2f(4, 4), new Vector2f(background.position.X + (background.GetSize().X / 2) - 32, background.position.Y + (background.GetSize().Y / 2) - 32), this);
            Game.CurrentScene.AddUiElement(button);
        }
        public override void Draw()
        {
            background.Draw();
            bar.Draw();
        }
        public override void Update(Time elapsed)
        {
            progress -= elapsed.AsSeconds() * 0.1f;
            bar.SetProgress(progress);
            if(progress > 1)
            {
                player.CloseMenu(this);
                player.GiveItem(button.Catch());
                Game.CurrentScene.tileMap.RemoveTileEntity(sourceBubbles);
            }
            else if (progress < 0)
            {
                player.CloseMenu(this);
            }
            button.Update(elapsed);
        }
        public void DealDamage()
        {
            progress += 0.1f;
        }
        public Vector2f KeepInBounds(Vector2f original)
        {
            if (original.X < background.position.X + 12 + 32) { original.X = background.position.X + 12 + 32; button.TouchLeft(); }
            if (original.Y < background.position.Y + 12 + 32) { original.Y = background.position.Y + 12 + 32; button.TouchTop(); }
            if (original.X > background.position.X + background.GetSize().X - 56) { original.X = background.position.X + background.GetSize().X - 56; button.TouchRight(); }
            if (original.Y > background.position.Y + background.GetSize().Y - 56) { original.Y = background.position.Y + background.GetSize().Y - 56; button.TouchBottom(); }
            return original;
        }
        public override void SetPosition(Vector2f position) { this.position = position; }
        public override Vector2f GetPosition() { return position; }
        public override void MakeDead()
        {
            base.MakeDead();
            button.MakeDead();
        }
        public override void UnDead()
        {
            base.UnDead();
            Game.CurrentScene.AddUiElement(this);
            button.UnDead();
            Game.CurrentScene.AddUiElement(button);
        }
        public void SetOpen(bool open)
        {
            this.open = open;
            if (open) { UnDead(); }
            else { MakeDead(); }
        }
    }
}
