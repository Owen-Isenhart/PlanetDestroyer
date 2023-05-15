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
    public class StoreAndPrestige
    {
        public Rectangle border;
        public List<StoreItem> items;
        public List<int> indexes;
        public ScrollView grid;
        public int totalDmg;
        
        public StoreAndPrestige()
        {
            border = new Rectangle(0, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, Game1.screenH);
            items = new List<StoreItem>();
            indexes = new List<int>();
            totalDmg = 0;
            List<Rectangle> t = organizeRects(21);
            List<Texture2D> temp = new List<Texture2D>();
            List<Color> colors = new List<Color>();
            Texture2D tex = Game1.shipSheet;
            for (int i = 0; i < t.Count; i++)
            {
                //if (i % 3 == 0)
                //{
                //    if (tex == Game1.ship)
                //    {
                //        tex = Game1.questionMark;
                //    }

                //    else
                //    {
                //        tex = Game1.ship;
                //    }

                //}

                colors.Add(new Color(255, 255 - (i / 3) * 50, 255 - (i / 3) * 50));
                temp.Add(tex);
            }
            grid = new ScrollView(border, Game1.itemRects[0], t, temp, colors, 3);
        }
        public Rectangle calculateInitRect(int i)
        {
            return new Rectangle(Game1.playScreen.planet.rect.Center.X-40, Game1.playScreen.planet.rect.Center.Y+(i * 25), Game1.screenW / 25, Game1.screenW / 25);
        }
        public void populateStore()
        {
            List<Rectangle> rects = organizeRects(5);
            
        }
        public List<Rectangle> organizeRects(int amnt)
        {
            
            List<Rectangle> list = new List<Rectangle>();
            if (border.Width >= 320)
            {
                int y = border.Y + (int)(Game1.fonts[2].MeasureString("STORE").Y/1.3);
                for (int i = 0, x = 0; i < amnt; i++, x++)
                {
                    if (i % 3 == 0)
                    {
                        y += border.Width / 6 + 10;
                        x = 0;
                    }

                    list.Add(new Rectangle(border.X + (int)(1.1 * x * border.Width / 6) + (int)(border.Width / 4.25), y, border.Width / 6, border.Width / 6));
                }
            }
            return list;
        }
        public void DamagePlanet()
        {
            totalDmg = 0;
            foreach (StoreItem item in items)
            {
                totalDmg += item.dps;
            }
            Game1.playScreen.planet.Health -= (double)totalDmg/60;
        }

        public void Update()
        {
            grid.Update();
            DamagePlanet();
            for (int i = grid.lastRow * 3 - 9, x = 0; i < grid.lastRow * 3; i++, x++)
            {
                if (grid.hoveringIndex == i)
                {
                    grid.popups[i].shown = true;
                    grid.calculatePopup("left", x);

                    if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                    {
                        int temp = i / 3;
                        items.Add(new StoreItem("ship", i, calculateInitRect(temp), Game1.shipSheet));
                        items = items.OrderByDescending(o => o.index).ToList(); //to get correct overlapping when drawn
                    }
                }
                else
                {
                    grid.popups[i].shown = false;
                }
                if (i % 3 == 0) //planet
                {

                }
                else if (i % 3 == 1) //money
                {

                }
                else //shop
                {

                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Game1.pixel, border, Color.Black);
            spriteBatch.DrawString(Game1.fonts[2], "STORE", new Vector2(border.Width/2 - Game1.fonts[2].MeasureString("STORE").X/2, grid.rects[0].Y - (int)(Game1.healthFont.MeasureString("STORE").Y/1.3)), Color.White);
            grid.Draw(spriteBatch);

            foreach (StoreItem item in items)
            {
                item.Update();
                item.Draw(spriteBatch);
            }

        }
    }
}
