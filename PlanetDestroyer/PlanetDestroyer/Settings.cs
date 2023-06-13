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
    public class Settings
    {
        public Rectangle border;
        public List<Button> buttons;
        public List<ModalPopup> popups;
        public Settings()
        {
            border = new Rectangle((Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2, Game1.screenH / 7 + Game1.screenH - Game1.screenH / 4 + 1, (int)(Game1.screenW / 2.5), Game1.screenH / 7 - 1);
            buttons = new List<Button>();
            popups = new List<ModalPopup>();
            string[] texts = { "Audio", "Video", "Stats" };
            for (int i = 0; i < 3; i++)
            {
                popups.Add(new ModalPopup());
                buttons.Add(new Button(new Rectangle(border.X + border.Width / 15 + border.Width / 3 * i, border.Y + 25, border.Width / 5, 50), texts[i]));
            }
            
        }
        public void Update()
        {
            for (int i = 0; i < 3; i++)
            {
                buttons[i].Update();
                popups[i].Update();
                if (buttons[i].clicked)
                {
                    popups[i].active = true;
                }
                

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, border, Color.White);
            
            for (int i = 0; i < 3; i++)
            {
                buttons[i].Draw(spriteBatch);
                if (popups[i].active)
                    popups[i].Draw(spriteBatch);
            }
        }
    }
}
