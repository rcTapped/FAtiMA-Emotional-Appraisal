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
        public SortedList<float, Goal> goalList { get; set; }

        public DriveSatisfaction(float energyWeight, float integrityWeight, float affiliationWeight)
        {
            drives = new Drives(energyWeight, integrityWeight, affiliationWeight);
            goalList = new SortedList<float, Goal>(new InvertedComparer());
        }

        public DriveSatisfaction(Drives drive)
        {
            drives = drive;
            goalList = new SortedList<float, Goal>(new InvertedComparer());
        }

        public void addGoal(Goal goal)
        {
            //check if goal already exists
            if (!goalList.ContainsValue(goal))
            {
                //determine goal drive satisfaction
                float benefit = drives.ExpectedEnergy(goal.EnergyEffect) + drives.ExpectedIntegrity(goal.IntegrityEffect) + drives.ExpectedAffiliation(goal.AffiliationEffect);

                // factor in low drive satisfaction urgency
                // double benefit of very low drives to simulate urgency
                // factor in medium drive satisfaction urgency
                // 1.5 times benefit of low drives to simulate urgency
                if (drives.Energy < 2)
                    benefit += drives.ExpectedEnergy(goal.EnergyEffect);
                else if (drives.Energy < 5)
                    benefit += 0.5f * drives.ExpectedEnergy(goal.EnergyEffect);

                if (drives.Integrity < 2)
                    benefit += drives.ExpectedIntegrity(goal.IntegrityEffect);
                else if (drives.Integrity < 5)
                    benefit += 0.5f * drives.ExpectedIntegrity(goal.IntegrityEffect);

                if (drives.Affiliation < 2)
                    benefit += drives.ExpectedAffiliation(goal.AffiliationEffect);
                else if (drives.Affiliation < 5)
                    benefit += 0.5f * drives.ExpectedAffiliation(goal.AffiliationEffect);

                goalList.Add(benefit, goal);
            } else
            {
                Console.WriteLine("Duplicate goal");
            }
        }
        
        public void printGoalList()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < goalList.Count; i++)
            {
                Console.WriteLine("Benefit: " + goalList.ElementAt(i).Key + " Goal: " + goalList.ElementAt(i).Value.Name);
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        //Return current goal with greatest overall benefit (aka last element in the ascending sorted list)
        public Goal chooseGoal()
        {
            return goalList.First().Value;
        }

        class InvertedComparer : IComparer<float>
        {
            public int Compare(float x, float y)
            {
                return y.CompareTo(x);
            }
        }

        public void actionPlanner(List<string> initialState, List<Action> actionList, Goal goal)
        {
            List<string> GoalState = goal.SuccessConditions;


        }
    }
}
