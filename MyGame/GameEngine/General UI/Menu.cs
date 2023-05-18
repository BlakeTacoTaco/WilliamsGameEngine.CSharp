using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using MyGame.GameEngine.Inventory;

namespace MyGame.GameEngine.General_UI
{
    public interface Menu
    {
        bool inventoryRequired { get; } //whether or not you need to have your inventory open to use that menu
        bool inventoryDisabled { get; } //whether or not the inventory can be open at the same time as the menu
        bool open { get; set; }
        bool eatKeyboardInputs { get; }
        public virtual void SetOpen(bool open)
        {
            this.open = open;
        }
    }
}
