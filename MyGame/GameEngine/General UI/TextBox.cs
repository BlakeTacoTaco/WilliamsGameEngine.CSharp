using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.General_UI
{
    internal class TextBox : GameObject
    {
        private Text topText;         //the header or something
        private Text bottomText;      //the main body of the textbox
        private NinePatch background; //the scalable background of the text
        public Vector2f position;
        public Vector2f scale;
        private bool show  = false;   //if there is no text in the box it shouldn't show anything
        public TextBox(string topText, string bottomText, Vector2f position, Vector2f scale)
        {
            this.position = position;
            this.topText = new Text();
            this.bottomText = new Text();
            this.topText.DisplayedString = topText;
            this.bottomText.DisplayedString = bottomText;
            background = new NinePatch(Game.GetTexture("../../../Resources/text background.png"), 7, 2, 2, 2, position, new Vector2f(100, 100), scale);
            this.topText.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");
            this.bottomText.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");

            this.scale = scale;
            this.topText.CharacterSize = (uint)(5 * scale.X);
            this.bottomText.CharacterSize = (uint)(15.0f / 4.0f * scale.X);

            UpdateDisplay();
        }
        public override void Draw()
        {
            if (show)
            {
                background.Draw();
                Game.RenderWindow.Draw(topText);
                Game.RenderWindow.Draw(bottomText);
            }
        }
        public void UpdateDisplay() //updates the visual based on the new parameters
        {
            //makes sure it doens't show an empty display
            if((topText.DisplayedString == "" || topText.DisplayedString == " ") && (bottomText.DisplayedString == "" || bottomText.DisplayedString == " "))
            {
                show = false;
                return;
            }
            else { show = true; }

            //aligns positions
            background.position = position;
            topText.Position = position + new Vector2f(2.5f * scale.X, 0.5f * scale.Y);
            bottomText.Position = position + new Vector2f(2.5f * scale.X, 7 * scale.Y);

            //finds height
            float height = topText.GetLocalBounds().Height + bottomText.GetLocalBounds().Height;
            height += (9 * scale.Y);

            //finds width
            float width;
            //figures out if the top or bottom text is wider
            if (topText.GetLocalBounds().Width > bottomText.GetLocalBounds().Width) { width = topText.GetLocalBounds().Width; }
            else { width = bottomText.GetLocalBounds().Width; }
            width += 6 * scale.X;

            //sets background to proper size to fit both texts
            background.SetSize(new Vector2f(width / scale.X, height / scale.Y));
        }
        public void setTopText(string text)
        {
            topText.DisplayedString = text;
            UpdateDisplay();
        }
        public void setBottomText (string text)
        {
            bottomText.DisplayedString = text;
            UpdateDisplay();
        }
        public void setBothText (string topText, string bottomText)
        {
            this.topText.DisplayedString = topText;
            this.bottomText.DisplayedString = bottomText;
            UpdateDisplay();
        }
        public override void Update(Time elapsed) { }
        public override Vector2f GetPosition()
        {
            return position;
        }
    }
}