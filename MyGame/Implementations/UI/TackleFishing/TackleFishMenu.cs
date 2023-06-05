using GameEngine;
using MyGame.GameEngine.General_UI;
using MyGame.GameEngine.Inventory;
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
        internal Player player;
        internal Vector2f position;
        internal float difficulty;
        internal float progress;
        public bool inventoryRequired { get; }
        public bool inventoryDisabled { get; }
        public bool open { get; set; }
        internal NinePatch background;
        internal TackleButton button;
        internal HorizontalProgressBar bar;
        public bool eatKeyboardInputs { get; }
        public TackleFishMenu(Player player)
        {
            this.player = player;
            eatKeyboardInputs = true;
            difficulty = 1;
            progress = 0.5f;
            inventoryRequired = false;
            inventoryDisabled = true;
            open = false;
            background = new NinePatch(Game.GetTexture("../../../Resources/item slot.png"), 2, 2, 2, 2, new Vector2f(400, 100), new Vector2f(1920 - 800, 400), new Vector2f(4, 4));

            bar = new HorizontalProgressBar(new Vector2f(background.position.X, background.position.Y + background.GetSize().Y - 4), 0.5f , 2, Game.GetTexture("../../../Resources/Progressbar.png"), Game.GetTexture("../../../Resources/item slot.png"),  new Vector2f(4,4));

            button = new TackleButton(new Vector2f(4, 4), new Vector2f(background.position.X + (background.GetSize().X / 2) - 32, background.position.Y + (background.GetSize().Y / 2) - 32), this);
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
                player.GiveItem(new Item(2,1));
            }
            else if (progress < 0)
            {
                player.CloseMenu(this);
            }
            button.SetPosition(GetNewButtonPos(button.GetPosition(), 1));
            button._sprite.Rotation += Game.Random.Next(3) - 1;
        }
        public void DealDamage()
        {
            progress += 0.1f;
        }
        public Vector2f GetNewButtonPos(Vector2f original, int dist)
        {
            Vector2f output = original + new Vector2f(Game.Random.Next(dist * 2 + 1) - dist, Game.Random.Next(dist * 2 + 1) - dist);
            if (output.X < background.position.X + 12 + 32) { output.X = background.position.X + 12 + 32; }
            if (output.Y < background.position.Y + 12 + 32) { output.Y = background.position.Y + 12 + 32; }
            if (output.X > background.position.X + background.GetSize().X - 64 - 12) { output.X = background.position.X + background.GetSize().X - 64 - 12; }
            if (output.Y > background.position.Y + background.GetSize().Y - 64 - 12) { output.Y = background.position.Y + background.GetSize().Y - 64 - 12; }
            return output;
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
