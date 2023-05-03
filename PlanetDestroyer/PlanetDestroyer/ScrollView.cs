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
    public class ScrollView
    {
        public Rectangle bigBorder;
        public Rectangle border, scrollbarRect;
        public Dictionary<int, Rectangle> rects;
        public Dictionary<int, Texture2D> textures;
        public Dictionary<int, Color> colors;
        public bool hoveringBorder, clickingBar;
        public int hoveringIndex;


        public ScrollView(Rectangle b, List<Rectangle> r, List<Texture2D> t)
        {
            bigBorder = b;
            rects = new Dictionary<int, Rectangle>();
            textures = new Dictionary<int, Texture2D>();
            colors = new Dictionary<int, Color>();
            for (int i = 0; i < r.Count; i++)
            {
                rects.Add(i, r[i]);
                textures.Add(i, t[i]);
                colors.Add(i, Color.White * .1f);
            }
            hoveringIndex = -1;
            hoveringBorder = false;
            clickingBar = false;
            border = new Rectangle(rects[0].X, rects[0].Y, rects[rects.Count-1].X + rects[0].Width - rects[0].X, rects[12].Y + rects[0].Height - rects[0].Y);
            scrollbarRect = new Rectangle(border.X + border.Width - 4, border.Y, 8, (rects[12].Y+rects[12].Height - rects[0].Y) - ((rects.Count/5 - 3) * (b.Width / 8 + 10 + rects[0].Height))/3);
        }
        public void Update()
        {
            if (Game1.mouseRect.Intersects(bigBorder) || Game1.mouseRect.Intersects(scrollbarRect))
                hoveringBorder = true;
            else
                hoveringBorder = false;

            if (hoveringBorder)
            {
                if (Game1.mouseRect.Intersects(scrollbarRect) && !clickingBar && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                {
                    clickingBar = true;
                }
                else if (clickingBar && Game1.mouse.LeftButton == ButtonState.Released)
                {
                    clickingBar = false;
                }
                    

                if (clickingBar)
                {
                    int diff = Game1.mouse.Y - Game1.oldMouse.Y;
                    if (scrollbarRect.Y + diff >= border.Y && scrollbarRect.Y + diff <= border.Y + border.Height)
                    {
                        scrollbarRect.Y += diff;
                    }
                }

                for (int i = 0; i < rects.Count; i++)
                {
                    if (Game1.mouseRect.Intersects(rects[i]))
                    {
                        colors[i] = Color.White * .3f;
                        hoveringIndex = i;
                        if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                        {
                            //clicked
                        }
                    }
                    else
                    {
                        colors[i] = Color.White * .1f;
                    }
                }
            }
            else
            {
                if (hoveringIndex > 0)
                    colors[hoveringIndex] = Color.White * .1f;
                hoveringIndex = -1;
                
            }
        }
        public int finalShownIndex()
        {
            return 1; 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                //if (rects)
                spriteBatch.Draw(Game1.whitePixel, rects[i], colors[i]);
                spriteBatch.Draw(textures[i], rects[i], Color.White);
            }
            if (hoveringBorder)
                spriteBatch.Draw(Game1.whitePixel, scrollbarRect, Color.White);
        }
    }
}
