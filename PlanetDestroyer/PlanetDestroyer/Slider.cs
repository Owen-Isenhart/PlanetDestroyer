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
    public class Slider
    {
        public Rectangle knob, line;
        public bool knobHeld, mouseInBounds, hover;
        public int sliderValue;
        SpriteFont font;
        public Slider(Rectangle pos)
        {
            line = new Rectangle(pos.X, pos.Y, Game1.screenW / 6, 5);
            knob = new Rectangle(line.Right - 18, line.Y - 6, 18, 18);
            knobHeld = false;
            hover = false;
            mouseInBounds = true;
            sliderValue = 100;
            font = Game1.getFont(6);
        }
        public void setByPercent(int value)
        {
            sliderValue = value;
            knob.Y = line.Y - 6;
            knob.X = (((line.Right - line.X - knob.Width) * value) / 100) + line.X;
        }
        public void Update()
        {
            if (Game1.mouse.X > 0 && Game1.mouse.X < Game1.screenW)
                mouseInBounds = true;
            else
                mouseInBounds = false;

            if (knobHeld && mouseInBounds)
            {
                knob.X = Game1.mouse.X - 5;
                if (knob.X < line.X)
                    knob.X = line.X;
                else if (knob.X > line.Right - knob.Width)
                    knob.X = line.Right - knob.Width;
                //sliderValue = (knob.X - line.X) / (line.Width / 100);
                double min = (knob.X - line.X);
                double range = (line.Right - line.X - knob.Width);
                sliderValue = (int)(100 * (min / range));
                //setByPercent(sliderValue);
                //if (sliderValue > 100)
                    //Console.WriteLine("how");
            }

            if (knobHeld && Game1.mouse.LeftButton == ButtonState.Released)
            {
                knobHeld = false;
            }
            else if (Game1.mouseRect.Intersects(knob) && Game1.oldMouse.LeftButton == ButtonState.Released && Game1.mouse.LeftButton == ButtonState.Pressed)
            {
                knobHeld = true;
            }
            else if (Game1.mouseRect.Intersects(knob))
            {
                hover = true;
            }
            else
                hover = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, line, Color.White);
            if (!hover && !knobHeld)
                spriteBatch.Draw(Game1.whitePixel, knob, Color.Green);
            else
                spriteBatch.Draw(Game1.whitePixel, knob, Color.DarkGreen);
            spriteBatch.DrawString(font, sliderValue + "%", new Vector2(knob.Center.X - font.MeasureString(sliderValue + "%").X / 2, knob.Bottom), Color.Black);
        }
    }
}
