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
        public List<Goal> goalList { get; set; }

        public DriveSatisfaction(float energyWeight, float integrityWeight, float affiliationWeight)
        {
            drives = new Drives(energyWeight, integrityWeight, affiliationWeight);
            goalList = new List<Goal>();
        }

        public DriveSatisfaction(Drives drive)
        {
            drives = drive;
            goalList = new List<Goal>();
        }

        public void addGoal(Goal newGoal)
        {
            goalList.Add(newGoal);
        }

        public void removeGoal(Goal removeGoal)
        {
            goalList.Remove(removeGoal);
        }

        public void ChooseGoal(WorldState state)
        {
            Goal bestGoal = new Goal("temp");
            float bestBenefit = float.MinValue;
            List<Goal> potentialGoals = new List<Goal>();

            //filter goals whose pre conditions are not met
            foreach(Goal goal in goalList)
            {
                if(goal.PreConditionsMet(state))
                {
                    potentialGoals.Add(goal);

                    //determine goal with greatest benefit
                    float benefit = drives.ExpectedEnergy(goal.EnergyEffect) + drives.ExpectedIntegrity(goal.IntegrityEffect) + drives.ExpectedAffiliation(goal.AffiliationEffect);

                    if (benefit > bestBenefit)
                    {
                        bestGoal = goal;
                        bestBenefit = benefit;
                    }
                }
            }

            //execute goal and process effects of goal on drives
            drives.Energy = drives.ExpectedEnergy(bestGoal.EnergyEffect);
            drives.Integrity = drives.ExpectedIntegrity(bestGoal.IntegrityEffect);
            drives.Affiliation = drives.ExpectedAffiliation(bestGoal.AffiliationEffect);
        }
    }
}
