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
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5)/2, Game1.screenH / 7, (int)(Game1.screenW / 2.5), Game1.screenH - Game1.screenH / 4);
            planet = new Planet(1);
        }
        public void resizeComponents()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 7, (int)(Game1.screenW / 2.5), Game1.screenH - Game1.screenH / 4);
            planet.resizeComponents();
        }
        public void Prestige()
        {
            int clicks = Planet.totalClicks;
            int dest = Planet.totalDestroyed;
            planet = new Planet(1);
            //idk if this works cause static weird
            Planet.totalClicks = clicks;
            Planet.totalDestroyed = dest;
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
