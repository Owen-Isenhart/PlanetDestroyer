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
    public class Money : Popup
    {
        public int runAmount, lifeAmount, index, comets, deltaC;
        public double multiplier, deltaM;
   
        public Rectangle border, moneyRect, cometRect;
        
        public Money() : base()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, 0, (int)(Game1.screenW / 2.5), Game1.screenH / 10 - 1);
            runAmount = lifeAmount = 0;
            index = 1;
            comets = 0;
            deltaC = 0;
            multiplier = 1;
            deltaM = 0;
        }
        public void Update()
        {
            
            int delta = (int)(index * 100 * multiplier);
            runAmount += delta;
            lifeAmount += delta;
            index++;

            deltaC = comets - (int)Math.Sqrt(lifeAmount / Math.Pow(10, 4)); //increase in comets
            deltaM = 1 + (double)deltaC / 5;
        }
        public void Prestige()
        {
            index = 1;
            runAmount = 0;
            comets = (int)Math.Sqrt(lifeAmount / Math.Pow(10, 4));
            multiplier = 1 + (double)comets / 5;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //money icon or something
            spriteBatch.Draw(Game1.pixel, border, Color.White);
        }
    }
}
