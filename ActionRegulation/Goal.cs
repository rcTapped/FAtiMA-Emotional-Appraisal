using System.Collections.Generic;

namespace ActionRegulation
{
    class Goal
    {
        public string Name { get; set; }
        public List<Literal> SuccessConditions { get; set; }
        public List<Literal> FailureConditions { get; set; }
        public float EnergyEffect { get; set; }
        public float IntegrityEffect { get; set; }
        public float AffiliationEffect { get; set; }
        public float CertaintyEffect { get; set; }
        public float CompetenceEffect { get; set; }
        
        public Goal(string name)
        {
            Name = name;
            SuccessConditions = new List<Literal>();
            FailureConditions = new List<Literal>();
            EnergyEffect = 0;
            IntegrityEffect = 0;
            AffiliationEffect = 0;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Goal(string name, List<Literal> successConditions)
        {
            Name = name;
            SuccessConditions = successConditions;
            FailureConditions = new List<Literal>();
            EnergyEffect = 0;
            IntegrityEffect = 0;
            AffiliationEffect = 0;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Goal(string name, List<Literal> successConditions, List<Literal> failureConditions, float energyEffect, float integrityEffect, float affiliationEffect)
        {
            Name = name;
            SuccessConditions = successConditions;
            FailureConditions = failureConditions;
            EnergyEffect = energyEffect;
            IntegrityEffect = integrityEffect;
            AffiliationEffect = affiliationEffect;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Goal(string name, List<Literal> successConditions, List<Literal> failureConditions, float energyEffect, float integrityEffect, float affiliationEffect, float certaintyEffect, float competenceEffect)
        {
            Name = name;
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
