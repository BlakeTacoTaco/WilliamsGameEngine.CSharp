using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Inventory
{
    internal class ItemSlot : Button
    {
        private Inventory parent;
        public int ID;
        private readonly Sprite _sprite;
        public Item _item;
        private readonly Sprite _itemSprite;

        public ItemSlot(Inventory parent, int ID, Vector2f scale, Vector2f position)
        {
            this.parent = parent;
            this.ID = ID;
            _sprite = new Sprite();
            _sprite.Texture = Game.GetTexture("../../../Resources/item slot.png");
            _sprite.Scale = scale;
            _item = new Item(-1, 0);
            _sprite.Position = position;
            _itemSprite = new Sprite();
            _itemSprite.Texture = ItemDat.GetTexture(_item.ID);
            _itemSprite.Position = position + new Vector2f(2 * scale.X, 2 * scale.Y);
            _itemSprite.Scale = scale;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
            Game.RenderWindow.Draw(_itemSprite);
        }
        public override void Clicked()
        {
            parent.SlotClicked(ID);
        }
        public void SetItem(Item item)
        {
            this._item = item;
            _itemSprite.Texture = ItemDat.GetTexture(_item.ID);
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
    }
}
