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
    public class Store
    {
        public Rectangle border;
        public HashSet<StoreItem> items;
        
        public Store()
        {
            border = new Rectangle(0, 0, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, Game1.screenH);
            items = new HashSet<StoreItem>();
            populateStore();
        }
        public void populateStore()
        {
            List<Rectangle> rects = organizeRects(5);
            items.Add(new StoreItem("Small Ship", 1, rects[0], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[1], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[2], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[3], Game1.ship));
            items.Add(new StoreItem("Small Ship", 1, rects[4], Game1.ship));
        }
        public List<Rectangle> organizeRects(int amnt)
        {
            List<Rectangle> list = new List<Rectangle>();
            if (border.Width >= 320)
            {
                int y = border.Height/21;
                //int x = 0;
                for (int i = 0, x = 0; i < amnt; i++, x++)
                {
                    if (i % 3 == 0)
                    {
                        y += 110 + border.Height / 23;
                        x = 0;
                    }
                    
                    list.Add(new Rectangle(x * 150 + border.Width/8, y, Game1.screenW/14, Game1.screenW/14));
                }
            }
            return list;
        }
        public void Update()
        {
            foreach (StoreItem item in items)
            {
                item.Update();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Game1.pixel, border, Color.Black);
            spriteBatch.DrawString(Game1.healthFont, "STORE", new Vector2(items.ElementAt(0).rect.X + Game1.healthFont.MeasureString("STORE").X/6, items.ElementAt(0).rect.Y - Game1.healthFont.MeasureString("STORE").Y), Color.White);
            foreach (StoreItem item in items)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
