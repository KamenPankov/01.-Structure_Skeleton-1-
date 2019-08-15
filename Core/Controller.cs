using SpaceStation.Core.Contracts;
using SpaceStation.Models;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Missions;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private IRepository<IAstronaut> astronuatRepository;
        private IRepository<IPlanet> planetRepository;
        private IMission mission;
        private int exploredPlanets;

        public Controller()
        {
            this.astronuatRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
            this.mission = new Mission();
            this.exploredPlanets = 0;
        }

        public string AddAstronaut(string type, string astronautName)
        {
            //if (type != nameof(Biologist) || type != nameof(Geodesist) || type != nameof(Meteorologist))
            //{
            //    throw new InvalidOperationException("Astronaut type doesn't exists!");
            //}

            IAstronaut astronautNew = null;
            string returnMessage = string.Empty;

            switch (type)
            {
                case nameof(Biologist):
                    astronautNew = new Biologist(astronautName);
                    returnMessage = $"Successfully added {type}: {astronautName}!";
                    break;

                case nameof(Geodesist):
                    astronautNew = new Geodesist(astronautName);
                    returnMessage = $"Successfully added {type}: {astronautName}!";
                    break;

                case nameof(Meteorologist):
                    astronautNew = new Meteorologist(astronautName);
                    returnMessage = $"Successfully added {type}: {astronautName}!";
                    break;

                default:
                    throw new InvalidOperationException("Astronaut type doesn't exists!");
                    
            }

            this.astronuatRepository.Add(astronautNew);

            return returnMessage;
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planetNew = new Planet(planetName, items);

            this.planetRepository.Add(planetNew);

            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> explorers = this.astronuatRepository.Models.Where(a => a.Oxygen > 60).ToList();

            if (explorers.Count == 0)
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet");
            }

            IPlanet planet = this.planetRepository.FindByName(planetName);

            this.mission.Explore(planet, explorers);

            this.exploredPlanets++;

            IEnumerable<IAstronaut> deadExplorers = explorers.Where(a => a.CanBreath == false);

            return $"Planet: {planetName} was explored! Exploration finished with {deadExplorers.Count()} dead astronauts!";

        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{this.exploredPlanets} planets were explored!");
            stringBuilder.AppendLine($"Astronauts info:");
            foreach (IAstronaut astronaut in this.astronuatRepository.Models)
            {
                stringBuilder.AppendLine($"Name: {astronaut.Name}");
                stringBuilder.AppendLine($"Oxygen: {astronaut.Oxygen}");
                stringBuilder.AppendLine($"Bag items: " +
                    $"{(astronaut.Bag.Items.Count == 0 ? "none" : string.Join(", ", astronaut.Bag.Items))}");
            }

            return stringBuilder.ToString().TrimEnd();

        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronautExists = this.astronuatRepository.FindByName(astronautName);

            if (astronautExists == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            this.astronuatRepository.Remove(astronautExists);

            return $"Astronaut {astronautName} was retired!";
        }
    }
}
