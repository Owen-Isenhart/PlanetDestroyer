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
            setupPopups();
        }
        public void setupPopups()
        {
            popups[0].buttons.Add(new Rectangle(popups[0].window.X + 10, popups[0].window.Y + 10, 100, 100));
            popups[0].buttonStates.Add(true);
            popups[0].buttonHovers.Add(false);
            popups[0].sliders.Add(new Slider(new Rectangle(popups[0].window.X + 10, popups[0].window.Y + 120, 100, 100)));
        }
        public void Update()
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                buttons[i].Update();
                popups[i].Update();
                if (buttons[i].clicked && !Game1.activeModal)
                {
                    popups[i].active = true;
                }
                
                if (popups[i].active)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                Game1.activeModal = true;
            }
            else
            {
                Game1.activeModal = false;
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
