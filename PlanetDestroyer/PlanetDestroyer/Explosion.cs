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
    class Explosion : Animation
    {
        public Rectangle rect;
        public float angle;
        public int index; // 0 for small, 1 for minor, 2 for large
        public Explosion(Rectangle pos, int i) : base("linear", Game1.explosionRects)
        {
            rect = new Rectangle(pos.X, pos.Y, Game1.screenW / 30, Game1.screenH / 30);
            angle = (float)Math.Atan2(Game1.screenH / 2 - pos.Y, Game1.screenW / 2 - pos.X);
            index = i;
        }
        public void Update()
        {
            base.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.explosionTextures[index], rect, Game1.explosionRects[frameIndex], Color.White, angle, new Vector2(Game1.explosionTextures[index].Width / 2, Game1.explosionTextures[index].Height / 2), SpriteEffects.None, 0);
        }
    }
}
