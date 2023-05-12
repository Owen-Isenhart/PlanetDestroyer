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
        public Rectangle rect;
        public Texture2D texture;
        public StoreItem(string n, int i, Rectangle r, Texture2D tex) : base("delayed-linear", Game1.itemRects)
        {
            name = n;
            texture = tex;
            rect = r;
            index = i;
            dps = (int)Math.Pow(index, 2);
            quantity = 0;
            repeat = true;
            angleY = 0;
            angleX = (float)Math.PI;
            subX = true;
            subY = true;
            dist = (int)(Game1.playScreen.planet.rect.Width*1.3);
            //dist = Game1.playScreen.planet.rect.Center.X - rect.Right;
        }
        
        public void Update()
        {
            if (subX)
            {
                angleX -= (float)Math.PI / 90;
            }
            
            else
            {
                angleX += (float)Math.PI / 90;
            }
                
            if (subY)
            {
                angleY -= (float)Math.PI / 90;

            }
            else
            {
                angleY += (float)Math.PI / 90;

            }

            if (angleX >= Math.PI) 
            {
                subX = true;

            } 
            else if (angleX <= 0)
            {
                subX = false;
            }
            if (angleY >= Math.PI / 2)
            {
                subY = true;
            } 
            else if (angleY <= -Math.PI / 2)
            {
                subY = false;
            }

            Console.WriteLine(subY);
            //dist += (int)(dist * Math.Cos(angle));
            rect.X -= (int)(dist * Math.Cos(angleX)) / 50;
            rect.Y += (int)(dist * Math.Sin(angleY)) / 130;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (subY)
                spriteBatch.Draw(texture, rect, Game1.itemRects[frameIndex], Color.White);
            else if (!Game1.playScreen.planet.rect.Intersects(rect))
            {
                spriteBatch.Draw(texture, rect, Game1.itemRects[frameIndex], Color.White);

            }
        }
    }
}
