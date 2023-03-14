using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyGame.GameEngine
{
    internal class MouseObj : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        private bool clickedLast = false; //if the mouse was clicked last frame
        private bool isClicked = false;   //if the left mouse button is currently pressed
        public bool inputEaten = false;   //keeps track of if the input was eaten or not. for example if you have a button on top of a clickable object in
                                          //the world it will have the button eat the input so it doesn't click two things at once.
        public MouseObj()
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
            _sprite.Origin = new Vector2f(16, 16);
            _sprite.Scale = new Vector2f(4, 4);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            _sprite.Position = Game.GetMousePos();

            //old mouse status
            clickedLast = isClicked;

            //current mouse status
            if (Mouse.IsButtonPressed(Mouse.Button.Left)) { isClicked = true; }
            else { isClicked = false; }

            //makes render window follow mouse cursor
            //Game.RenderWindow.Position = Mouse.GetPosition() - new Vector2i(800,450);
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
