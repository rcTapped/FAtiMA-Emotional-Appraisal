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
        public SortedList<float, Goal> potentialGoals { get; set; }

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

        public void PotentialGoalList(WorldState state)
        {
            potentialGoals = new SortedList<float, Goal>();

            //choose good goals to satisfy drives
            foreach (Goal goal in goalList)
            {
                //determine goal drive satisfaction
                float benefit = drives.ExpectedEnergy(goal.EnergyEffect) + drives.ExpectedIntegrity(goal.IntegrityEffect) + drives.ExpectedAffiliation(goal.AffiliationEffect);

                // factor in low drive satisfaction urgency
                // double benefit of very low drives to simulate urgency
                if (drives.Energy < 2)
                    benefit += drives.ExpectedEnergy(goal.EnergyEffect);
                if (drives.Integrity < 2)
                    benefit += drives.ExpectedIntegrity(goal.IntegrityEffect);
                if (drives.Affiliation < 2)
                    benefit += drives.ExpectedAffiliation(goal.AffiliationEffect);

                // factor in medium drive satisfaction urgency
                // double benefit of very low drives to simulate urgency
                if (drives.Energy < 5)
                    benefit += 0.5f * drives.ExpectedEnergy(goal.EnergyEffect);
                if (drives.Integrity < 5)
                    benefit += 0.5f * drives.ExpectedIntegrity(goal.IntegrityEffect);
                if (drives.Affiliation < 5)
                    benefit += 0.5f * drives.ExpectedAffiliation(goal.AffiliationEffect);
                
                potentialGoals.Add(benefit, goal);
            }
        }

        public void planGoal()
        {

        }
    }
}
