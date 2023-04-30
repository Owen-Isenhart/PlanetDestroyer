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
        public Explosion(Rectangle pos, string i) : base("linear", Game1.explosionRects[i])
        {
            rect = new Rectangle(pos.X, pos.Y, Game1.screenW / 30, Game1.screenH / 30);
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
