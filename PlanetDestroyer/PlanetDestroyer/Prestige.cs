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
    public class Prestige
    {
        public Texture2D[] prestigeTextures;
        public Rectangle[] prestigeRects;
        public ModalPopup[] confirmations;
        public PrestigeItem[] prestiges;
        public Rectangle prestigeBorder;
        public SpriteFont font;
        public static int totalPrestiges;
        public int confirmationIndex;
        public Prestige()
        {
            prestigeBorder = new Rectangle(0, Game1.store.storeBorder.Bottom + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, Game1.screenH - Game1.store.storeBorder.Bottom);
            prestigeRects = new Rectangle[3];
            prestigeTextures = new Texture2D[3];
            confirmations = new ModalPopup[3];
            prestigeTextures[0] = Game1.prestigeDmg; prestigeTextures[1] = Game1.prestigeMoney; prestigeTextures[2] = Game1.prestigeCost;
            for (int i = 0; i < 3; i++)
            {
                prestigeRects[i] = new Rectangle(prestigeBorder.X + (int)(prestigeBorder.Width / 5.8) + prestigeBorder.Width / 4 * i, prestigeBorder.Y + prestigeBorder.Height / 3 + 15, (int)(prestigeBorder.Width / 5.8), (int)(prestigeBorder.Width / 5.8));
                confirmations[i] = new ModalPopup();
            }
            
            prestiges = new PrestigeItem[3];
            prestiges[0] = new PrestigeItem(prestigeRects[0], prestigeTextures[0], 4, 4, 13);
            prestiges[1] = new PrestigeItem(prestigeRects[1], prestigeTextures[1], 3, 3, 9);
            prestiges[2] = new PrestigeItem(prestigeRects[2], prestigeTextures[2], 4, 4, 15);
            
            font = Game1.getFont(2);
            totalPrestiges = 0;
            confirmationIndex = 0;
            setupPopups();
        }
        public void setupPopups()
        {
            //dmg
            confirmations[0].text.Add("Damage Prestige"); confirmations[0].positions.Add(new Vector2(confirmations[0].window.Center.X - confirmations[0].font.MeasureString(confirmations[0].text[0]).X / 2, confirmations[0].window.Y + 10));
            confirmations[0].text.Add("Prestiging will reset your game for an added benefit"); 
            confirmations[0].text.Add("The percentage of increase is proportional to your comets"); 
            confirmations[0].text.Add("This prestige will increase all damage done by _%"); 
            confirmations[0].text.Add("Are you sure you want to prestige?"); 
            for (int i = 1; i < 5; i++)
            {
                confirmations[0].positions.Add(new Vector2(confirmations[0].window.Center.X - confirmations[0].font.MeasureString(confirmations[0].text[i]).X / 2, (int)(confirmations[0].window.Y + (i + 1.5) * confirmations[0].font.MeasureString(confirmations[0].text[i]).Y + 10 * i)));
            }
            confirmations[0].buttons.Add(new Rectangle(confirmations[0].window.Center.X - confirmations[0].window.Width / 10, (int)(confirmations[0].window.Y + (4.5) * confirmations[0].font.MeasureString(confirmations[0].text[1]).Y + 10 * 5) + confirmations[0].window.Width / 10, confirmations[0].window.Width / 5, confirmations[0].window.Width / 10));
            confirmations[0].buttonHovers.Add(false);
            confirmations[0].buttonStates.Add(true);
            confirmations[0].text.Add("Yes"); confirmations[0].positions.Add(new Vector2(confirmations[0].buttons[0].Center.X - confirmations[0].font.MeasureString("Yes").X / 2, confirmations[0].buttons[0].Center.Y - confirmations[0].font.MeasureString("Yes").Y / 2));


            //money
            confirmations[1].text.Add("Money Prestige"); confirmations[1].positions.Add(new Vector2(confirmations[1].window.Center.X - confirmations[1].font.MeasureString(confirmations[1].text[0]).X / 2, confirmations[1].window.Y + 10));
            confirmations[1].text.Add("Prestiging will reset your game for an added benefit");
            confirmations[1].text.Add("The percentage of increase is proportional to your comets");
            confirmations[1].text.Add("This prestige will increase money gained by _%");
            confirmations[1].text.Add("Are you sure you want to prestige?");
            for (int i = 1; i < 5; i++)
            {
                confirmations[1].positions.Add(new Vector2(confirmations[0].window.Center.X - confirmations[0].font.MeasureString(confirmations[0].text[i]).X / 2, (int)(confirmations[0].window.Y + (i + 1.5) * confirmations[0].font.MeasureString(confirmations[0].text[i]).Y + 10 * i)));
            }
            confirmations[1].buttons.Add(new Rectangle(confirmations[0].window.Center.X - confirmations[0].window.Width / 10, (int)(confirmations[0].window.Y + (4.5) * confirmations[0].font.MeasureString(confirmations[0].text[1]).Y + 10 * 5) + confirmations[0].window.Width / 10, confirmations[0].window.Width / 5, confirmations[0].window.Width / 10));
            confirmations[1].buttonHovers.Add(false);
            confirmations[1].buttonStates.Add(true);
            confirmations[1].text.Add("Yes"); confirmations[1].positions.Add(new Vector2(confirmations[1].buttons[0].Center.X - confirmations[1].font.MeasureString("Yes").X / 2, confirmations[1].buttons[0].Center.Y - confirmations[1].font.MeasureString("Yes").Y / 2));

            //sale
            confirmations[2].text.Add("Price Prestige"); confirmations[2].positions.Add(new Vector2(confirmations[2].window.Center.X - confirmations[2].font.MeasureString(confirmations[2].text[0]).X / 2, confirmations[2].window.Y + 10));
            confirmations[2].text.Add("Prestiging will reset your game for an added benefit");
            confirmations[2].text.Add("The percentage of increase is proportional to your comets");
            confirmations[2].text.Add("This prestige will decrease all prices by _%");
            confirmations[2].text.Add("Are you sure you want to prestige?");
            for (int i = 1; i < 5; i++)
            {
                confirmations[2].positions.Add(new Vector2(confirmations[0].window.Center.X - confirmations[0].font.MeasureString(confirmations[0].text[i]).X / 2, (int)(confirmations[0].window.Y + (i + 1.5) * confirmations[0].font.MeasureString(confirmations[0].text[i]).Y + 10 * i)));
            }
            confirmations[2].buttons.Add(new Rectangle(confirmations[0].window.Center.X - confirmations[0].window.Width / 10, (int)(confirmations[0].window.Y + (4.5) * confirmations[0].font.MeasureString(confirmations[0].text[1]).Y + 10 * 5) + confirmations[0].window.Width / 10, confirmations[0].window.Width / 5, confirmations[0].window.Width / 10));
            confirmations[2].buttonHovers.Add(false);
            confirmations[2].buttonStates.Add(true);
            confirmations[2].text.Add("Yes"); confirmations[2].positions.Add(new Vector2(confirmations[2].buttons[0].Center.X - confirmations[2].font.MeasureString("Yes").X / 2, confirmations[2].buttons[0].Center.Y - confirmations[2].font.MeasureString("Yes").Y / 2));
        }
        public void resizeComponents()
        {
            prestigeBorder = new Rectangle(0, Game1.store.storeBorder.Bottom + 1, (Game1.screenW / 2) - (int)(Game1.screenW / 2.5) / 2 - 1, Game1.screenH - Game1.store.storeBorder.Bottom);
            for (int i = 0; i < 3; i++)
            {
                prestiges[i].rect = new Rectangle(prestigeBorder.X + (int)(prestigeBorder.Width / 5.8) + prestigeBorder.Width / 4 * i, prestigeBorder.Y + prestigeBorder.Height / 3 + 15, (int)(prestigeBorder.Width / 5.8), (int)(prestigeBorder.Width / 5.8));
                confirmations[i].resizeComponents();
                confirmations[i].buttons.Clear();
                confirmations[i].buttonHovers.Clear();
                confirmations[i].buttonStates.Clear();
                confirmations[i].text.Clear();
                confirmations[i].positions.Clear();
            }
            font = Game1.getFont(2);

            setupPopups();
        }
        public void Update()
        {
            int count = 0;
            for (int i = 0; i < prestiges.Length; i++)
            {
                prestiges[i].Update();
                confirmations[i].Update();
                if (prestiges[i].active && Game1.mouse.LeftButton == ButtonState.Pressed && Game1.oldMouse.LeftButton == ButtonState.Released && !Game1.activePrestigeModal)
                {
                    confirmations[i].active = true;
                    confirmationIndex = i;
                }
                if (confirmations[i].active)
                    count++;

                if (!confirmations[i].buttonStates[0])
                {
                    //do the actual prestige thing
                    totalPrestiges++;
                    confirmations[i].active = false;
                    confirmations[i].buttonStates[0] = true;
                }
            }
            if (count > 0)
                Game1.activePrestigeModal = true;
            else
                Game1.activePrestigeModal = false;

            
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.pixel, prestigeBorder, Color.Black);
            spriteBatch.DrawString(font, "PRESTIGE", new Vector2(Game1.store.storeBorder.Width / 2 - font.MeasureString("PRESTIGE").X / 2, prestigeBorder.Y + 10), Color.White);
        
            //foreach (PrestigeItem p in prestiges)
            //{
            //    p.Draw(spriteBatch);
            //}
            
            for (int i = 0; i < prestiges.Length; i++)
            {
                prestiges[i].Draw(spriteBatch);
                
            }
            if (confirmations[confirmationIndex].active)
                confirmations[confirmationIndex].Draw(spriteBatch);
        }
    }
}
