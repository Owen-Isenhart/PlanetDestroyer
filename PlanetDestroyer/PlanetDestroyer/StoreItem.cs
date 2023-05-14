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
    public class StoreItem : Animation
    {
        public int price, dps, quantity, index, dist;
        public string name;
        public float angleX, angleY;
        public bool subX, subY;
        public Rectangle rect, intersectRect;
        public Texture2D texture;
        public Color color;
        public StoreItem(string n, int i, Rectangle r, Texture2D tex) : base("delayed-linear", 25, Game1.itemRects)
        {
            name = n;
            texture = tex;
            rect = r;
            intersectRect = new Rectangle(r.X, r.Y, 1, 1);
            index = i;
            dps = (int)Math.Pow(index+1, 2);
            quantity = 0;
            //repeat = true;
            angleY = 0;
            angleX = (float)Math.PI;
            subX = true;
            subY = true;
            dist = (int)(Game1.playScreen.planet.rect.Width*1.3);
            //dist = Game1.playScreen.planet.rect.Center.X - rect.Right;
            frameIndex = 2;
            framesInbetween = 13;
            color = new Color(255, 255 - (index / 3) * 50, 255 - (index / 3) * 50);
        }
        
        public void Update()
        {
            base.Update(); 
            /* 
            Essentially, this just models the items movement using a sine and cosine wave
            dist is just a pre-established constant determined by the size of the screen
            the rectangles position just changes by using the changing angles, sine/cosine respectively, and the dist constant
             */
            if (subX)
                angleX -= (float)Math.PI / 90;
            else
                angleX += (float)Math.PI / 90;

            if (subY)
                angleY -= (float)Math.PI / 90;
            else
                angleY += (float)Math.PI / 90;

            if (angleX >= Math.PI) 
                subX = true;
            else if (angleX <= 0)
                subX = false;
            
            if (angleY >= Math.PI / 2)
                subY = true;            
            else if (angleY <= -Math.PI / 2)
                subY = false;
            
            rect.X -= (int)(dist * Math.Cos(angleX)) / 50;
            rect.Y += (int)(dist * Math.Sin(angleY)) / 130;

            intersectRect.X = rect.Center.X;
            intersectRect.Y = rect.Center.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (subY)
                spriteBatch.Draw(texture, rect, Game1.itemRects[frameIndex], color);
            else if (!Game1.playScreen.planet.rect.Intersects(intersectRect))
            {
                spriteBatch.Draw(texture, rect, Game1.itemRects[frameIndex], color);

            }
            else
            {
                base.Reset("delayed-linear", Game1.itemRects);
            }
                //frameIndex = 0;
        }
    }
}
