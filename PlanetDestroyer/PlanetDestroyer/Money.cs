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
        public string[] popupText;
        public static int lifetimeMoney, lifetimeSpent;
        SpriteFont font;
        
        public Money() : base("linear", 9, Game1.cometSources)
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, 0, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            moneyRect = new Rectangle(border.X + border.Width / 10, border.Height/4, 71, 71);
            cometRect = new Rectangle(border.Right - border.Width / 5, moneyRect.Y - 10, 75, 75);
            popup = new Rectangle(0, moneyRect.Bottom, border.Width / 3, border.Height / 2 + border.Height / 5);
            popupShown = false;
            runAmount = lifeAmount = 0;
            index = 1;
            comets = 0;
            deltaC = 0;
            multiplier = 1;
            deltaM = 0;
            repeat = true;
            lifetimeMoney = 0;
            lifetimeSpent = 0;
            popupText = new string[] { "a", "b", "c" };
            font = Game1.getFont(6);
        }
        public void Update()
        {
            base.Update();
            if (Game1.mouseRect.Intersects(moneyRect))
            {
                popup.X = moneyRect.X + moneyRect.Width / 2 - popup.Width / 2;
                popupShown = true;
                double temp = runAmount;
                if (runAmount > 9999)
                {
                    temp = Math.Round(temp / 1000, 1);
                    popupText[0] = "$" + temp + "k";
                }
                else
                    popupText[0] = "$" + runAmount;
                popupText[1] = "Used for items";
                popupText[2] = "and upgrades";
            }
            else if (Game1.mouseRect.Intersects(cometRect))
            {
                popup.X = cometRect.X + cometRect.Width / 2 - popup.Width / 2;
                popupShown = true;
                double temp = comets;
                if (comets > 9999)
                {
                    temp = Math.Round(temp / 1000, 1);
                    popupText[0] = temp + "k comets";

                }
                else
                    popupText[0] = comets + " comets";

                popupText[1] = "Used for";
                popupText[2] = "prestiging";
            }
            else
                popupShown = false;
        }
        public void IncreaseMoney(int i)
        {
            runAmount += i * 100;
            lifeAmount += i * 100;
            lifetimeMoney = lifeAmount;
        }
        public void Prestige()
        {
            runAmount = 0;
            comets = (int)Math.Sqrt(lifeAmount / Math.Pow(10, 4));
            multiplier = 1 + (double)comets / 5;

            deltaC = comets - (int)Math.Sqrt(lifeAmount / Math.Pow(10, 4)); //increase in comets
            deltaM = 1 + (double)deltaC / 5;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            //money icon or something
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            spriteBatch.Draw(Game1.logo, new Rectangle(border.Center.X - 100, border.Y + 5, 200, 150), Color.White);
            spriteBatch.Draw(Game1.cash, moneyRect, Color.White);
            spriteBatch.Draw(Game1.cometSheet, cometRect, Game1.cometSources[frameIndex], Color.White);

            if (popupShown)
            {
                spriteBatch.Draw(Game1.whitePixel, popup, Color.LightGray);
                for (int i = 0; i < popupText.Length; i++)
                {
                    if (i == 2)
                        spriteBatch.DrawString(font, popupText[i], new Vector2(popup.Center.X - font.MeasureString(popupText[i]).X / 2, 3 + popup.Y + (3 + popup.Height / 4 * i)), Color.Black);
                    else
                        spriteBatch.DrawString(font, popupText[i], new Vector2(popup.Center.X - font.MeasureString(popupText[i]).X / 2, 3 + popup.Y + (popup.Height / 3 * i)), Color.Black);
                }
                
            }
        }
    }
}
