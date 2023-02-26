using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//todo
//add default animation/frame
//set frame

namespace MyGame
{
    internal class SpriteRenderer
    {
        //basic sprite data
        private Sprite[] frames;
        public Vector2f position;
        public float angle = 0;

        //animations
        private List<List<int>> animations = new List<List<int>>()
        {
            new List<int> { 0, 1 },
        };

        //current animation data
        private float lastFrame = 0; //time since last frame
        private int currentAnimation = 0;
        private int currentFrame;
        private float secondsPerFrame;
        public bool playing = false;

        public SpriteRenderer(Sprite[] frames, Vector2f position)
        {
            this.frames = frames;
            this.currentFrame = 0;
            this.position = position;
        }
        public void Draw(float delta)
        {
            if (playing)
            {
                //animation logic
                lastFrame += delta;
                if (lastFrame >= secondsPerFrame) { currentFrame++; }
                if (currentFrame > animations[currentAnimation].Count) { playing = false; }
            }

            //end render
            frames[currentFrame].Position = position;
            frames[currentFrame].Rotation = angle;
            Game.RenderWindow.Draw(frames[currentFrame]);
        }
        public void PlayAnimation(int animationId)
        {
            currentAnimation = animationId;
            lastFrame = 0;
            currentFrame = animations[animationId][0];//set first frame
        }
    }
}
