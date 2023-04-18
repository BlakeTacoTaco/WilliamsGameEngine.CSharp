using GameEngine;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyGame.GameEngine.Inventory
{
    internal class ButtonInventory : Inventory
    {
        internal ItemSlot[] slots;
        internal TrashSlot trash;
        internal SortButton sort;
        public bool open = false;
        public ButtonInventory(Scene scene)
        {
            slots = new ItemSlot[sizex * sizey];
            _items = new Item[sizex * sizey];
            for (int i = 0; i < sizex * sizey; i++)
            {
                slots[i] = new ItemSlot(this, i, scale, new Vector2f((i % sizex) * scale.X * 20, (i / sizex) * scale.Y * 20));
                _items[i] = new Item(Game.Random.Next(4) - 1, Game.Random.Next(80) + 1);
                scene.AddUiElement(slots[i]);
            }
            trash = new TrashSlot(this);
            trash._sprite.Scale = scale;
            trash._sprite.Position = new Vector2f(((sizex * 20) + 2) * scale.X, (sizey - 1) * 20 * scale.Y);
            scene.AddUiElement(trash);

            sort = new SortButton(this);
            sort._sprite.Scale = scale;
            sort._sprite.Position = new Vector2f(((sizex * 20) + 2) * scale.X, (sizey - 2) * 20 * scale.Y);
            scene.AddUiElement(sort);
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
                slots[ID].SetItem(new Item(-1, 0));
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
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Deselect();
            }
        }
        public override void AddItem(Item item)
        {
            base.AddItem(item);
            UpdateSlots();
        }
        public override Vector2f GetPosition()
        {
            throw new NotImplementedException();
        }
        public override void SetPosition(Vector2f position)
        {
            throw new NotImplementedException();
        }
        public override void Sort()
        {
            base.Sort();
            UpdateSlots();
        }
    }
}
