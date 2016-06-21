﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class Action
    {
        public string Name { get; set; }
        public List<string> PreConditions { get; set; }
        public List<string> Effects { get; set; }
        public float EnergyEffect { get; set; }
        public float IntegrityEffect { get; set; }
        public float AffiliationEffect { get; set; }
        public float CertaintyEffect { get; set; }
        public float CompetenceEffect { get; set; }

        public Action(string name)
        {
            Name = name;
            PreConditions = new List<string>();
            Effects = new List<string>();
            EnergyEffect = 0;
            IntegrityEffect = 0;
            AffiliationEffect = 0;
            CertaintyEffect = 0;
            CompetenceEffect = 0;
        }

        public Action(string name, List<string> preConditions, List<string> effects)
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

        public Action(string name, List<string> preConditions, List<string> effects, float energyEffect, float integrityEffect, float affiliationEffect)
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
    }
}