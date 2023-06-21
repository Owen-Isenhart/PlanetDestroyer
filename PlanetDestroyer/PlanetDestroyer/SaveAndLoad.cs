using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace PlanetDestroyer
{
    public class SaveAndLoad
    {

        public static string Load(string FileSpec)
        {
            try
            {
                StreamReader fileIn = new StreamReader(FileSpec);
                string output = fileIn.ReadToEnd();
                return output;
            }
            catch (Exception)
            {
                return "";
            }


        }

        public static void Save(string data, string FileSpec)
        {
            StreamWriter fileOut = new StreamWriter(FileSpec, false);
            fileOut.WriteLine(data);
            fileOut.Close();
        }
    }
}
