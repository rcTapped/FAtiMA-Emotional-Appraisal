using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class DriveSatisfaction
    {
        public Drives drives { get; set; }

        public DriveSatisfaction(float energyWeight, float integrityWeight, float affiliationWeight)
        {
            drives = new Drives();

            drives.Energy = 0;
            drives.EnergyWeight = energyWeight;
            drives.Integrity = 0;
            drives.IntegrityWeight = integrityWeight;
            drives.Affiliation = 0;
            drives.AffiliationWeight = affiliationWeight;
        }

        public void ChooseGoal()
        {

        }
    }
}
