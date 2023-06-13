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
    public class Button
    {
        public string text;
        public Rectangle rect;
        public Color color;
        public bool hovering, clicked;
        public Button(Rectangle r, string t)
        {
            rect = r;
            text = t;
            color = new Color(44, 44, 44);
            clicked = false;
        }
        public void Update()
        {
            if (Game1.mouseRect.Intersects(rect))
            {
                //color = Color.White * .3f;
                hovering = true;
                if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released)
                {
                    //color = Color.White * .4f;
                    clicked = true;
                }
                else
                {
                    clicked = false;
                }
            }
            else
            {
                //color = Color.White * .2f;
                hovering = false;
                clicked = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (hovering)
                spriteBatch.Draw(Game1.whitePixel, new Rectangle(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), Color.White);
            spriteBatch.Draw(Game1.whitePixel, rect, color);
            spriteBatch.DrawString(Game1.fonts[5], text, new Vector2(rect.Center.X - Game1.fonts[5].MeasureString(text).X / 2, rect.Center.Y - Game1.fonts[5].MeasureString(text).Y / 2), Color.White);
        }
    }
}
