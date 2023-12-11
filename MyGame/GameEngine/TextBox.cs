using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class TextBox : Sprite
    {
        public string text
        {
            get
            {
                return text;
            }
            set
            {
                SetText(value);
            }
        }
        public TextBox(string text)
        {
        }
        public override void Update(Time elapsed)
        {
            
        }
        public override void Draw()
        {
            
        }
        private void SetText(string text)
        {
            this.text = text;
        }
    }
}
