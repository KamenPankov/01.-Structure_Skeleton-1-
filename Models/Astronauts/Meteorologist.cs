using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models
{
    public class Meteorologist : Astronaut
    {
        private const double InitialOxygen = 90;

        public Meteorologist(string name) 
            : base(name, InitialOxygen)
        {
        }
    }
}
