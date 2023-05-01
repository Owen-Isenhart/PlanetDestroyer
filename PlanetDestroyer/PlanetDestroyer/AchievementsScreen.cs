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
    public class AchievementsScreen
    {
        public Rectangle border;
        public HashSet<Achievement> achievements;
        //public 
        public AchievementsScreen()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, Game1.screenH / 2 + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 2);
            achievements = new HashSet<Achievement>();
            populateAchievements();
        }
        public void populateAchievements()
        {
            //alternate planets destroyed, money collected, and things bought in shop
        }
        public void Update()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            //foreach
        }
    }
}
