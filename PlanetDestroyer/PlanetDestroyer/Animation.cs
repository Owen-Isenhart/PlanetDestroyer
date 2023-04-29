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
    class Animation
    {
        public int frameIndex, framesInbetween;
        public bool repeat, finished;
        public List<Rectangle> frameRects;
        public string anim;
        public Animation(string state, List<Rectangle> sourceRects)
        {
            framesInbetween = 60;
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
                if (frameIndex == frameRects.Count)
                    frameIndex = 0;
                if (repeat)
                    framesInbetween = 60;
                else
                    finished = true;
                    
            }
        }
    }
}
