using System;
using EmotionalAppraisal.DTOs;

namespace ActionRegulation
{
    class Drives
    {
        // drive values range from -10 to 10
        public float Energy { get; set; }
        public float EnergyWeight { get; set; } //importance of drive to agent (range 0 to 1)

        public float Integrity { get; set; }
        public float IntegrityWeight { get; set; }

        public float Affiliation { get; set; }
        public float AffiliationWeight { get; set; }

        public float Certainty { get; set; }
        public float CertaintyWeight { get; set; }

        public float Competence { get; set; }
        public float CompetenceWeight { get; set; }

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

            compare with 4 motives van McClelland (find papers)

            achievement(goal-directedness)->? 
            competence->power
            novelty vs. control (Too little change vs. too much change) <- achievement is motivation to change things
            achievement drive not goal specific but trying to find goals to accomplish
            high affiliation = many social achievements
            high achievement can mean lower affiiation
            avoidance -> related to integrity and certainty (unsure of outcome can be dangerous)
            classification to make the theory work, not how humans think

            make summary of thought process behind the code creation

            provide reasoning for number/value choices where possible(feasibiity) or logical
            (difference between thinking like a human and behaving like a human)

        */
        
        // create an appraisal rule based on the average drive gain/loss from a goal
        public AppraisalRuleDTO GoalToAppraisalRule(Goal goal)
        {
            AppraisalRuleDTO appraisalRule = new AppraisalRuleDTO();

            float averageEffect = (ExpectedEnergy(goal.EnergyEffect) + ExpectedIntegrity(goal.IntegrityEffect) 
                + ExpectedAffiliation(goal.AffiliationEffect)) / 3; //average drive gain/loss

            appraisalRule.Desirability = averageEffect;
            appraisalRule.Praiseworthiness = ExpectedAffiliation(goal.AffiliationEffect);

            if (averageEffect>0)
                appraisalRule.EventMatchingTemplate = "event(action,*," + goal.Name + "(gain),self)";
            else
                appraisalRule.EventMatchingTemplate = "event(action,*," + goal.Name + "(lose),self)";

            return appraisalRule;
        }

        // create an appraisal rule based on the average drive gain/loss
        public AppraisalRuleDTO DrivesToAppraisalRule(float energy, float integrity, float affiliation)
        {
            AppraisalRuleDTO appraisalRule = new AppraisalRuleDTO();

            float averageEffect = (ExpectedEnergy(energy) + ExpectedIntegrity(integrity) + ExpectedAffiliation(affiliation)) / 3; //average drive gain/loss

            appraisalRule.Desirability = averageEffect;
            appraisalRule.Praiseworthiness = averageEffect;

            if (averageEffect > 0)
                appraisalRule.EventMatchingTemplate = "event(action,*,drives(gain),self)";
            else
                appraisalRule.EventMatchingTemplate = "event(action,*,drives(lose),self)";

            return appraisalRule;
        }

        // returns appraisal rule with desirability and praiseworthiness equal to energy level
        // if energy level is 0 then null is returned as no rule needs to be added for no change
        public AppraisalRuleDTO EnergyToAppraisalRule(float energy)
        {
            AppraisalRuleDTO energyAppraisalRule = new AppraisalRuleDTO();
            
            energyAppraisalRule.EventMatchingTemplate = "event(action,*,energy(gain),self)";
            energyAppraisalRule.Desirability = ExpectedEnergy(energy);
            energyAppraisalRule.Praiseworthiness = ExpectedEnergy(energy);

            return energyAppraisalRule;
        }

        // returns appraisal rule with desirability and praiseworthiness equal to integrity level
        // if integrity level is 0 then null is returned as no rule needs to be added for no change
        public AppraisalRuleDTO IntegrityToAppraisalRule(float integrity)
        {
            AppraisalRuleDTO integrityAppraisalRule = new AppraisalRuleDTO();
            
            integrityAppraisalRule.EventMatchingTemplate = "event(action,*,integrity(gain),self)";
            integrityAppraisalRule.Desirability = ExpectedIntegrity(integrity);
            integrityAppraisalRule.Praiseworthiness = ExpectedIntegrity(integrity);
            
            return integrityAppraisalRule;
        }

        // returns appraisal rule with desirability and praiseworthiness equal to affiliation level
        // if affiliation level is 0 then null is returned as no rule needs to be added for no change
        public AppraisalRuleDTO AffiliationToAppraisalRule(float affiliation)
        {
            AppraisalRuleDTO affiliationAppraisalRule = new AppraisalRuleDTO();
            
            affiliationAppraisalRule.EventMatchingTemplate = "event(action,*,affiliation(gain),self)";
            affiliationAppraisalRule.Desirability = ExpectedAffiliation(affiliation);
            affiliationAppraisalRule.Praiseworthiness = ExpectedAffiliation(affiliation);
            
            return affiliationAppraisalRule;
        }

        public float ExpectedEnergy(float energy)
        {
            return Math.Max(-10, Math.Min(10, Energy + (energy * EnergyWeight)));
        }

        public float ExpectedIntegrity(float integrity)
        {
            return Math.Max(-10, Math.Min(10, Integrity + (integrity * IntegrityWeight)));
        }

        public float ExpectedAffiliation(float affiliation)
        {
            return Math.Max(-10, Math.Min(10, Affiliation + (affiliation * AffiliationWeight)));
        }

        public float ExpectedCertainty(float certainty)
        {
            return Math.Max(-10, Math.Min(10, Certainty + (certainty * CertaintyWeight)));
        }

        public float ExpectedCompetence(float competence)
        {
            return Math.Max(-10, Math.Min(10, Competence + (competence * CompetenceWeight)));
        }
    }
}
