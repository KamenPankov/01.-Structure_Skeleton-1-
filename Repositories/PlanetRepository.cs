using SpaceStation.Models.Planets;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly IDictionary<string, IPlanet> planetsByName;

        public PlanetRepository()
        {
            this.planetsByName = new Dictionary<string, IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.planetsByName.Values.ToList();

        public void Add(IPlanet model)
        {
            this.planetsByName.Add(model.Name, model);
        }

        public IPlanet FindByName(string name)
        {
            if (this.planetsByName.ContainsKey(name))
            {
                return this.planetsByName[name];
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IPlanet model)
        {
            return this.planetsByName.Remove(model.Name);
        }
    }
}
