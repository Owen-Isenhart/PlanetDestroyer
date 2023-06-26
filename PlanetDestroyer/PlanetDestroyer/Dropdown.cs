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
    public class Dropdown
    {
        public List<Rectangle> rect;
        public List<string> options;
        public bool hover, opened, newIndex;
        public int hoverIndex, selectedIndex;
        SpriteFont font;
        public Dropdown (List<Rectangle> pos, List<string> o)
        {
            rect = pos;
            options = o;
            hover = false;
            opened = false;
            hoverIndex = 0;
            selectedIndex = 0;
            newIndex = false;
            font = Game1.getFont(7);
        }
        
        public void Update()
        {
            newIndex = false;
            for (int i = 0; i < rect.Count; i++)
            {
                if (!opened && i == 0)
                {
                    if (Game1.mouseRect.Intersects(rect[i]))
                    {
                        hover = true;
                        hoverIndex = i;
                        break;
                    }
                    else
                    {
                        hover = false;
                    }
                }
                else if (opened)
                {
                    if (Game1.mouseRect.Intersects(rect[i]))
                    {
                        hover = true;
                        hoverIndex = i;
                        break;
                    }
                    else
                    {
                        hover = false;
                    }
                }
                    
                    
            }
            
            if (hover && hoverIndex == 0)
            {
                //hover = true;
                //hoverIndex = 0;
                if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                {
                    opened = !opened;
                }
            }
            

            if (hover && hoverIndex != 0)
            {
                if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                {
                    selectedIndex = hoverIndex - 1;
                    opened = false;
                    hover = false;
                    newIndex = true;
                }
            }

            if (opened && !hover && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
            {
                opened = false;
            }
            
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (hoverIndex == 0 && hover)
                spritebatch.Draw(Game1.whitePixel, rect[0], Color.DarkGreen);
            else
                spritebatch.Draw(Game1.whitePixel, rect[0], Color.DarkGreen * .8f);

            spritebatch.DrawString(font, options[selectedIndex], new Vector2(rect[0].Center.X - font.MeasureString(options[selectedIndex]).X / 2, rect[0].Y), Color.Black);

            if (opened)
            {
                for (int i = 1, x = 0; i < rect.Count; i++, x++)
                {
                    if (hover && hoverIndex == i)
                        spritebatch.Draw(Game1.whitePixel, rect[i], Color.Green * .7f);
                    else
                        spritebatch.Draw(Game1.whitePixel, rect[i], Color.LightGreen);

                    spritebatch.DrawString(font, options[x], new Vector2(rect[i].Center.X - font.MeasureString(options[x]).X / 2, rect[i].Y), Color.Black);
                }
            }
        }
    }
}
