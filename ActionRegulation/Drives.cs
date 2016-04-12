using System;
using EmotionalAppraisal;
using KnowledgeBase.WellFormedNames;
using EmotionalAppraisal.AppraisalRules;

namespace ActionRegulation
{
    class Drives
    {
        // drive values range from 1 to 10
        public int Energy { get; set; }
        public int EnergyWeight { get; set; } //importance of drive to agent (range 0 to 1)

        public int Integrity { get; set; }
        public int IntegritWeighty { get; set; }

        public int Affiliation { get; set; }
        public int AffiliationWeight { get; set; }

        public int Certainty { get; set; }
        public int CertaintyWeight { get; set; }

        public int Competence { get; set; }
        public int CompetenceWeight { get; set; }

        /* fatima occ emotions
            admiration
            anger
            gratitude
            distress 
            gratification
            joy
            pride
            reproach
            shame
        */
        public void DriveToAppraisalRule()
        {

        }

        public AppraisalRule EnergyToEmotion(int Energy)
        {
            if(Energy>0)
            {
                AppraisalRule gainEnergyAppraisalRule = new AppraisalRule((Name)"Event(action,*,Energy(gain),self)");
                gainEnergyAppraisalRule.Desirability = 5;
                gainEnergyAppraisalRule.Praiseworthiness = 5;

                return gainEnergyAppraisalRule;
            } else
            {
                AppraisalRule loseEnergyAppraisalRule = new AppraisalRule((Name)"Event(action,*,Energy(lose),self)");
                loseEnergyAppraisalRule.Desirability = -5;
                loseEnergyAppraisalRule.Praiseworthiness = -5;

                return loseEnergyAppraisalRule;
            }
            
        }
    }
}
