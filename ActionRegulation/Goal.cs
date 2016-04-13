using System.Collections.Generic;

namespace ActionRegulation
{
    class Goal
    {
        public string Name { get; set; }
        public List<string> PreConditions { get; set; }
        public List<string> SuccessConditions { get; set; }
        public List<string> FailureConditions { get; set; }
        public float EnergyEffect { get; set; }
        public float IntegrityEffect { get; set; }
        public float AffiliationEffect { get; set; }
        public float CertaintyEffect { get; set; }
        public float CompetenceEffect { get; set; }
        
        public Goal(string name)
        {
            Name = name;
            PreConditions = new List<string>();
            SuccessConditions = new List<string>();
            FailureConditions = new List<string>();
            EnergyEffect = 0;
            IntegrityEffect = 0;
            AffiliationEffect = 0;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Goal(string name, float energyEffect, float integrityEffect, float affiliationEffect)
        {
            Name = name;
            PreConditions = new List<string>();
            SuccessConditions = new List<string>();
            FailureConditions = new List<string>();
            EnergyEffect = energyEffect;
            IntegrityEffect = integrityEffect;
            AffiliationEffect = affiliationEffect;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Goal(string name, List<string> preConditions, List<string> successConditions, List<string> failureConditions, float energyEffect, float integrityEffect, float affiliationEffect, float certaintyEffect, float competenceEffect)
        {
            Name = name;
            PreConditions = preConditions;
            SuccessConditions = successConditions;
            FailureConditions = failureConditions;
            EnergyEffect = energyEffect;
            IntegrityEffect = integrityEffect;
            AffiliationEffect = affiliationEffect;
            CertaintyEffect = certaintyEffect;
            CompetenceEffect = competenceEffect;
        }
    }
}
