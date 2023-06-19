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
        public Upgrades() : base()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 + (int)(Game1.screenW / 2.5) + 1, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH/2);
            int diff = (int)(border.Width - Game1.fonts[3].MeasureString("UPGRADES").X);
            titlePos = new Vector2(border.X + diff / 2, border.Y + Game1.fonts[3].MeasureString("UPGRADES").Y/2);
            rects = new List<Rectangle>();
            textures = new List<Texture2D> { Game1.clickUpgrade, Game1.shipUpgrade, Game1.ballUpgrade, Game1.spikyUpgrade };
            hoveringIndex = -1;

            for (int i = 0; i < 4; i++)
            {
                rects.Add(new Rectangle(border.X + (border.Width / 9 * i * 2) + border.Width / 12, border.Center.Y - border.Width / 18, border.Width / 6, border.Width / 6));
            }
            popupRect.Y = rects[0].Bottom + 10;
            popupRect.Width = 180;
            popupRect.Height = 110;
        }
        
        public void Update()
        {
            for (int i = 0; i < rects.Count; i++)
            {
                if (Game1.mouseRect.Intersects(rects[i]))
                {
                    hoveringIndex = i;
                    popupRect.X = rects[i].X + rects[i].Width / 2 - popupRect.Width / 2;
                    shown = true;
                    break;
                }
                else
                {
                    hoveringIndex = -1;
                    shown = false;
                }
                    
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            spriteBatch.DrawString(Game1.fonts[3], "UPGRADES", titlePos, Color.White);

            for (int i = 0; i < rects.Count; i++)
            {
                
                if (hoveringIndex == i)
                    spriteBatch.Draw(Game1.whitePixel, rects[i], Color.White * .3f);
                else
                    spriteBatch.Draw(Game1.whitePixel, rects[i], Color.White * .1f);

                if (i != 0)
                    spriteBatch.Draw(textures[i], new Rectangle(rects[i].X + 10, rects[i].Y + 10, rects[i].Width - 20, rects[i].Height - 20), Color.White);
                else //click texture is weird
                    spriteBatch.Draw(textures[i], new Rectangle(rects[i].X + 5, rects[i].Y + 10, rects[i].Width - 20, rects[i].Height - 20), new Rectangle(0, 0, 300, 306), Color.White);
            }

            if (shown)
                spriteBatch.Draw(Game1.whitePixel, popupRect, Color.White * .7f);
        }
    }
}
