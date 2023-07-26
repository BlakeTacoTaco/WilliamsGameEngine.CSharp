using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class DebugText : GameObject
    {
        Text text = new Text();
        List<float> lastFrames = new List<float>();
        List<float> lastDraw = new List<float>();
        List<float> lastUpdate = new List<float>();
        public DebugText()
        {
            text.Font = Game.GetFont("../../../Resources/Courneuf-Regular.ttf");
            for (int i = 0; i < 60; i++)
            {
                lastFrames.Add(0);
                lastDraw.Add(0);
                lastUpdate.Add(0);
            }
        }
        public override void Update(Time elapsed)
        {
            lastFrames.RemoveAt(0);
            lastFrames.Add(elapsed.AsSeconds());

            float total = 0;
            for (int i = 0; i < lastFrames.Count; i++)
            {
                total += lastFrames[i];
            }
            float totalDraw = 0;
            for (int i = 0; i < lastDraw.Count; i++)
            {
                totalDraw += lastDraw[i];
            }
            float totalUpdate = 0;
            for (int i = 0; i < lastUpdate.Count; i++)
            {
                totalUpdate += lastUpdate[i];
            }

            text.DisplayedString =
                "fps:" + Math.Round(1 / (total / lastFrames.Count)) +
                "\ntotal: \t" + Math.Round(total / lastFrames.Count * 1000, 1) + "ms" +
                "\ndraw:  \t" + Math.Round(totalDraw / lastDraw.Count * 1000, 1) + "ms" +
                "\nupdate:\t" + Math.Round(totalUpdate / lastUpdate.Count * 1000, 1) + "ms";
        }
        public override void DrawSelf(Camera camera)
        {
            camera.DrawThis(text);
        }
        public void GiveDrawTime(Time time)
        {
            lastDraw.RemoveAt(0);
            lastDraw.Add(time.AsSeconds());
        }
        public void GiveUpdateTime(Time time)
        {
            lastUpdate.RemoveAt(0);
            lastUpdate.Add(time.AsSeconds());
        }
    }
}
