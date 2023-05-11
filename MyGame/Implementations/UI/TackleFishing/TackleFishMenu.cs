using GameEngine;
using MyGame.GameEngine.General_UI;
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
        private Vector2f position;
        private float difficulty;
        private float progress;
        public bool inventoryRequired { get; }
        public bool open { get; set; }
        private NinePatch background;
        private TackleButton button;
        private ProgressBar bar;
        public bool eatKeyboardInputs { get; }
        public TackleFishMenu()
        {
            button = new TackleButton(new Vector2f(4,4),new Vector2f(1920 / 2,1080 / 2),this);
            Game.CurrentScene.AddUiElement(button);
            eatKeyboardInputs = true;
            difficulty = 1;
            progress = 0.5f;
            inventoryRequired = false;
            open = false;
            background = new NinePatch(Game.GetTexture("../../../Resources/item slot.png"), 2, 2, 2, 2, new Vector2f(400, 100), new Vector2f(1920 - 800, 400), new Vector2f(4, 4));

            bar = new ProgressBar(new Vector2f(0,0), 0.5f , 2, Game.GetTexture("../../../Resources/Progressbar.png"), Game.GetTexture("../../../Resources/item slot.png"),  new Vector2f(4,4));
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
        }
        public void DealDamage()
        {
            progress += 0.1f;
        }
        public Vector2f GetNewButtonPos(Vector2f original)
        {
            Vector2f output = original + new Vector2f(Game.Random.Next(200) - 100, Game.Random.Next(200) - 100);
            if (output.X < background.position.X) { output.X = background.position.X; }
            if (output.Y < background.position.Y) { output.Y = background.position.Y; }
            if (output.X > background.position.X + background.GetSize().X - 64) { output.X = background.position.X + background.GetSize().X - 64; }
            if (output.Y > background.position.Y + background.GetSize().Y - 64) { output.Y = background.position.Y + background.GetSize().Y - 64; }
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
