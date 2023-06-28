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
   
        public Rectangle border, moneyRect, cometRect, popup; //C# can't inherit more than 1 class so i can't just use my popup class :(
        public bool popupShown;
        public string[] popupText;
        public static int lifetimeMoney, lifetimeSpent;
        SpriteFont font;
        
        public Money() : base("linear", 9, Game1.cometSources)
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, 0, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            moneyRect = new Rectangle(border.X + border.Width / 10, border.Height/4, border.Width / 10, border.Width / 10);
            cometRect = new Rectangle(border.Right - border.Width / 5, moneyRect.Y - 10, 4 + border.Width / 10, 4 + border.Width / 10);
            popup = new Rectangle(0, moneyRect.Bottom, border.Width / 3, border.Height / 2 + border.Height / 5);
            popupShown = false;
            runAmount = lifeAmount = lifetimeMoney = 55555500;
            comets = 0;
            deltaC = (int)Math.Sqrt(lifeAmount / Math.Pow(10, 2)) - comets;
            comets += deltaC;
            index = 1;

            repeat = true;
            lifetimeSpent = 0;
            popupText = new string[] { "a", "b", "c" };
            font = Game1.getFont(7);
        }
        public void resizeComponents()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, 0, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            moneyRect = new Rectangle(border.X + border.Width / 10, border.Height / 4, border.Width / 10, border.Width / 10);
            cometRect = new Rectangle(border.Right - border.Width / 5, moneyRect.Y - 10, 4 + border.Width / 10, 4 + border.Width / 10);
            popup = new Rectangle(0, moneyRect.Bottom, border.Width / 3, border.Height / 2 + border.Height / 5);
            font = Game1.getFont(7);
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
            int value = (int)(i * 1500 * Game1.prestige.moneyIncrease);
            runAmount += value;
            lifeAmount += value;
            lifetimeMoney += value;

            deltaC = (int)Math.Sqrt(lifeAmount / Math.Pow(10, 2)) - comets;
            comets += deltaC;
        }
        
        public void Prestige()
        {
            runAmount = lifeAmount = 500;
            comets = (int)Math.Sqrt(lifeAmount / Math.Pow(10, 2));
            deltaC = 0;

        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            spriteBatch.Draw(Game1.logo, new Rectangle(border.Center.X - border.Width / 8, border.Y + 5, border.Width / 4, border.Width / 5), Color.White);
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
