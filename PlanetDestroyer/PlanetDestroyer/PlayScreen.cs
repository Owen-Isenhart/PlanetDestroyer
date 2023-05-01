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
    public class PlayScreen
    {
        public Rectangle border;
        public Planet planet;

        public PlayScreen()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5)/2, 0, (int)(Game1.screenW / 2.5), Game1.screenH);
            planet = new Planet(1);
        }
        public void Update()
        {
            planet.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            planet.Draw(spriteBatch);
        }
    }
}
