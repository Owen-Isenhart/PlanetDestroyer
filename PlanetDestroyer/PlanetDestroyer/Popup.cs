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
    public class Popup
    {
        public Rectangle popupRect;
        public string text;
        public string orientation;
        public bool shown;
        public Popup()
        {
            popupRect = new Rectangle(0, 0, 250, 150);
            shown = false;
            text = "";
            orientation = "";
        }
        
    }
}
