using SpaceStation.Models.Planets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models
{
    
    public class Planet : IPlanet
    {
        private string name;
        private readonly IList<string> items;

        public Planet(string name, params string[] planetItems)
        {
            this.Name = name;
            items = new List<string>(planetItems);
        }

        public ICollection<string> Items => this.items;

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Invalid name!");
                }

                this.name = value;
            }
        }
    }
}
