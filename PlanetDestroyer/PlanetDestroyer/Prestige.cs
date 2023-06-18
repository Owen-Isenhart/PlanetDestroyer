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
    public class Prestige : Popup
    {
        public Texture2D[] prestigeTextures;
        public Rectangle[] prestigeRects;
        public ModalPopup[] confirmations;
        public PrestigeItem[] prestiges;
        public Rectangle prestigeBorder;
        public Prestige() : base()
        {
            prestigeBorder = new Rectangle(0, Game1.store.storeBorder.Bottom + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, Game1.screenH - Game1.store.storeBorder.Bottom);
            prestigeRects = new Rectangle[3];
            prestigeTextures = new Texture2D[3];
            prestigeTextures[0] = Game1.prestigeDmg; prestigeTextures[1] = Game1.prestigeMoney; prestigeTextures[2] = Game1.prestigeCost;
            for (int i = 0; i < 3; i++)
            {
                prestigeRects[i] = new Rectangle(prestigeBorder.X + (int)(prestigeBorder.Width / 5.8) + prestigeBorder.Width / 4 * i, prestigeBorder.Y + prestigeBorder.Height / 3 + 15, 100, 100);
            }
            confirmations = new ModalPopup[3];
            prestiges = new PrestigeItem[3];
            prestiges[0] = new PrestigeItem(prestigeRects[0], prestigeTextures[0], 4, 4, 13);
            prestiges[1] = new PrestigeItem(prestigeRects[1], prestigeTextures[1], 3, 3, 9);
            prestiges[2] = new PrestigeItem(prestigeRects[2], prestigeTextures[2], 4, 4, 15);
            popupRect.Width = 180;
            popupRect.Height = 80;
        }
        public void Update()
        {
            int count = 0;
            for (int i = 0; i < prestiges.Length; i++)
            {
                prestiges[i].Update();
                if (prestiges[i].active)
                {
                    count++;
                    popupRect.X = prestigeRects[i].X + prestigeRects[i].Width / 2 - popupRect.Width / 2;
                    popupRect.Y = prestigeRects[i].Bottom + 10;
                    shown = true;
                }
            }
            if (count == 0) shown = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, prestigeBorder, Color.Black);
            spriteBatch.DrawString(Game1.fonts[4], "PRESTIGE", new Vector2(Game1.store.storeBorder.Width / 2 - Game1.fonts[4].MeasureString("PRESTIGE").X / 2, prestigeBorder.Y + 10), Color.White);
        
            foreach (PrestigeItem p in prestiges)
            {
                p.Draw(spriteBatch);
            }
            if (shown)
                spriteBatch.Draw(Game1.whitePixel, popupRect, Color.White * .7f);
        }
    }
}
