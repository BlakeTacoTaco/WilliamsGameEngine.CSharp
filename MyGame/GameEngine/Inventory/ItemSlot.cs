using GameEngine;
using MyGame.GameEngine.General_UI;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Inventory
{
    internal class ItemSlot : Button
    {
        private ButtonInventory parent;
        public int ID;
        public Item _item;
        private readonly Sprite _itemSprite;
        private readonly Text text;
        public bool selected = false;
        private Texture selectedTexture;
        private Texture defaultTexture;

        public ItemSlot(ButtonInventory parent, int ID, Vector2f scale, Vector2f position)
        {
            this.parent = parent;
            this.ID = ID;
            selectedTexture = Game.GetTexture("../../../Resources/selected item slot.png");
            defaultTexture = Game.GetTexture("../../../Resources/item slot.png");
            _sprite = new Sprite();
            _sprite.Texture = defaultTexture;
            _sprite.Scale = scale;
            _item = new Item(-1, 0);
            _sprite.Position = position;
            _itemSprite = new Sprite();
            _itemSprite.Texture = ItemDat.GetTexture(_item.ID);
            _itemSprite.Position = position + new Vector2f(2 * scale.X, 2 * scale.Y);
            _itemSprite.Scale = scale;
            text = new Text();
            text.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");
            text.Position = _itemSprite.Position + new Vector2f(scale.X * 13, scale.Y * 12);
            text.CharacterSize = 20;
            text.FillColor = Color.White;
            text.OutlineColor = Color.Black;
            text.OutlineThickness = 4;
            text.DisplayedString = Convert.ToString(_item.amount);
        }
        public override void Hover()
        {
            if (parent.open)
            {
                Game._Mouse.textbox.setBothText(ItemDat.GetName(_item.ID), ItemDat.GetDesc(_item.ID));
                Game._Mouse.textbox.UpdateDisplay();
                Game._Mouse.isTextBoxShowing = true;
                base.Hover();
            }
        }
        public override void Update(Time elapsed)
        {
            base.Update(elapsed);
        }
        public override void Draw()
        {
            if (parent.open)
            {
                Game.RenderWindow.Draw(_sprite);
                Game.RenderWindow.Draw(_itemSprite);
                Game.RenderWindow.Draw(text);
            }
        }
        public override void HoldLeft()
        {
            if (parent.open) { Select(); }
        }
        public void SetItem(Item item)
        {
            this._item = item;
            _item.MakeValid();
            UpdateSlot();
        }
        public void AddItem(Item item)
        {
            _item.AddItem(item);
            UpdateSlot();
        }
        public void UpdateSlot()
        {
            _itemSprite.Texture = ItemDat.GetTexture(_item.ID);
            if (_item.amount > 0) { text.DisplayedString = Convert.ToString(_item.amount); }
            else { text.DisplayedString = " "; }
        }
        public void Deselect()
        {
            selected = false;
            _sprite.Texture = defaultTexture;
        }
        public void Select()
        {
            selected = true;
            _sprite.Texture = selectedTexture;
        }
    }
}
