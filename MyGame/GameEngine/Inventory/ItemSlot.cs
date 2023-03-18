﻿using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
        private readonly Text text;

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
            text = new Text();
            text.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");
            text.Position = _itemSprite.Position + new Vector2f(scale.X * 13, scale.Y * 12);
            text.CharacterSize = 20;
            text.FillColor = Color.White;
            text.OutlineColor = Color.Black;
            text.OutlineThickness = 4;
            text.DisplayedString = Convert.ToString(_item.amount);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
            Game.RenderWindow.Draw(_itemSprite);
            Game.RenderWindow.Draw(text);
        }
        public override void Clicked()
        {
            parent.SlotClicked(ID);
        }
        public void SetItem(Item item)
        {
            this._item = item;
            _item.MakeValid();
            _itemSprite.Texture = ItemDat.GetTexture(_item.ID);
            if (item.amount > 0) { text.DisplayedString = Convert.ToString(item.amount); }
            else { text.DisplayedString = " "; }
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
    }
}