using System;
using System.Collections.Generic;
using System.Threading;
using MyGame.GameEngine;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    // The Scene manages all the GameObjects currently in the game.
    class Scene
    {
        //for keeping track of things
        private Clock clock = new Clock();

        //debug stuff
        private DebugText debug = new DebugText();

        // This holds our game objects.
        public Node root = new Node();

        // list of cameras to be rendered this frame
        List<Camera> cameras = new List<Camera>();

        //the default camera
        internal Camera _camera = new Camera(new View());

        // Called by the Game instance once per frame.
        public void Update(Time time)
        {
            // Clear the window.
            Game.RenderWindow.Clear();

            // Go through our normal sequence of game loop stuff.

            // Handle any keyboard, mouse events, etc. for our game window.
            Game.RenderWindow.DispatchEvents();

            //update
            clock.Restart();
            UpdateGameObjects(time);
            debug.GiveUpdateTime(clock.Restart());

            //collisions
            HandleCollisions();

            //ded
            RemoveDeadGameObjects();

            //draw stuff
            clock.Restart();
            DrawGameObjects();
            debug.GiveDrawTime(clock.Restart());
            debug.Update(time);
            debug.Draw(_camera);

            // Draw the window as updated by the game objects.
            Game.RenderWindow.Display();
        }

        // This method lets game objects respond to collisions.
        private void HandleCollisions()
        {
            //add collision layers that objects submit themselves to
        }

        // This function calls update on each of our game objects.
        private void UpdateGameObjects(Time time)
        {
            root.Update(time);
        }

        // This function calls draw on each of our game objects.
        private void DrawGameObjects()
        {
            root.Draw(_camera);
            cameras.Add(_camera);
            foreach (Camera camera in cameras)
            {
                camera.DrawFrom();
            }
            cameras.Clear();
        }

        //Submits a camera to the draw cycle
        public void SubmitCamera(Camera camera)
        {
            cameras.Add(camera);
        }

        // This function removes objects that indicate they are dead from the scene.
        private void RemoveDeadGameObjects()
        {
            root.RemoveDead();
        }
    }
}