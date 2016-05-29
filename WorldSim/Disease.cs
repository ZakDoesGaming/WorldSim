using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldSim
{
    class Disease
    {
        private int _deathFactor; // The death factor is the maximum number of people that are able to be killed by this disease each day.
        private string _name;
        private int _daysToExtermination;
        private int _daysAlive;
        private int _peopleKilled;
        public int deathFactor
        {
            get
            {
                return _deathFactor;
            }
            set
            {
                _deathFactor = value;
            }
        }
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int daysToExtermination
        {
            get
            {
                return _daysToExtermination;
            }
            set
            {
                _daysToExtermination = value;
            }
        }
        public int daysAlive
        {
            get
            {
                return _daysAlive;
            }
            set
            {
                _daysAlive = value;
            }
        }
        public int peopleKilled
        {
            get
            {
                return _peopleKilled;
            }
            set
            {
                _peopleKilled = value;
            }
        }

        public Disease(string name, int deathFactor)
        {
            this.name = name;
            this.deathFactor = deathFactor;
        }
        public Disease(string name, int deathFactor, int daysToExtermination)
        {
            this.name = name;
            this.deathFactor = deathFactor;
            this.daysToExtermination = daysToExtermination;
        }
    }
}
