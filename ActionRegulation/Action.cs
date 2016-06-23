using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class Action
    {
        public string Name { get; set; }
        public List<Literal> PreConditions { get; set; }
        public List<Literal> Effects { get; set; }
        public float EnergyEffect { get; set; }
        public float IntegrityEffect { get; set; }
        public float AffiliationEffect { get; set; }
        public float CertaintyEffect { get; set; }
        public float CompetenceEffect { get; set; }

        public Action(string name)
        {
            Name = name;
            PreConditions = new List<Literal>();
            Effects = new List<Literal>();
            EnergyEffect = 0;
            IntegrityEffect = 0;
            AffiliationEffect = 0;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Action(string name, List<Literal> preConditions, List<Literal> effects)
        {
            Name = name;
            PreConditions = preConditions;
            Effects = effects;
            EnergyEffect = 0;
            IntegrityEffect = 0;
            AffiliationEffect = 0;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Action(string name, List<Literal> preConditions, List<Literal> effects, float energyEffect, float integrityEffect, float affiliationEffect)
        {
            Name = name;
            PreConditions = preConditions;
            Effects = effects;
            EnergyEffect = energyEffect;
            IntegrityEffect = integrityEffect;
            AffiliationEffect = affiliationEffect;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        //actions are equal if they have the same name, preconditions, and effects
        public override bool Equals(object obj)
        {
            Action action = obj as Action;

            if (action == null)
                return false;
            return this.Name.Equals(action.Name) && 
                this.PreConditions.Equals(action.PreConditions) &&
                this.Effects.Equals(action.Effects);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
