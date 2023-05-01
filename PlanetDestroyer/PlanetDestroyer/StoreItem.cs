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
    public class StoreItem
    {
        public int price, dps, quantity, index;
        public bool unlocked;
        public string name;
        public Rectangle rect;
        public Color rectColor;
        public Texture2D texture;
        public StoreItem(string n, int i, Rectangle r, Texture2D tex)
        {
            name = n;
            texture = tex;
            unlocked = true;
            if (index != 1)
                unlocked = false;
            rect = r;
            index = i;
            dps = (int)Math.Pow(index, 2);
            quantity = 0;
            rectColor = Color.White*.1f;
        }
        public void Update()
        {
            Console.WriteLine(Game1.mouseRect + " " + rect);
            if (Game1.mouseRect.Intersects(rect))
            {
                
                rectColor = Color.LightGray * .5f;
                if (Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released && unlocked)
                {
                    //create popup later
                    quantity++;
                }
            }
            else
            {
                rectColor = Color.White*.1f;
            }
        }
        public Vector2 textPosition()
        {
            int widthDiff = (int)(rect.Width - Game1.fonts[5].MeasureString(name).X);
            return new Vector2(rect.X + widthDiff/2, rect.Y + rect.Height - Game1.fonts[5].MeasureString(name).Y - 5);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.whitePixel, rect, rectColor);
            spriteBatch.Draw(texture, new Rectangle(rect.X + 10, rect.Y, rect.Width - 20, rect.Height - 20), Color.White);
            spriteBatch.DrawString(Game1.fonts[5], name, textPosition(), Color.White);
        }
    }
}
