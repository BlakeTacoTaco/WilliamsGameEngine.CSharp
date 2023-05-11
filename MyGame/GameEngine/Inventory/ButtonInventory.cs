using GameEngine;
using MyGame.GameEngine.General_UI;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Inventory
{
    internal class ButtonInventory : Inventory , Menu
    {
        internal ItemSlot[] slots;
        internal TrashSlot trash;
        internal SortButton sort;
        internal bool player;
        public Vector2f position = new Vector2f();
        public bool inventoryRequired { get; }
        public bool open { get; set; }
        public bool eatKeyboardInputs { get; }

        public ButtonInventory(Scene scene, Vector2f position,bool player)
        {
            this.player = player;
            slots = new ItemSlot[sizex * sizey];
            _items = new Item[sizex * sizey];
            for (int i = 0; i < sizex * sizey; i++)
            {
                slots[i] = new ItemSlot(this, i, scale, new Vector2f((i % sizex) * scale.X * 20, (i / sizex) * scale.Y * 20) + position);
                _items[i] = new Item(-1, 0);
                scene.AddUiElement(slots[i]);
            }
            if (player)
            {
                trash = new TrashSlot(this);
                trash._sprite.Scale = scale;
                scene.AddUiElement(trash);
            }
            inventoryRequired = !player;
            eatKeyboardInputs = false;
            sort = new SortButton(this);
            sort._sprite.Scale = scale;
            scene.AddUiElement(sort);
            SetPosition(position);
            UpdateSlots();
        }
        public override void Update(Time elapsed)
        {
            if (open)
            {
                if (Game._Mouse.IsLeftJustReleased())
                {
                    //making a list of all slots that were selected
                    List<ItemSlot> selected = new List<ItemSlot>() { };
                    foreach (ItemSlot slot in slots)
                    {
                        if (slot.selected) { selected.Add(slot); slot.Deselect(); }
                    }

                    //if only one slot was selected it does one function but if multiple were it calls a different one
                    if (selected.Count == 1) { SlotClicked(selected[0].ID); }
                    else { SlotsClicked(selected); }
                }
            }
        }
        public void SlotsClicked(List<ItemSlot> selected)//when more than one slot is selected
        {
            //makes sure the mouse isn't empty
            if (Game._Mouse.item.ID == -1) { return; }

            //remove things that cant have things put in them
            Predicate<ItemSlot> removeStuff = g => (g._item.ID != Game._Mouse.item.ID && g._item.ID != -1);
            selected.RemoveAll(removeStuff);

            //stop going if there are no slots left to put anything in
            if (selected.Count == 0) { return; }

            //tries to put items in slots evenly and leaves the rest in the mouse cursor
            int itemsPerSlot = Game._Mouse.item.amount / selected.Count;
            int remainingItems = Game._Mouse.item.amount % selected.Count;
            foreach (ItemSlot slot in selected)
            {
                //if it hits the maximum items per slot it puts the rest in remainingItems
                if (slot._item.amount + itemsPerSlot >= ItemDat.GetStackSize(Game._Mouse.item.ID))
                {
                    remainingItems += (slot._item.amount + itemsPerSlot - ItemDat.GetStackSize(Game._Mouse.item.ID));
                    slot.AddItem(new Item(Game._Mouse.item.ID, itemsPerSlot));
                }
                //otherwise it just adds the item to the slot
                else { slot.AddItem(new Item(Game._Mouse.item.ID, itemsPerSlot)); }
            }
            Game._Mouse.SetItem(new Item(Game._Mouse.item.ID, remainingItems));
            UpdateSlots();
        }
        public void SlotClicked(int ID) //for when one slot is clicked
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
            {
                _items[ID] = new Item(-1, 0);
            }
            else
            {
                //swaps slots if they are dissimilar
                if (slots[ID]._item.ID != Game._Mouse.item.ID)
                {
                    Item temp = slots[ID]._item;
                    _items[ID] = Game._Mouse.item;
                    Game._Mouse.SetItem(temp);
                }
                //merges slots into 1 if they fit
                else if (ItemDat.GetStackSize(slots[ID]._item.ID) >= slots[ID]._item.amount + Game._Mouse.item.amount)
                {
                    _items[ID] = new Item(slots[ID]._item.ID, slots[ID]._item.amount + Game._Mouse.item.amount);
                    Game._Mouse.SetItem(new Item(-1, 0));
                }
                //fills up slot an puts rest in mouse if they don't fit in one slot
                else if (!(slots[ID]._item.amount == ItemDat.GetStackSize(slots[ID]._item.ID) & Game._Mouse.item.amount == ItemDat.GetStackSize(slots[ID]._item.ID)))
                {
                    Game._Mouse.SetItem(new Item(Game._Mouse.item.ID, (Game._Mouse.item.amount + slots[ID]._item.amount) % (ItemDat.GetStackSize(Game._Mouse.item.ID))));
                    _items[ID] = new Item(Game._Mouse.item.ID, ItemDat.GetStackSize(Game._Mouse.item.ID));
                }
            }
            UpdateSlots();
        }
        private void UpdateSlots()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetItem(_items[i]);
            }
        }
        public void ToggleOpen()
        {
            open = !open;
            UpdateOpen();
        }
        public void UpdateOpen()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Deselect();
            }

            if (!open) { this.MakeDead(); }
            else { this.UnDead(); }
        }
        public override void AddItem(Item item)
        {
            base.AddItem(item);
            UpdateSlots();
        }
        public override void Sort()
        {
            base.Sort();
            UpdateSlots();
        }
        public override Vector2f GetPosition()
        {
            return position;
        }
        public override void SetPosition(Vector2f position)
        {
            for (int i = 0; i < sizex * sizey; i++)
            {
                slots[i].SetPosition(new Vector2f((i % sizex) * scale.X * 20, (i / sizex) * scale.Y * 20) + position);
            }
            if (player) { trash._sprite.Position = new Vector2f(((sizex * 20) + 2) * scale.X, 1 * 20 * scale.Y) + position; }
            sort._sprite.Position = new Vector2f(((sizex * 20) + 2) * scale.X, 0) + position;
        }
        public void SetOpen(bool open)
        {
            this.open = open;
            UpdateOpen();
        }
        public override void MakeDead()
        {
            base.MakeDead();
            foreach(ItemSlot slot in slots) { slot.MakeDead(); }
            if (player) { trash.MakeDead(); }
            sort.MakeDead();
        }
        public override void UnDead()
        {
            base.UnDead();
            Game.CurrentScene.AddUiElement(this);
            foreach (ItemSlot slot in slots) { slot.UnDead(); Game.CurrentScene.AddUiElement(slot); }
            if (player) { trash.UnDead(); Game.CurrentScene.AddUiElement(trash); }
            sort.UnDead();
            Game.CurrentScene.AddUiElement(sort);
        }
    }
}
