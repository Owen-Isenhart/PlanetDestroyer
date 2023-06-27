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
    public class PrestigeItem : Animation
    {
        public Rectangle rect;
        public Texture2D tex;
        public bool active;
        public PrestigeItem(Rectangle re, Texture2D t, int r, int c, int i) : base("s", 5, Game1.rectsBySheet(r, c, 300, 300, i))
        {
            rect = re;
            tex = t;
            active = false;
            repeat = true;
        }
        public void Update()
        {
            if (active)
            {
                base.Update();
                if (frameIndex == 0) frameIndex = 1;
            }
            else
            {
                frameIndex = 0;
            }

            if (Game1.mouseRect.Intersects(rect) && !Game1.oldMouseRect.Intersects(rect))
            {
                frameIndex = 1;
                active = true;
                if (Game1.cursorSound)
                    Game1.sounds[0].Play(volume: Game1.soundsVolume, pitch: 1f, pan: 0f);
            }

            if (active && !Game1.mouseRect.Intersects(rect))
            {
                active = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, frameRects[frameIndex], Color.White);
        }
    }
}
