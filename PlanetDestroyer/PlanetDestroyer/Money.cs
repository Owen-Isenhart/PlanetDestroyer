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
    public class Money : Animation
    {
        public int runAmount, lifeAmount, index, comets, deltaC;
        public double multiplier, deltaM;
   
        public Rectangle border, moneyRect, cometRect, popup; //C# can't inherit more than 1 class so i can't just use my popup class :(
        public bool popupShown;
        public string popupText;
        
        public Money() : base("linear", 8, Game1.cometSources)
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, 0, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            moneyRect = new Rectangle(border.X + border.Width / 10, border.Height/4, 75, 75);
            cometRect = new Rectangle(border.Right - border.Width / 5, moneyRect.Y, 75, 75);
            popup = new Rectangle(0, moneyRect.Bottom, 250, 150);
            popupShown = false;
            runAmount = lifeAmount = 0;
            index = 1;
            comets = 0;
            deltaC = 0;
            multiplier = 1;
            deltaM = 0;
            repeat = true;
        }
        public void Update()
        {
            base.Update();
            if (Game1.mouseRect.Intersects(moneyRect))
            {
                popup.X = moneyRect.X + moneyRect.Width / 2 - popup.Width / 2;
                popupShown = true;
            }
            else if (Game1.mouseRect.Intersects(cometRect))
            {
                popup.X = cometRect.X + cometRect.Width / 2 - popup.Width / 2;
                popupShown = true;
            }
            else
                popupShown = false;
        }
        public void Prestige()
        {
            index = 1;
            runAmount = 0;
            comets = (int)Math.Sqrt(lifeAmount / Math.Pow(10, 4));
            multiplier = 1 + (double)comets / 5;


            int delta = (int)(index * 100 * multiplier);
            runAmount += delta;
            lifeAmount += delta;
            index++;

            deltaC = comets - (int)Math.Sqrt(lifeAmount / Math.Pow(10, 4)); //increase in comets
            deltaM = 1 + (double)deltaC / 5;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //money icon or something
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            spriteBatch.Draw(Game1.cash, moneyRect, Color.White);
            spriteBatch.Draw(Game1.cometSheet, cometRect, Game1.cometSources[frameIndex], Color.White);
            spriteBatch.DrawString(Game1.fonts[4], "Planet Destroyer", new Vector2(moneyRect.Right + 10, moneyRect.Y), Color.White) ;
            if (popupShown)
            {
                spriteBatch.Draw(Game1.whitePixel, popup, Color.White * .5f);

            }
        }
    }
}
