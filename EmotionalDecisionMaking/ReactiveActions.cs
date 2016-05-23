﻿using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	[Serializable]
	public sealed class ReactiveActions : ICustomSerialization
	{
		private float m_defaultActionCooldown = 2;
		public float DefaultActionCooldown
		{
			get { return m_defaultActionCooldown; }
			set { m_defaultActionCooldown = value < 0 ? 0 : value; }
		}

		private ActionSelector<ActionTendency> m_actionTendencies;

		public ReactiveActions()
		{
			m_actionTendencies = new ActionSelector<ActionTendency>((tendency,p, set) => !tendency.IsCoolingdown);
		}

		public void AddActionTendency(ActionTendency at)
		{
			m_actionTendencies.AddActionDefinition(at);
		}

        public void RemoveAction(Guid id)
        {
            var action = m_actionTendencies.GetActionDefinition(id);
            if (action != null)
            {
                m_actionTendencies.RemoveActionDefinition(action);
            }
        }

        public IEnumerable<IAction> SelectAction(KB kb, Name perspective)
		{
			return m_actionTendencies.SelectAction(kb, perspective).Select(p => p.Item1);
		}

		public IEnumerable<ActionTendency> GetAllActionTendencies()
		{
			return m_actionTendencies.GetAllActionDefinitions();
		}

	    public ActionTendency GetActionTendency(Guid id)
	    {
	        return m_actionTendencies.GetActionDefinition(id);
	    }

		
		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("DefaultActionCooldown", m_defaultActionCooldown);
			context.PushContext();
			context.Context = m_defaultActionCooldown;
			dataHolder.SetValue("ActionTendencies", m_actionTendencies.GetAllActionDefinitions().ToArray());
			context.PopContext();
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			DefaultActionCooldown = dataHolder.GetValue<float>("DefaultActionCooldown");
			context.PushContext();
			context.Context = m_defaultActionCooldown;
			var ats = dataHolder.GetValue<ActionTendency[]>("ActionTendencies");
			foreach (var at in ats)
				m_actionTendencies.AddActionDefinition(at);
			context.PopContext();
		}

	    
	}
}