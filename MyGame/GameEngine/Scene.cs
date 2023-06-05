using System;
using System.Collections.Generic;
using MyGame.GameEngine;
using MyGame.GameEngine.General_UI;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.TileEntites;
using MyGame.GameEngine.TileMap;
using MyGame.Implementations;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    // The Scene manages all the GameObjects currently in the game.
    class Scene
    {
        public Player player;

        public readonly List<GameObject> _gameObjects = new List<GameObject>();

        internal readonly List<GameObject> _uiElements = new List<GameObject>();

        public TileMap tileMap;

        internal string lastAdded; //last thing to be added to the scene

        // Puts a GameObject into the scene.
        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        //puts a Ui element into the scene
        public void AddUiElement(GameObject gameObject)
        {
            _uiElements.Add(gameObject);
            if (gameObject.ToString() != lastAdded)
            {
                Console.Write("\nspawned: " + gameObject.ToString() + " ");
            }
            else
            {
                Console.Write("+");
            }
            lastAdded = gameObject.ToString();
        }

        // Called by the Game instance once per frame.
        public void Update(Time time)
        {
            // Clear the window.
            Game.RenderWindow.Clear();

            // Go through our normal sequence of game loop stuff.

            // Handle any keyboard, mouse events, etc. for our game window.
            Game.RenderWindow.DispatchEvents();

            if (!(time.AsSeconds() >= 0.1))
            {
                ClickCheck();
                UpdateGameObjects(time);
                HandleCollisions();
                HandleTileCollisions();
                RemoveDeadGameObjects();
                DrawGameObjects();
            }
            else
            {
                DrawGameObjects();
            }

            // Draw the window as updated by the game objects.
            Game.RenderWindow.Display();

            //updates time based on time elapsed
            Game.time += time.AsSeconds();
        }

        // This method lets game objects respond to collisions.

        internal void HandleCollisions()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var gameObject = _gameObjects[i];

                // Only check objects that ask to be checked.
                if (!gameObject.IsCollisionCheckEnabled()) continue;

                FloatRect collisionRect = gameObject.GetCollisionRect();

                // Don't bother checking if this game object has a collision rectangle with no area.
                if (collisionRect.Height == 0 || collisionRect.Width == 0) continue;

                // See if this game object is colliding with any other game object.
                for (int j = 0; j < _gameObjects.Count; j++)
                {
                    
                    var otherGameObject = _gameObjects[j];


                    // Don't check an object colliding with itself.
                    if (gameObject == otherGameObject) continue;

                    if (gameObject.IsDead()) return;


                    if (gameObject is Player && otherGameObject is UsableTileEntity)
                    {
                        Player player = (Player)gameObject;
                        UsableTileEntity tileEntity = (UsableTileEntity)otherGameObject;
                        if(player.GetCollisionRect().Intersects(tileEntity.GetUseCollision()))
                        {
                            tileEntity.InCollision(player);
                        }
                    }

                    // When we find a collision, invoke the collision handler for both objects.
                    if (collisionRect.Intersects(otherGameObject.GetCollisionRect()))
                    {
                        gameObject.HandleCollision(otherGameObject);
                        otherGameObject.HandleCollision(gameObject);
                    }
                }
            }
        }

        // This function calls update on each of our game objects.
        internal virtual void UpdateGameObjects(Time time)
        {
            BetterKeyboard.Update();
            Game._Mouse.Update(time);
            for (int i = 0; i < _gameObjects.Count; i++) { _gameObjects[i].Update(time); }
            for (int i = 0; i < _uiElements.Count; i++) { _uiElements[i].Update(time); }
            Game._Mouse.inputEaten = false;
        }

        // This function calls draw on each of our game objects.
        internal virtual void DrawGameObjects()
        {
            tileMap.Draw();
            foreach (var gameObject in _gameObjects) { gameObject.Draw(); }
            foreach (var gameObject in _uiElements) { gameObject.Draw(); }
            Game._Mouse.Draw();
        }

        // This function removes objects that indicate they are dead from the scene.
        internal void RemoveDeadGameObjects()
        {
            // This is a "lambda", which is a fancy name for an anonymous function.
            // It's "anonymous" because it doesn't have a name. We've declared a variable
            // named "isDead", and that variable can be used to call the function, but the
            // function itself is nameless.
            Predicate<GameObject> isDead = g => g.IsDead();

            // Here we use the lambda declared above by passing it to the standard RemoveAll
            // method on List<T>, which calls our lambda once for each element in
            // gameObjects. If our lambda returns true, that game object ends up being
            // removed from our list.
            _gameObjects.RemoveAll(isDead);
            _uiElements.RemoveAll(isDead);
        }
        //checks to see if anything has been clicked
        internal void ClickCheck()
        {
            if (!Game._Mouse.inputEaten)
            {
                foreach (var gameObject in _uiElements)
                {
                    if (gameObject is MouseInterface)
                    {
                        if (gameObject.GetCollisionRect().Contains(Game._Mouse.position.X, Game._Mouse.position.Y))
                        {
                            //type casts the object reference as a mouseinterface so it can call the coresponding functions
                            MouseInterface gameObject2 = (MouseInterface)gameObject;

                            //Right
                            if (Game._Mouse.IsRightJustReleased()) { gameObject2.ReleaseRight(); }
                            else if (Game._Mouse.IsRightJustPressed()) { gameObject2.PressRight(); }
                            else if (Game._Mouse.IsRightPressed()) { gameObject2.HoldRight(); }

                            //left
                            if (Game._Mouse.IsLeftJustReleased()) { gameObject2.ReleaseLeft(); }
                            else if (Game._Mouse.IsLeftJustPressed()) { gameObject2.PressLeft(); }
                            else if (Game._Mouse.IsLeftPressed()) { gameObject2.HoldLeft(); }

                            else { gameObject2.Hover(); }

                            if (Game._Mouse.inputEaten == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if ((!Game._Mouse.inputEaten && Game._Mouse.IsLeftPressed()) || (!Game._Mouse.inputEaten && Game._Mouse.IsLeftJustReleased()))
            {
                ItemDat.UseItem();
            }
        }
        internal void HandleTileCollisions()
        {
            foreach (var gamobject in _gameObjects)
            {
                if (gamobject.HasTag("tilecollision"))
                {
                    tileMap.CollisionCheck(gamobject);
                }
            }
        }
    }
}