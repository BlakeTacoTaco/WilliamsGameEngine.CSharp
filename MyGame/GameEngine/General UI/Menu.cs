using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.General_UI
{
    public interface Menu
    {
        bool inventoryRequired { get; } //whether or not you need to have your inventory open to use that menu
        bool open { get; set; }
        public virtual void SetOpen(bool open)
        {
            this.open = open;
        }
    }
}
