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
    public class Upgrades : Popup
    {
        public Rectangle border;
        public Vector2 titlePos;
        public List<Rectangle> rects; //cant use a scrollview for this stuff because I coded it in a way where I can't have something be layed out horizontally without a ton of elements :(
        public List<Texture2D> textures;
        public int hoveringIndex;
        public SpriteFont font, smallFont;
        public static int totalUpgrades;
        public List<string> popupText;
        public List<int> prices, ogPrices;
        public List<double> increases;
        public Upgrades() : base()
        {
            font = Game1.getFont(1);
            smallFont = Game1.getFont(7);
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH/2);
            int diff = (int)(border.Width - font.MeasureString("UPGRADES").X);
            titlePos = new Vector2(border.X + diff / 2, border.Y + font.MeasureString("UPGRADES").Y/4);
            rects = new List<Rectangle>();
            textures = new List<Texture2D> { Game1.clickUpgrade, Game1.shipUpgrade, Game1.ballUpgrade, Game1.spikyUpgrade };
            hoveringIndex = -1;
            popupText = new List<string> { "a", "b", "c", "d", "e" };
            increases = new List<double> { 1, 1, 1 };
            prices = new List<int> { (int)(250 * Game1.prestige.costDecrease), (int)(500 * Game1.prestige.costDecrease), (int)(1000 * Game1.prestige.costDecrease), (int)(2000 * Game1.prestige.costDecrease) };
            ogPrices = new List<int> { (int)(250 * Game1.prestige.costDecrease), (int)(500 * Game1.prestige.costDecrease), (int)(1000 * Game1.prestige.costDecrease), (int)(2000 * Game1.prestige.costDecrease) };
            int x = border.Center.X - border.Width / 5 - border.Width/40;
            int y = border.Center.Y - border.Width / 10;
            for (int i = 0; i < 4; i++)
            {
                rects.Add(new Rectangle(x, y, border.Width / 5, border.Width / 5));
                x += border.Width / 5 + border.Width / 20;
                if (i == 1)
                {
                    x = border.Center.X - border.Width / 5 - border.Width / 40;
                    y += border.Width / 5 + border.Width / 20;
                }
                
            }
            popupRect.Width = (int)(Game1.screenW / 4.5);
            popupRect.Height = Game1.screenH / 7;
            totalUpgrades = 0;
        }
        public void resizeComponents()
        {
            smallFont = Game1.getFont(7);
            font = Game1.getFont(1);
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 2);
            int diff = (int)(border.Width - font.MeasureString("UPGRADES").X);
            titlePos = new Vector2(border.X + diff / 2, border.Y + font.MeasureString("UPGRADES").Y / 4);
            rects = new List<Rectangle>();
            textures = new List<Texture2D> { Game1.clickUpgrade, Game1.shipUpgrade, Game1.ballUpgrade, Game1.spikyUpgrade };
            hoveringIndex = -1;

            int x = border.Center.X - border.Width / 5 - border.Width / 40;
            int y = border.Center.Y - border.Width / 10;
            for (int i = 0; i < 4; i++)
            {
                rects.Add(new Rectangle(x, y, border.Width / 5, border.Width / 5));
                x += border.Width / 5 + border.Width / 20;
                if (i == 1)
                {
                    x = border.Center.X - border.Width / 5 - border.Width / 40;
                    y += border.Width / 5 + border.Width / 20;
                }

            }
            //popupRect.Y = rects[0].Bottom + 10;
            popupRect.Width = (int)(Game1.screenW / 4.5);
            popupRect.Height = Game1.screenH / 7;
        }
        public void Prestige()
        {
            popupText = new List<string> { "a", "b", "c", "d", "e" };
            increases = new List<double> { 1, 1, 1 };
            prices = new List<int> { (int)(250 * Game1.prestige.costDecrease), (int)(500 * Game1.prestige.costDecrease), (int)(1000 * Game1.prestige.costDecrease), (int)(2000 * Game1.prestige.costDecrease) };
            ogPrices = new List<int> { (int)(250 * Game1.prestige.costDecrease), (int)(500 * Game1.prestige.costDecrease), (int)(1000 * Game1.prestige.costDecrease), (int)(2000 * Game1.prestige.costDecrease) };

        }
        public void Update()
        {
            for (int i = 0; i < rects.Count; i++)
            {
                if (Game1.mouseRect.Intersects(rects[i]))
                {
                    if (Game1.cursorSound && !Game1.oldMouseRect.Intersects(rects[i]))
                        Game1.sounds[0].Play(volume: Game1.soundsVolume, pitch: 1f, pan: 0f);

                    hoveringIndex = i;
                    popupRect.X = rects[i].X - popupRect.Width - 10;
                    popupRect.Y = Game1.mouse.Y - popupRect.Height / 2;
                    shown = true;
                    
                    if (Game1.oldMouse.LeftButton == ButtonState.Released && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.money.runAmount >= prices[i])
                    {
                        Game1.money.runAmount -= prices[i];
                        Money.lifetimeSpent += prices[i];
                        totalUpgrades++;
                        prices[i] = prices[i] + ogPrices[i];


                        if (i == 0)
                        {
                            Game1.clickDamage += 2;
                        }
                        else
                        {
                            increases[i-1] += 0.01;
                            //Game1.store.shipDamages[i-1] = (ulong)(Game1.store.shipDamages[i-1] * increases[i-1]);
                        }
                    }
                    break;
                }
                else
                {
                    hoveringIndex = -1;
                    shown = false;
                }
                    
            }
            if (shown)
            {
                switch (hoveringIndex)
                {
                    case 0:
                        popupText[0] = "Click Upgrade";
                        popupText[1] = "$" + prices[0];
                        popupText[2] = "Increases click damage by 2";
                        popupText[3] = "Current click damage: " + Game1.clickDamage;
                        popupText[4] = "";
                        break;
                    case 1:
                        popupText[0] = "Missile-ship Upgrade";
                        popupText[1] = "$" + prices[1];
                        popupText[2] = "Increases missile-ship damage by 1%";
                        popupText[3] = "Current missile-ship damage: " + Game1.store.shipDamages[0];
                        popupText[4] = "Current % increase: " + Math.Round((increases[0] - 1) * 100, 0);
                        break;
                    case 2:
                        popupText[0] = "Gunner-ship Upgrade";
                        popupText[1] = "$" + prices[2];
                        popupText[2] = "Increases gunner-ship damage by 1%";
                        popupText[3] = "Current gunner-ship damage: " + Game1.store.shipDamages[1];
                        popupText[4] = "Current % increase: " + Math.Round((increases[1] - 1) * 100, 0);
                        break;
                    case 3:
                        popupText[0] = "Laser-ship Upgrade";
                        popupText[1] = "$" + prices[3];
                        popupText[2] = "Increases laser-ship damage by 1%";
                        popupText[3] = "Current laser-ship damage: " + Game1.store.shipDamages[2];
                        popupText[4] = "Current % increase: " + Math.Round((increases[2] - 1) * 100, 0);
                        break;
                }
            }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            spriteBatch.DrawString(font, "UPGRADES", titlePos, Color.White);

            for (int i = 0; i < rects.Count; i++)
            {
                
                if (hoveringIndex == i)
                    spriteBatch.Draw(Game1.whitePixel, rects[i], Color.White * .3f);
                else
                    spriteBatch.Draw(Game1.whitePixel, rects[i], Color.White * .1f);

                if (i == 1)
                    spriteBatch.Draw(textures[i], new Rectangle(rects[i].X + 12, rects[i].Y + 12, rects[i].Width - 24, rects[i].Height - 24), null, Color.White, (float)(4 * Math.PI / 3), new Vector2(360, 110), SpriteEffects.None, 0); 
                else if (i != 0)
                    spriteBatch.Draw(textures[i], new Rectangle(rects[i].X + 10, rects[i].Y + 10, rects[i].Width - 20, rects[i].Height - 20), Color.White);
                else //click texture is weird
                    spriteBatch.Draw(textures[i], new Rectangle(rects[i].X + 5, rects[i].Y + 10, rects[i].Width - 20, rects[i].Height - 20), new Rectangle(0, 0, 300, 306), Color.White);
            }

            if (shown)
            {
                spriteBatch.Draw(Game1.whitePixel, popupRect, Color.White * .7f);
                for (int i = 0; i < popupText.Count; i++)
                {
                    spriteBatch.DrawString(smallFont, popupText[i], new Vector2(popupRect.X + 5, popupRect.Y + 5 + (i * smallFont.MeasureString(popupText[i]).Y)), Color.Black);
                }
            }
                
        }
    }
}
