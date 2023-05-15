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
    public class Money
    {
        public int runAmount, lifeAmount, index;
        public double multiplier;
        
        public Money()
        {
            runAmount = lifeAmount = 0;
            index = 1;
            multiplier = 1;
        }
        public void Update()
        {
            int delta = (int)(index * 100 * multiplier);
            runAmount += delta;
            lifeAmount += delta;
            index++;
        }
        public void Prestige()
        {
            index = 1;
            runAmount = 0;
            multiplier = 1 + Math.Sqrt(lifeAmount / Math.Pow(10, 6));
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
