using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WorldSim
{
    class UserInterface
    {
        private Texture2D uiTexture;
        private Texture2D uiTextureNoCountry;
        private Texture2D currentFlag;
        private Texture2D[] flags = new Texture2D[2];
        private Texture2D[] icons = new Texture2D[3];
        public void LoadContent(Game game)
        {
            uiTexture = game.Content.Load<Texture2D>("UI/uiTexture");
            uiTextureNoCountry = game.Content.Load<Texture2D>("UI/uiTexture_noCountry");
            flags[0] = game.Content.Load<Texture2D>("UI/Flags/flag_RU");
            flags[1] = game.Content.Load<Texture2D>("UI/Flags/flag_AU");
            icons[0] = game.Content.Load<Texture2D>("UI/Icons/warAlert_red");
            icons[1] = game.Content.Load<Texture2D>("UI/Icons/infectionAlert_amber");
            icons[2] = game.Content.Load<Texture2D>("UI/Icons/infectionAlert_red");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(uiTextureNoCountry, new Rectangle(0, 0, 1280, 720), Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, Country selectedCountry, SpriteFont font)
        {
            switch (selectedCountry.Name)
            {
                case "Australia": currentFlag = flags[1]; break;
                case "Russia": currentFlag = flags[0]; break;
            }
            Console.WriteLine(selectedCountry.Population);
            spriteBatch.Draw(uiTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(currentFlag, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.DrawString(font, selectedCountry.ScienceRating.ToString(), new Vector2(420, 648), Color.White);
            spriteBatch.DrawString(font, selectedCountry.HappinessRating.ToString(), new Vector2(445, 678), Color.White);
            spriteBatch.DrawString(font, selectedCountry.Population.ToString(), new Vector2(620, 678), Color.White);
            if (selectedCountry.Diseases.Count > 0 && selectedCountry.Diseases.Count < 3)
                spriteBatch.Draw(icons[1], new Rectangle(1043, 645, 35, 35), Color.White);
            else if (selectedCountry.Diseases.Count >= 3)
                spriteBatch.Draw(icons[2], new Rectangle(1043, 645, 35, 35), Color.White);
            if (selectedCountry.Enemies.Count > 0)
                spriteBatch.Draw(icons[0], new Rectangle(1043, 685, 35, 35), Color.White);
            
        }
    }
}
