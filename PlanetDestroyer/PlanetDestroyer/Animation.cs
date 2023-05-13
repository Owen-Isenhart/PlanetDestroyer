using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanetDestroyer
{
    public class Animation
    {
        public int frameIndex, framesInbetween, ogFrames;
        public bool repeat, finished;
        public List<Rectangle> frameRects;
        public string anim;
        public Animation(string state, int f, List<Rectangle> sourceRects)
        {
            framesInbetween = f;
            ogFrames = f;
            frameIndex = 0;
            repeat = false;
            finished = false;
            frameRects = sourceRects;
            anim = state;
        }
        public void Reset(string state, List<Rectangle> sourceRects)
        {
            framesInbetween = ogFrames;
            frameIndex = 0;
            repeat = false;
            finished = false;
            frameRects = sourceRects;
            anim = state;
        }
        public void Update()
        {
            if (framesInbetween == 0 && !finished)
            {
                frameIndex++;
                framesInbetween = ogFrames;
                if (frameIndex == frameRects.Count)
                {
                    
                    if (!repeat)
                    {
                        frameIndex--;
                        finished = true;
                    }
                        
                    else
                        frameIndex = 0;
                }
            }
            else if (!finished)
                framesInbetween--;
        }
    }
}
