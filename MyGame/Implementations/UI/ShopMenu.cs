using System;
using GameEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.GameEngine.General_UI;
using SFML.System;

namespace MyGame.Implementations.UI
{
    internal class ShopMenu : GameObject, Menu
    {
        public bool inventoryRequired { get; }
        public bool inventoryDisabled { get; }
        public bool open { get; set; }
        public bool eatKeyboardInputs { get; }
        internal Player player;
        public ShopMenu(Player player)
        {
            this.player = player;
            inventoryRequired = true;
            inventoryDisabled = false;
            open = false;
            eatKeyboardInputs = true;
        }
        public override void Draw()
        {
            
        }
        public override void Update(Time elapsed)
        {
            
        }
        public override void SetPosition(Vector2f position)
        {
            
        }
        public override Vector2f GetPosition()
        {
            return new Vector2f(0,0);
        }
    }
}
