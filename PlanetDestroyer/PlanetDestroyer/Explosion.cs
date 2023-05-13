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
    public class Explosion : Animation
    {
        public Rectangle rect;
        public float angle;
        public string type; 
        public Explosion(Rectangle pos, string i) : base("linear", 3, Game1.explosionRects[i])
        {
            int size;
            if (i.Equals("small"))
            {
                size = 30;
                rect = new Rectangle(pos.X + pos.Width / 2 - Game1.screenW / size / 2, pos.Y + pos.Height / 2 - Game1.screenH / size / 2, Game1.screenW / size, Game1.screenH / size);
            }
            else if (i.Equals("large"))
            {
                size = 2;
                rect = new Rectangle(pos.X + pos.Width/2 - Game1.screenW / size / 2 - 10, pos.Y + pos.Height/2 - Game1.screenH / size / 2, Game1.screenW / size, Game1.screenH / size);
            }
            else size = 30;
            
            angle = (float)Math.Atan2(Game1.screenH / 2 - pos.Y, Game1.screenW / 2 - pos.X);
            type = i;
        }
        public void Update()
        {
            base.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.explosionsSheet, rect, Game1.explosionRects[type][frameIndex], Color.White);
        }
    }
}
