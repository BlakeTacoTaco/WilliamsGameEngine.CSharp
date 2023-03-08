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

namespace MyGame.GameEngine
{
    internal class SpriteRenderer
    {
        //basic sprite data
        private Sprite[] frames;
        public Vector2f position;
        public float angle = 0;

        //animations
        private List<List<int>> animations = new List<List<int>>();

        //current animation data
        private float lastFrame = 0; //time since last frame
        private int currentAnimation = 0;//current animation ID (based on animations list
        private int currentFrame;//id of current frame
        private float secondsPerFrame;//how many seconds are in beteween each frame
        public bool playing = false;//if there is currently an animation playing
        public int defaultFrame = 0;//frame that is set to when animations are over
        private int frameInAnimation;//frame based on animation data

        public SpriteRenderer(Sprite[] frames, Vector2f position, int defaultFrame, Vector2f scale, Vector2f origin, List<List<int>> animations, float secondsPerFrame)
        {
            this.frames = frames;
            this.position = position;
            this.defaultFrame = defaultFrame;
            currentFrame = defaultFrame;
            this.animations = animations;
            this.secondsPerFrame = secondsPerFrame;
            for (int i = 0; i < frames.Length; i++)//applies stuff to each frame that only needs to be applied once
            {
                frames[i].Scale = scale;
                frames[i].Origin = origin;
            }
        }
        public void Draw(float delta)
        {
            if (playing && !(frameInAnimation + 1 >= animations[currentAnimation].Count))
            {
                //animation logic
                lastFrame += delta;
                if (lastFrame >= secondsPerFrame) { currentFrame = animations[currentAnimation][frameInAnimation + 1]; frameInAnimation++; }
            }
            else { playing = false; currentFrame = defaultFrame; }

            //end render
            frames[currentFrame].Position = position;
            frames[currentFrame].Rotation = angle;
            Game.RenderWindow.Draw(frames[currentFrame]);
        }
        public bool PlayAnimation(int animationId, bool force)
        {
            if (!playing || force)
            {
                currentAnimation = animationId;
                lastFrame = 0;
                currentFrame = animations[animationId][0];//set first frame
                frameInAnimation = 0;
                playing = true;
                return true;
            }
            return false;
        }
        public void SetFrame(int frame) //overrides current animation
        {
            playing = false;
            currentFrame = frame;
        }
    }
}
