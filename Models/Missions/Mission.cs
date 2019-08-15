using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpaceStation.Models.Missions
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            while (true)
            {
                IAstronaut astronautCurrent = astronauts.FirstOrDefault(a => a.CanBreath);
                if (astronautCurrent == null)
                {
                    break;
                }

                string item = planet.Items.FirstOrDefault();
                if (item == null)
                {
                    break;
                }

                astronautCurrent.Bag.Items.Add(item);
                astronautCurrent.Breath();
                planet.Items.Remove(item);

                //if (!astronautCurrent.CanBreath)
                //{
                //    break;
                //}


            }
        }
    }
}
