using Microsoft.Xna.Framework;
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
        private string selectedCountry;
        const uint russia = 4282228480;
        const uint australia = 4280549599;
        const uint usa = 4278240244;
        private Texture2D[] t2dCountries = new Texture2D[1];
        private int iCountryToHilight = -1;
        private int gameDays;
        private int gameYears;
        private int dayOfWeek;
        private uint sCountry;
        private string dayName = "Monday";
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
        Country uk;
        Country au;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            mousePos = new Rectangle(0, 0, 25, 25);
            uk = new Country("United Kingdom");
            au = new Country("Australia");
            countries.Add(uk);
            countries.Add(au);
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
            t2dCountries[0] = Content.Load<Texture2D>("map_AU");
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

            mousePos.X = MPos.X;
            mousePos.Y = MPos.Y;
            uint[] colourValue = new uint[1];
            if (MPos.LeftButton == ButtonState.Pressed && MPos.X >= 0 && MPos.X < map.Width && MPos.Y >= 0 && MPos.Y < map.Height)
            {
                map.GetData(0, new Rectangle(MPos.X, MPos.Y, 1, 1), colourValue, 0, 1);
                sCountry = colourValue[0];
            }
            
            switch (sCountry)
            {
                case australia: selectedCountry = "Australia"; iCountryToHilight = 0; break;
                case russia: selectedCountry = "Russia"; iCountryToHilight = -1; break;
                case usa: selectedCountry = "USA"; iCountryToHilight = -1; break;
            }

            Console.WriteLine(selectedCountry + " was clicked!");

            Days = gameTime.TotalGameTime.Seconds;
            gameYears = gameDays / 365;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Day " + Days + ": " + dayName, new Vector2(640 - (int)font.MeasureString("Day 999:").X, 360), Color.Black);
            spriteBatch.DrawString(font, "Year " + Years, new Vector2(640 - (int)font.MeasureString("Day 999:").X, 380), Color.Black);
            spriteBatch.Draw(map, new Rectangle(0, 0, 1280, 720), Color.White);
            if (iCountryToHilight != -1)
            {
                spriteBatch.Draw(t2dCountries[iCountryToHilight], new Rectangle(0, 0, 1280, 720), Color.White);
            }
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
