using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly IDictionary<string, IAstronaut> astronautsByName;

        public AstronautRepository()
        {
            astronautsByName = new Dictionary<string, IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => this.astronautsByName.Values.ToList();

        public void Add(IAstronaut model)
        {
            this.astronautsByName.Add(model.Name, model);
        }

        public IAstronaut FindByName(string name)
        {
            if (this.astronautsByName.ContainsKey(name))
            {
                return this.astronautsByName[name];
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IAstronaut model)
        {
            return this.astronautsByName.Remove(model.Name);
        }
    }
}
