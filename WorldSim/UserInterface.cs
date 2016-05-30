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
        private Texture2D dateTexture;
        private Texture2D uiTextureNoCountry;
        private Texture2D currentFlag;
        private Texture2D[] flags = new Texture2D[5];
        private Texture2D[] icons = new Texture2D[3];
        private Button[] buttons = new Button[3];
        public void LoadContent(Game game)
        {
            uiTexture = game.Content.Load<Texture2D>("UI/uiTexture");
            dateTexture = game.Content.Load<Texture2D>("UI/uiTexture_date");
            uiTextureNoCountry = game.Content.Load<Texture2D>("UI/uiTexture_noCountry");
            flags[0] = game.Content.Load<Texture2D>("UI/Flags/flag_RU");
            flags[1] = game.Content.Load<Texture2D>("UI/Flags/flag_AU");
            flags[2] = game.Content.Load<Texture2D>("UI/Flags/flag_MN");
            flags[3] = game.Content.Load<Texture2D>("UI/Flags/flag_CN");
            flags[4] = game.Content.Load<Texture2D>("UI/Flags/flag_KZ");
            icons[0] = game.Content.Load<Texture2D>("UI/Icons/warAlert_red");
            icons[1] = game.Content.Load<Texture2D>("UI/Icons/infectionAlert_amber");
            icons[2] = game.Content.Load<Texture2D>("UI/Icons/infectionAlert_red");
            buttons[0] = new Button(new Rectangle(1043, 645, 35, 35), "UI/Icons/infectionAlert_amber", "Amber Disease Alert");
            buttons[1] = new Button(new Rectangle(1043, 645, 35, 35), "UI/Icons/infectionAlert_red", "Red Disease Alert");
            buttons[2] = new Button(new Rectangle(1043, 685, 35, 35), "UI/Icons/warAlert_red", "Red War Alert");
            foreach (Button button in buttons)
            {
                button.clickEvent += OnButtonClick;
                button.LoadContent(game);
            }
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, int day, int year)
        {
            spriteBatch.Draw(uiTextureNoCountry, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(dateTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.DrawString(font, day.ToString(), new Vector2(78, 20), Color.White);
            spriteBatch.DrawString(font, year.ToString(), new Vector2(240, 20), Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, Country selectedCountry, SpriteFont font, int day, int year)
        {
            switch (selectedCountry.Name)
            {
                case "Russia": currentFlag = flags[0]; break;
                case "Australia": currentFlag = flags[1]; break;
                case "Mongolia": currentFlag = flags[2]; break;
                case "China": currentFlag = flags[3]; break;
                case "Kazakhstan": currentFlag = flags[4]; break;
            }
            spriteBatch.Draw(uiTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(dateTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(currentFlag, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.DrawString(font, day.ToString(), new Vector2(78, 20), Color.White);
            spriteBatch.DrawString(font, year.ToString(), new Vector2(240, 20), Color.White);
            spriteBatch.DrawString(font, selectedCountry.ScienceRating.ToString(), new Vector2(420, 648), Color.White);
            spriteBatch.DrawString(font, selectedCountry.HappinessRating.ToString(), new Vector2(445, 678), Color.White);
            spriteBatch.DrawString(font, selectedCountry.Population.ToString(), new Vector2(620, 678), Color.White);
            if (selectedCountry.Diseases.Count > 0 && selectedCountry.Diseases.Count < 3)
            {
                buttons[2].ButtonActive = false;
                buttons[1].ButtonActive = false;
                buttons[0].ButtonActive = true;
                buttons[0].Draw(spriteBatch);
            }
            else if (selectedCountry.Diseases.Count >= 3)
            {
                buttons[0].ButtonActive = false;
                buttons[2].ButtonActive = false;
                buttons[1].ButtonActive = true;
                buttons[1].Draw(spriteBatch);
            }
            if (selectedCountry.Enemies.Count > 0)
            {
                buttons[0].ButtonActive = false;
                buttons[1].ButtonActive = false;
                buttons[2].ButtonActive = true; 
                buttons[2].Draw(spriteBatch);
            }
        }
        public void Update()
        {
            foreach (Button button in buttons)
            {
                button.Update();
            }
        }
        public void OnButtonClick(string button)
        {
            Console.WriteLine(button + " was clicked!");
        }
    }
}
