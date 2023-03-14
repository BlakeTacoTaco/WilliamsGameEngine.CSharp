using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    //items that are able to be stored in the inventory
    internal class Item
    {
        private readonly Texture _texture;
        private readonly string _name;
        private readonly string _description;
        private readonly int _stackSize;
    }
}