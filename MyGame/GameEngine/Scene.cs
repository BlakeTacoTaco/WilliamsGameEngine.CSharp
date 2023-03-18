﻿using System;
using System.Collections.Generic;
using MyGame.GameEngine;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    // The Scene manages all the GameObjects currently in the game.
    class Scene
    {
        // @TODO I need to make a function for removing dead UI elements
        // the distinction between gameobject and ui is made so the UI draws on top
        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        private readonly List<GameObject> _uiElements = new List<GameObject>();

        // Puts a GameObject into the scene.
        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        //puts a Ui element into the scene
        public void AddUiElement(GameObject gameObject)
        {
            _uiElements.Add(gameObject);
        }

        // Called by the Game instance once per frame.
        public void Update(Time time)
        {
            // Clear the window.
            Game.RenderWindow.Clear();

            // Go through our normal sequence of game loop stuff.

            // Handle any keyboard, mouse events, etc. for our game window.
            Game.RenderWindow.DispatchEvents();

            HandleCollisions();
            UpdateGameObjects(time);
            ClickCheck();
            RemoveDeadGameObjects();
            DrawGameObjects();

            // Draw the window as updated by the game objects.
            Game.RenderWindow.Display();
        }

        // This method lets game objects respond to collisions.
        private void HandleCollisions()
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
        private void UpdateGameObjects(Time time)
        {
            Game._Mouse.Update(time);
            for (int i = 0; i < _gameObjects.Count; i++) { _gameObjects[i].Update(time); }
            for (int i = 0; i < _uiElements.Count; i++) { _uiElements[i].Update(time); }
        }

        // This function calls draw on each of our game objects.
        private void DrawGameObjects()
        {
            foreach (var gameObject in _gameObjects) { gameObject.Draw(); }
            foreach (var gameObject in _uiElements) { gameObject.Draw(); }
            Game._Mouse.Draw();
        }

        // This function removes objects that indicate they are dead from the scene.
        private void RemoveDeadGameObjects()
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
        private void ClickCheck()
        {
            foreach (var gameObject in _uiElements) 
            {
                if(gameObject is MouseInterface)
                {
                    if(gameObject.GetCollisionRect().Contains(Game._Mouse.position.X, Game._Mouse.position.Y))
                    {
                        //type casts the object reference as a mouseinterface so it can call the coresponding functions
                        MouseInterface gameObject2 = (MouseInterface)gameObject;

                        if (Game._Mouse.IsMouseJustReleased()) { gameObject2.Clicked(); }
                        else { gameObject2.Hover(); }
                    }
                }
            }
        }
    }
}