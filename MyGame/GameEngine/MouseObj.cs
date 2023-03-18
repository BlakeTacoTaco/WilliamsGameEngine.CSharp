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

namespace MyGame.GameEngine
{
    internal class MouseObj : GameObject
    {
        public Vector2f position = new Vector2f(0,0);
        private bool clickedLast = false; //if the mouse was clicked last frame
        private bool isClicked = false;   //if the left mouse button is currently pressed
        public bool inputEaten = false;   //keeps track of if the input was eaten or not. for example if you have a button on top of a clickable object in
                                          //the world it will have the button eat the input so it doesn't click two things at once.
        public Item item;
        private readonly Sprite itemSprite;
        public MouseObj()
        {
            itemSprite = new Sprite();
            itemSprite.Scale = new Vector2f(4, 4);
            SetItem(new Item(-1,0));
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(itemSprite);
        }
        public override void Update(Time elapsed)
        {
            //un eats input
            inputEaten = false;

            position = Game.GetMousePos();
            itemSprite.Position = position;

            //old mouse status
            clickedLast = isClicked;

            //current mouse status
            if (Mouse.IsButtonPressed(Mouse.Button.Left)) { isClicked = true; }
            else { isClicked = false; }

            //makes render window follow mouse cursor
            //Game.RenderWindow.Position = Mouse.GetPosition() - new Vector2i(800,450);
        }
        //inventory stuff
        public void SetItem(Item item)
        {
            this.item = item;
            itemSprite.Texture = ItemDat.GetTexture(item.ID);
        }


        //returns true if the mouse button just got pressed
        public bool IsMouseJustPressed()
        {
            return (clickedLast == false && isClicked == true);
        }
        //returns true if the mouse button just got released
        public bool IsMouseJustReleased()
        {
            return (clickedLast == true && isClicked == false);
        }
        //returns true if the mouse button is currently held
        public bool IsMousePressed()
        {
            return isClicked;
        }
    }
}
