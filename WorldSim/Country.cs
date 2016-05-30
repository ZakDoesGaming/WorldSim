using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldSim
{
    class Country
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public int DaysAlive { get; set; }
        public int HappinessRating { get; set; }
        public int ScienceRating { get; set; }
        public int Wealth { get; set; }

        public Color highlightColour = Color.Yellow;
        public List<Country> Enemies
        {
            get
            {
                return enemies;
            }
            set
            {
                enemies = value;
            }
        }
        public List<Country> Allies
        {
            get
            {
                return allies;
            }
            set
            {
                allies = value;
            }
        }
        public List<Disease> Diseases
        {
            get
            {
                return diseases;
            }
            set
            {
                diseases = value;
            }
        }
        public List<ScienceEvent> ScientificResearch {
            get
            {
                return scientificResearch;
            }
            set
            {
                scientificResearch = value;
            }
        }
        private List<Disease> diseases = new List<Disease>();
        private List<Country> allies = new List<Country>();
        private List<Country> enemies = new List<Country>();
        private List<ScienceEvent> scientificResearch = new List<ScienceEvent>();
        public Country(string countryName)
        {
            this.Name = countryName;
            this.Population = 2;
            this.Allies.RemoveRange(0, Allies.Count);
            this.Enemies.RemoveRange(0, Enemies.Count);
            this.Diseases.RemoveRange(0, Diseases.Count);
        }
        public Country(string countryName, int countryPopulation)
        {
            this.Name = countryName;
            this.Population = countryPopulation;
        }
        public Country(string countryname, int countryPopulation, int science, int happiness, int money)
        {
            this.Name = countryname;
            this.Population = countryPopulation;
            this.ScienceRating = science;
            this.HappinessRating = happiness;
            this.Wealth = money;
        }
        static Random rand = new Random();

        public void nextDay()
        {
            this.DaysAlive++;
            this.Population += rand.Next(1000, 2000) + HappinessRating * ScienceRating;
            updateDiseases();
            updateResearch();
        }

        public void giveDisease(Disease disease)
        {
            this.Diseases.Add(disease);
            Console.WriteLine(this.Name + " is infected with " + disease.name + "!");
        }

        public void startResearch(ScienceEvent research)
        {
            this.ScientificResearch.Add(research);
            research.DaysResearched = 0;
            Console.WriteLine(this.Name + " has started research on " + research.Name);
        }

        public void updateResearch()
        {
            if (scientificResearch.Count > 0)
            {
                foreach (ScienceEvent research in scientificResearch)
                {
                    if (research.DaysResearched == research.CompletionTime)
                    {
                        this.ScientificResearch.Remove(research);
                        Console.WriteLine(this.Name + " has finished researching " + research.Name);
                        this.ScienceRating++;
                    }
                    else
                    {
                        research.DaysResearched++;
                    }
                }
            }
        }

        public void updateDiseases()
        {
            if (diseases.Count > 0)
            {
                foreach (Disease disease in diseases.ToArray())
                {
                    if (disease.daysAlive == disease.daysToExtermination)
                    {
                        diseases.Remove(disease);
                        Console.WriteLine(disease.name + " has been eradicated from " + this.Name + "!");
                        Console.WriteLine(disease.peopleKilled + " people were killed by " + disease.name);
                    }
                    else
                    {
                        disease.daysAlive++;
                        int peopleDead = rand.Next(0, disease.deathFactor);
                        this.Population -= peopleDead;
                        disease.peopleKilled += peopleDead;
                        if (peopleDead > 100)
                            this.HappinessRating--;
                        else if (peopleDead > 200)
                            this.HappinessRating -= 5;
                        else if (peopleDead > 500)
                            this.HappinessRating -= 10;
                        else if (peopleDead > 1000)
                            this.HappinessRating -= 20;
                    }
                }
            }
        }

        public void makeAlly(Country country)
        {
            if (enemies.Contains(country))
            {
                enemies.Remove(country);
                country.Enemies.Remove(this);
                allies.Add(country);
                country.Allies.Add(this);
                country.HappinessRating++;
                this.HappinessRating++;
                Console.WriteLine(this.Name + " is now allies with " + country.Name + " after long war with each other!");
                Console.WriteLine("Both countries receive 1 happiness point!");
            }
            else if (allies.Contains(country))
            {
                Console.WriteLine(this.Name + " is already allies with " + country.Name);
            }
            else
            {
                allies.Add(country);
                country.Allies.Add(this);
                Console.WriteLine(this.Name + " is now allies with " + country.Name + "!");
                Console.WriteLine("Both countries receive 1 happiness point!");
            }
        }

        public void startWar(Country country)
        {
            if (allies.Contains(country))
            {
                allies.Remove(country);
                country.Allies.Remove(this);
                enemies.Add(country);
                country.Enemies.Add(this);
                country.HappinessRating--;
                this.HappinessRating--;
                Console.WriteLine(this.Name + " is now at war with " + country.Name + " after a long friendship with each other!");
                Console.WriteLine("Both countries lose 1 happiness point!");
            }
            else if (enemies.Contains(country))
            {
                Console.WriteLine(this.Name + " is already at war with " + country.Name);
            }
            else
            {
                Console.WriteLine(this.Name + " is now at war with " + country.Name + "!");
                enemies.Add(country);
                country.Enemies.Add(this);
            }
        }

        void Update()
        {
      
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, int x, int y)
        {
            
        }

    }
}
