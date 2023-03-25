using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.General_UI
{
    internal class TextBox : GameObject
    {
        private Text topText;      //the header or something
        private Text bottomText;   //the main body of the textbox
        private NinePatch background; //the scalable background of the text
        public TextBox(string topText, string bottomText, Vector2f position, Vector2f scale)
        {
            this.topText.DisplayedString = topText;
            this.bottomText.DisplayedString = bottomText;
            background = new NinePatch(Game.GetTexture("../../../Resources/text background.png"), 7, 2, 2, 2, position, new Vector2f(0, 0), scale);

            UpdateDisplay();
        }
        public override void Draw()
        {
           background.Draw();
           Game.RenderWindow.Draw(topText);
           Game.RenderWindow.Draw(bottomText);
        }
        public void UpdateDisplay() //updates the visual based on the new parameters
        {

        }
        public override void Update(Time elapsed) { }
    }
}