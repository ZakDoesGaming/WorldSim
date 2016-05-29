﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace WorldSim
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Country> countries = new List<Country>();
        private string[] days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private SpriteFont font;
        private Texture2D cursor;
        private Texture2D map;
        private Rectangle mousePos;
        private Country selectedCountry;
        private UserInterface UI;
        private bool selected;
        const uint russia = 4294910976;
        const uint norway = 4107724238;
        const uint australia = 4280549599;
        const uint usa = 4278240244;
        private Texture2D[] t2dCountries = new Texture2D[2];
        private int iCountryToHilight = -1;
        private int gameDays;
        private int gameYears;
        private int daysInYear = 360;
        private int dayOfWeek;
        private uint sCountry;
        private string dayName = "Monday";
        private string[] diseaseNames = new string[5] { "Ebola", "Meme", "Mem", "Trem", "Dem" };
        public int Days
        {
            get
            {
                return gameDays;
            }
            set
            {
                if (value == gameDays) return;
                gameDays = value;
                nextDay();
            }
        }
        public int Years
        {
            get
            {
                return gameYears;
            }
            set
            {
                if (value == gameYears) return;
                gameYears = value;
                nextYear();
            }
        }
        private Random rand;
        Country Australia;
        Country Russia;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 30.0);
            UI = new UserInterface();
            mousePos = new Rectangle(0, 0, 25, 25);
            Australia = new Country("Australia");
            Russia = new Country("Russia");
            countries.Add(Australia);
            countries.Add(Russia);
            rand = new Random();
        }

        protected override void Initialize()
        {
      
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("MainFont");
            cursor = Content.Load<Texture2D>("cursor");
            map = Content.Load<Texture2D>("map");
            t2dCountries[0] = Content.Load<Texture2D>("Countries/map_AU");
            t2dCountries[1] = Content.Load<Texture2D>("Countries/map_RU");
            UI.LoadContent(this);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var MPos = Mouse.GetState();
            var KBS = Keyboard.GetState();

            mousePos.X = MPos.X;
            mousePos.Y = MPos.Y;
            uint[] colourValue = new uint[1];
            if (MPos.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("Clicked!");
                map.GetData(0, new Rectangle(MPos.X, MPos.Y, 1, 1), colourValue, 0, 1);
                sCountry = colourValue[0];
            }

            switch (sCountry)
            {
                case australia: selected = true; selectedCountry = Australia; iCountryToHilight = 0; break;
                case russia: selected = true; selectedCountry = Russia; iCountryToHilight = 1; break;
                default: selected = false; break;
            }
            Days = (int)gameTime.TotalGameTime.TotalSeconds * 2;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            //spriteBatch.Draw(sea, Vector2.Zero, new Rectangle(0, 0, 1280, 720), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(map, new Rectangle(0, 0, 1280, 720), Color.White);
            if (iCountryToHilight != -1)
            {
                spriteBatch.Draw(t2dCountries[iCountryToHilight], new Rectangle(0, 0, 1280, 720), Color.White);
                UI.Draw(spriteBatch, selectedCountry, font, daysInYear, gameYears);
            }
            else
                UI.Draw(spriteBatch, font, daysInYear, gameYears);
            spriteBatch.Draw(cursor, mousePos, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void nextDay()
        {
            dayOfWeek++;
            if (dayOfWeek == 7)
                dayOfWeek = 0;
            dayName = days[dayOfWeek];
            daysInYear++;
            if (daysInYear >= 366)
            {
                daysInYear = 1;
                gameYears++;
            }
            foreach (Country country in countries)
            {
                country.nextDay();
            }
            Console.WriteLine("A new day has dawned... (Day: " + Days + ")");
        }

        void nextYear()
        {
            Console.WriteLine("YEAR " + Years + " HAS ARRIVED!");
            Console.WriteLine("All countries receive 5 happiness points!");
            foreach (Country country in countries)
            {
                country.HappinessRating += 5;
            }
        }
    }
}
