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
        private Texture2D map;
        private Rectangle mousePos;
        private Country selectedCountry;
        private UserInterface UI;
        const uint russia = 4294910976;
        const uint norway = 4107724238;
        const uint australia = 4280549599;
        const uint mongolia = 4279646800;
        const uint china = 4282015221;
        const uint kazakhstan = 4278205951;
        const uint india = 4284506263;
        const uint usa = 4284338075;
        private Texture2D[] t2dCountries = new Texture2D[7];
        private int iCountryToHilight = -1;
        private int gameDays;
        private int gameYears = 2000;
        private int daysInYear;
        private int dayOfWeek;
        private int worldPopulation;
        private uint sCountry;
        private string dayName = "Monday";
        private string[] diseaseNames = new string[5] { "Ebola", "Meme", "Mem", "Trem", "Dem" };
        private EventHandler EventHandler;
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
        Country Mongolia;
        Country China;
        Country Kazakhstan;
        Country India;
        Country USA;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            UI = new UserInterface();
            EventHandler = new EventHandler();
            mousePos = new Rectangle(0, 0, 25, 25);
            Australia = new Country("Australia", 190000000);
            Russia = new Country("Russia", 146000000);
            Mongolia = new Country("Mongolia", 2397000);
            China = new Country("China", 100000000);
            Kazakhstan = new Country("Kazakhstan", 1488000);
            India = new Country("India", 140000000);
            USA = new Country("USA", 282000000);
            countries.Add(Australia);
            countries.Add(Russia);
            countries.Add(Mongolia);
            countries.Add(China);
            countries.Add(Kazakhstan);
            countries.Add(India);
            countries.Add(USA);
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
            map = Content.Load<Texture2D>("map");
            t2dCountries[0] = Content.Load<Texture2D>("Countries/map_AU");
            t2dCountries[1] = Content.Load<Texture2D>("Countries/map_RU");
            t2dCountries[2] = Content.Load<Texture2D>("Countries/map_MN");
            t2dCountries[3] = Content.Load<Texture2D>("Countries/map_CN");
            t2dCountries[4] = Content.Load<Texture2D>("Countries/map_KZ");
            t2dCountries[5] = Content.Load<Texture2D>("Countries/map_IN");
            t2dCountries[6] = Content.Load<Texture2D>("Countries/map_US");
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
                map.GetData(0, new Rectangle(MPos.X, MPos.Y, 1, 1), colourValue, 0, 1);
                sCountry = colourValue[0];
            }
            switch (sCountry)
            {
                case australia: selectedCountry = Australia; iCountryToHilight = 0; break;
                case russia: selectedCountry = Russia; iCountryToHilight = 1; break;
                case mongolia: selectedCountry = Mongolia; iCountryToHilight = 2; break;
                case china: selectedCountry = China; iCountryToHilight = 3; break;
                case kazakhstan: selectedCountry = Kazakhstan; iCountryToHilight = 4; break;
                case india: selectedCountry = India; iCountryToHilight = 5; break;
                case usa: selectedCountry = USA; iCountryToHilight = 6; break;
            }
            Console.WriteLine(sCountry);
            Days = (int)gameTime.TotalGameTime.TotalSeconds;
            UI.Update();

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
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void nextDay()
        {
            dayOfWeek++;
            if (Days == 10)
            {
                Russia.startResearch(new ScienceEvent("The Manhattan Project", "Leads to new nuclear discoveries.", 10));
                Russia.startResearch(new ScienceEvent("The Manhattan Project", "Leads to new nuclear discoveries.", 10));
                Russia.startResearch(new ScienceEvent("The Manhattan Project", "Leads to new nuclear discoveries.", 10));
                Russia.startResearch(new ScienceEvent("The Manhattan Project", "Leads to new nuclear discoveries.", 10));
                Russia.startResearch(new ScienceEvent("The Manhattan Project", "Leads to new nuclear discoveries.", 10));
                Russia.giveDisease(new Disease("Ebola", 6, 10));
                Russia.giveDisease(new Disease("Ebola", 6, 10));
                Russia.giveDisease(new Disease("Ebola", 6, 10));
                Russia.giveDisease(new Disease("Ebola", 6, 10));
                Russia.giveDisease(new Disease("Ebola", 6, 10));
            }
            if (dayOfWeek == 7)
                dayOfWeek = 0;
            dayName = days[dayOfWeek];
            daysInYear++;
            if (daysInYear >= 366)
            {
                daysInYear = 1;
                gameYears++;
            }
            worldPopulation = 0;
            foreach (Country country in countries)
            {
                country.nextDay();
                worldPopulation += country.Population;
            }
            Console.WriteLine("A new day has dawned... (Day: " + Days + ")");
            Console.WriteLine("The world's population is now " + worldPopulation);
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
