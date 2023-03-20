using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using MyGame.GameEngine.Inventory;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame.GameEngine
{
    internal class MouseObj : GameObject
    {
        public Vector2f position = new Vector2f(0,0);
        private bool leftLast = false;    //if the left mouse was clicked last frame
        private bool leftClicked = false; //if the left mouse button is currently pressed
        private bool rightLast = false;    //if the right mouse was clicked last frame
        private bool rightClicked = false; //if the right mouse button is currently pressed
        public bool inputEaten = false;   //keeps track of if the input was eaten or not. for example if you have a button on top of a clickable object in
                                          //the world it will have the button eat the input so it doesn't click two things at once.
        public Item item;
        private readonly Sprite itemSprite;
        private readonly SFML.Graphics.Text text;
        public MouseObj()
        {
            itemSprite = new Sprite();
            itemSprite.Scale = new Vector2f(4, 4);
            text = new SFML.Graphics.Text();
            text.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");
            text.Position = position + new Vector2f(4 * 13, 4 * 12);
            text.CharacterSize = 20;
            text.FillColor = Color.White;
            text.OutlineColor = Color.Black;
            text.OutlineThickness = 4;
            SetItem(new Item(-1, 0));
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(itemSprite);
            Game.RenderWindow.Draw(text);
        }
        public override void Update(Time elapsed)
        {
            //un eats input
            inputEaten = false;

            position = Game.GetMousePos();
            itemSprite.Position = position;
            text.Position = position + new Vector2f(4 * 13, 4 * 12);

            //old mouse status
            leftLast = leftClicked;

            //current mouse status
            if (Mouse.IsButtonPressed(Mouse.Button.Left)) { leftClicked = true; }
            else { leftClicked = false; }

            //makes render window follow mouse cursor
            //Game.RenderWindow.Position = Mouse.GetPosition() - new Vector2i(800,450);
        }
        //inventory stuff
        public void SetItem(Item item)
        {
            this.item = item;
            item.MakeValid();
            itemSprite.Texture = ItemDat.GetTexture(item.ID);
            if (item.amount > 0) { text.DisplayedString = Convert.ToString(item.amount); }
            else { text.DisplayedString = " "; }
        }

        //LEFT
        //returns true if the mouse button just got pressed
        public bool IsLeftJustPressed()
        {
            return (leftLast == false && leftClicked == true);
        }
        //returns true if the mouse button just got released
        public bool IsLeftJustReleased()
        {
            return (leftLast == true && leftClicked == false);
        }
        //returns true if the mouse button is currently held
        public bool IsLeftPressed()
        {
            return leftClicked;
        }

        //RIGHT
        //returns true if the mouse button just got pressed
        public bool IsRightJustPressed()
        {
            return (rightLast == false && rightClicked == true);
        }
        //returns true if the mouse button just got released
        public bool IsRightJustReleased()
        {
            return (rightLast == true && rightClicked == false);
        }
        //returns true if the mouse button is currently held
        public bool IsRightPressed()
        {
            return rightClicked;
        }
    }
}
