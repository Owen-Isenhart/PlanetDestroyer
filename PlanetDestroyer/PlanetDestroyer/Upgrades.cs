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
    public class Upgrades //add dps increases to every ship of a single type
    {
        public Rectangle border;
        public Upgrades()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH/2);
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
        }
    }
}
