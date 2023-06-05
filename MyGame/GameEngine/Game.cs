using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Reflection;
using MyGame.GameEngine;
using MyGame.GameEngine.Inventory;
using MyGame.GameEngine.General_UI;
using MyGame.GameEngine.TileMap;
using MyGame.Implementations;

namespace GameEngine
{
    // The Game manages scenes and runs the main game loop.
    static class Game
    {
        //how long has it been since you started the save (in seconds)
        //its a float because it will last like forever
        //2^32 = 4,294,967,296
        //4,294,967,296 / (60 * 60) = 1,193,046.4
        //one million hours should last way longer than any player will ever play
        //thats 136 years
        //if one player has enogh dedication to play my game for 136 years their fricking immortal
        //you would have to pass down a single save of my game for generations to break the time system
        public static float time = 0;

        // The number of frames that will be drawn to the screen in one second.
        internal const int FramesPerSecond = 60000;

        // We keep a current and next scene so the scene can be changed mid-frame.
        internal static Scene _currentScene;
        internal static Scene _nextScene;

        // Cached textures
        internal static readonly Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();

        // Cached sounds
        internal static readonly Dictionary<string, SoundBuffer> Sounds = new Dictionary<string, SoundBuffer>();

        // Cached fonts
        internal static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        // The window we will draw to.
        internal static RenderWindow _window;

        // the camera
        public static Camera _Camera;

        //the mouse
        public static MouseObj _Mouse;

        // A flag to prevent being initialized twice.
        internal static bool _initialized;

        // A Random number generator we can use throughout the game. S
        public static Random Random = new Random();

        // Creates our render window. Must be called once at startup.
        public static void Initialize(uint windowWidth, uint windowHeight, string windowTitle)
        {
            // Only initialize once.
            if (_initialized) return;
            _initialized = true;

            // Create the render window.
            _window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle, Styles.Fullscreen);
            _window.SetFramerateLimit(FramesPerSecond);

            // Add a method to be called whenever the "Closed" event fires.
            _window.Closed += ClosedEventHandler;

            // initializes camera
            _Camera = new Camera();

            //initializes mouse
            _Mouse = new MouseObj();

            ItemDat.Initialize();
            TileDat.Initialize();
        }

        // Called whenever you try to close the game window.
        internal static void ClosedEventHandler(object sender, EventArgs e)
        {
            // This indicates we should close the window, so just do that.
            _window.Close();
        }

        // Returns a reference to the game's RenderWindow.
        public static RenderWindow RenderWindow
        {
            get { return _window; }
        }

        // Get a texture (pixels) from a file
        public static Texture GetTexture(string fileName)
        {
            Texture texture;

            if (Textures.TryGetValue(fileName, out texture)) return texture;

            texture = new Texture(fileName);
            Textures[fileName] = texture;
            return texture;
        }
        // Get a sound from a file
        public static SoundBuffer GetSoundBuffer(string fileName)
        {
            SoundBuffer soundBuffer;

            if (Sounds.TryGetValue(fileName, out soundBuffer)) return soundBuffer;

            soundBuffer = new SoundBuffer(fileName);
            Sounds[fileName] = soundBuffer;
            return soundBuffer;
        }

        // Get a font from a file
        public static Font GetFont(string fileName)
        {
            Font font;

            if (Fonts.TryGetValue(fileName, out font)) return font;

            font = new Font(fileName);
            Fonts[fileName] = font;
            return font;
        }

        // Returns the active running scene.
        public static Scene CurrentScene
        {
            get { return _currentScene; }
        }

        // Specifies the next Scene to run.
        public static void SetScene(Scene scene)
        {
            // If we don't have a current scene, set it.
            // Otherwise, note the next scene.
            if (_currentScene == null)
                _currentScene = scene;
            else
                _nextScene = scene;
        }

        // Begins the main game loop with the initial scene.
        public static void Run()
        {
            Clock clock = new Clock();

            // Keep looping until the window closes.
            while (_window.IsOpen)
            {
                // If the next scene has been set, swap it with the current scene.
                if (_nextScene != null)
                {
                    _currentScene = _nextScene;
                    _nextScene = null;
                    clock.Restart();
                }

                // Get the time since the last frame, then have the scene update itself.
                Time time = clock.Restart();
                _currentScene.Update(time);
            }
        }
        public static Vector2f GetLocalMousePos()
        {
            Vector2f mousPos = (Vector2f)Mouse.GetPosition();
            Vector2f windowPos = (Vector2f)_window.Position;

            return new Vector2f((mousPos.X - windowPos.X) * ((float)MyGameb.WindowWidth / (float)_window.Size.X), ((mousPos.Y - windowPos.Y) * ((float)MyGameb.WindowHeight / (float)_window.Size.Y)));
        }
        public static Vector2f GetGlobalMousePos()
        {
            return GetLocalMousePos() + _Camera.position;
        }
    }
}