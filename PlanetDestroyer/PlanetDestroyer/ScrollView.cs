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
            int full = rects[12].Y + rects[12].Height - rects[0].Y;
            int columns = rects.Count / 5;
            int cPerF = full / columns;
            scrollbarRect = new Rectangle(border.X + border.Width, border.Y, 8, 3*cPerF)  ;
        }
        public void Update()
        {
            if (Game1.mouseRect.Intersects(border) || Game1.mouseRect.Intersects(scrollbarRect))
                hoveringBorder = true;
            else
                hoveringBorder = false;

            if (hoveringBorder || clickingBar)
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
                    if (scrollbarRect.Y + diff >= border.Y && scrollbarRect.Y + scrollbarRect.Height + diff <= border.Y + border.Height)
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

            int center = scrollbarRect.Center.Y;
            int full = rects[12].Y + rects[12].Height - rects[0].Y;
            int columns = rects.Count / 5;
            int cPerF = full / columns / 2;
            for (int i = 0; i < columns * 2; i++)
            {
                if (Math.Abs(center - border.Y - cPerF) < 20)
                    return 2 + i;
                else
                    cPerF += cPerF;
            }
            return 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = finalShownIndex() * 5 - 15; i < finalShownIndex()*5; i++)
            {
                //if (rects)
                spriteBatch.Draw(Game1.whitePixel, rects[i], colors[i]);
                spriteBatch.Draw(textures[i], rects[i], Color.White);
            }
            if (hoveringBorder || clickingBar)
                spriteBatch.Draw(Game1.whitePixel, scrollbarRect, Color.White);
        }
    }
}
