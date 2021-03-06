﻿using System;
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
        private Country selectedCountry;
        private Texture2D[] flags = new Texture2D[9];
        private Button[] buttons = new Button[4];
        private SciencePanel sciencePanel;
        private DiseasePanel diseasePanel;
        private WarPanel warPanel;
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
            flags[5] = game.Content.Load<Texture2D>("UI/Flags/flag_IN");
            flags[6] = game.Content.Load<Texture2D>("UI/Flags/flag_US");
            flags[7] = game.Content.Load<Texture2D>("UI/Flags/flag_CAN");
            flags[8] = game.Content.Load<Texture2D>("UI/Flags/flag_GL");
            buttons[0] = new Button(new Rectangle(1043, 645, 35, 35), "UI/Icons/infectionAlert_amber", "Disease Icon");
            buttons[1] = new Button(new Rectangle(1043, 685, 35, 35), "UI/Icons/warAlert_red", "Relations Icon");
            buttons[2] = new Button(new Rectangle(330, 685, 20, 20), "UI/Icons/icon_happiness", "Happiness Icon");
            buttons[3] = new Button(new Rectangle(330, 655, 20, 20), "UI/Icons/icon_science", "Science Icon");
            buttons[0].ButtonActive = true;
            buttons[1].ButtonActive = true;
            buttons[2].ButtonActive = true;
            buttons[3].ButtonActive = true;
            sciencePanel = new SciencePanel("UI/Panels/sciencePanel", new Rectangle(20, 120, 300, 500));
            sciencePanel.LoadContent(game);
            sciencePanel.IsEnabled = false;
            diseasePanel = new DiseasePanel("UI/Panels/diseasePanel", new Rectangle(20, 120, 300, 500));
            diseasePanel.LoadContent(game);
            diseasePanel.IsEnabled = false;
            warPanel = new WarPanel("UI/Panels/warPanel", new Rectangle(20, 120, 300, 500));
            warPanel.LoadContent(game);
            warPanel.IsEnabled = false;
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
            if (sciencePanel.IsEnabled)
                sciencePanel.IsEnabled = false;
            if (diseasePanel.IsEnabled)
                diseasePanel.IsEnabled = false;
            if (warPanel.IsEnabled)
                warPanel.IsEnabled = false;
        }
        public void Draw(SpriteBatch spriteBatch, Country selectedCountry, SpriteFont font, int day, int year)
        {
            this.selectedCountry = selectedCountry;
            switch (this.selectedCountry.Name)
            {
                case "Russia": currentFlag = flags[0]; break;
                case "Australia": currentFlag = flags[1]; break;
                case "Mongolia": currentFlag = flags[2]; break;
                case "China": currentFlag = flags[3]; break;
                case "Kazakhstan": currentFlag = flags[4]; break;
                case "India": currentFlag = flags[5]; break;
                case "USA": currentFlag = flags[6]; break;
                case "Canada": currentFlag = flags[7]; break;
                case "Greenland": currentFlag = flags[8]; break;
            }
            spriteBatch.Draw(uiTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(dateTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(currentFlag, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.DrawString(font, day.ToString(), new Vector2(78, 20), Color.White);
            spriteBatch.DrawString(font, year.ToString(), new Vector2(240, 20), Color.White);
            buttons[0].Draw(spriteBatch);
            buttons[1].Draw(spriteBatch);
            buttons[2].Draw(spriteBatch);
            buttons[3].Draw(spriteBatch);
            spriteBatch.DrawString(font, selectedCountry.ScienceRating.ToString(), new Vector2(420, 648), Color.White);
            spriteBatch.DrawString(font, selectedCountry.HappinessRating.ToString(), new Vector2(445, 678), Color.White);
            spriteBatch.DrawString(font, selectedCountry.Population.ToString(), new Vector2(620, 678), Color.White);
            if (sciencePanel.IsEnabled)
                sciencePanel.Draw(spriteBatch, font);
            if (diseasePanel.IsEnabled)
                diseasePanel.Draw(spriteBatch, font);
            if (warPanel.IsEnabled)
                warPanel.Draw(spriteBatch, font);
        }
        public void Update()
        {
            sciencePanel.selectedCountry = selectedCountry;
            diseasePanel.selectedCountry = selectedCountry;
            warPanel.selectedCountry = selectedCountry;
            foreach (Button button in buttons)
            {
                button.Update();
            }
        }
        public void OnButtonClick(string button)
        {
            if (button == "Science Icon")
            {
                if (diseasePanel.IsEnabled)
                    diseasePanel.TogglePanel();  
                else if (warPanel.IsEnabled)
                    warPanel.TogglePanel();
                sciencePanel.TogglePanel();
            }
            else if (button == "Disease Icon")
            {
                if (sciencePanel.IsEnabled)
                    sciencePanel.TogglePanel();
                else if (warPanel.IsEnabled)
                    warPanel.TogglePanel();
                diseasePanel.TogglePanel();
            }
            else if (button == "Relations Icon")
            {
                if (sciencePanel.IsEnabled)
                    sciencePanel.TogglePanel();
                else if (diseasePanel.IsEnabled)
                    diseasePanel.TogglePanel();
                warPanel.TogglePanel();
            }
        }
    }
}
