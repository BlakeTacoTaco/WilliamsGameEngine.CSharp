using GameEngine;
using SFML.Graphics;
using MyGame.GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using System.Transactions;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.General_UI;
using MyGame.GameEngine.TileEntites;
using MyGame.GameEngine.TileMap;

namespace MyGame.Implementations
{
    internal class Player : GameObject
    {
        private ButtonInventory inventory;
        private Sprite _sprite = new Sprite();
        private float speed = 1000;
        public Vector2f position;
        public UsableTileEntity useEntity = null;
        private Menu menu;
        public Player(ButtonInventory inventory, Scene scene)
        {
            this.inventory = inventory;
            _sprite.Texture = Game.GetTexture("../../../Resources/mouse test.png");
            _sprite.Scale = new Vector2f(4, 4);
            scene.AddUiElement(inventory);
            SetCollisionCheckEnabled(true);
            AssignTag("tilecollision");
            inventory.AddItem(new Item(3, 1));
        }
        public override void Draw()
        {
            _sprite.Position = Game._Camera.ToLocalPos(position);
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            float delta = elapsed.AsSeconds();

            if (menu == null)
            {
                //movement
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { position.Y -= speed * delta; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { position.Y += speed * delta; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { position.X -= speed * delta; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { position.X += speed * delta; }
            }
            if (BetterKeyboard.IsKeyJustPressed(Keyboard.Key.Escape) && menu != null) { CloseMenu(menu); }

            if (menu != null)
            {
                if (!menu.eatKeyboardInputs)
                {
                    if (BetterKeyboard.IsKeyJustReleased(Keyboard.Key.Q) && useEntity != null) { useEntity.Use(this); }
                    if (BetterKeyboard.IsKeyJustPressed(Keyboard.Key.T)) { OpenMenu(new TackleFishMenu(this)); }
                    if (!menu.inventoryDisabled)
                    {
                        if (BetterKeyboard.IsKeyJustReleased(Keyboard.Key.E)) { OpenInventory(!inventory.open); }
                    }
                }
            }
            else
            {
                if (BetterKeyboard.IsKeyJustReleased(Keyboard.Key.Q) && useEntity != null) { useEntity.Use(this); }
                if (BetterKeyboard.IsKeyJustPressed(Keyboard.Key.T)) { OpenMenu(new TackleFishMenu(this)); }
                if (BetterKeyboard.IsKeyJustReleased(Keyboard.Key.E)) { OpenInventory(!inventory.open); }
            }

            if (BetterKeyboard.IsKeyJustReleased(Keyboard.Key.Tilde)) { Environment.Exit(1); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F)) { Game.CurrentScene.tileMap.SaveTo("../../../Resources/TestTileMap.txt"); }

            useEntity = null;
            //camera movement
            Game._Camera.position = position - new Vector2f(1920 / 2, 1080 / 2);
        }
        public override FloatRect GetCollisionRect()
        {
            FloatRect box = _sprite.GetGlobalBounds();
            box.Left = position.X;
            box.Top = position.Y;
            return box;
        }
        public void GiveItem(Item item)
        {
            inventory.AddItem(item);
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            base.HandleCollision(otherGameObject);
            Game._Camera.position = position - new Vector2f(1920 / 2, 1080 / 2);
        }
        public override Vector2f GetPosition()
        {
            return position;
        }
        public override void SetPosition(Vector2f position)
        {
            this.position = position;
        }
        public void OpenInventory(bool set)
        {
            inventory.SetOpen(set);
            if (menu == null) { return; }
            if (!set && menu.inventoryRequired) { CloseMenu(menu); }
        }
        public void OpenMenu(Menu menu)
        {
            this.menu = menu;
            menu.SetOpen(true);
            if (menu.inventoryRequired) { OpenInventory(true); }
            if (menu.inventoryDisabled) { OpenInventory(false); }
        }
        public void CloseMenu(Menu menu)
        {
            if (this.menu == null) { return; }
            if (this.menu == menu)
            {
                menu.SetOpen(false);
                this.menu = null;
            }
        }
    }
}
