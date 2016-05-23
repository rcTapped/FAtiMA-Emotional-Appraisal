﻿using System;
using EmotionalAppraisal.DTOs;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal.AppraisalRules
{
	/// <summary>
	///     Represents an Emotional AppraisalRule based in Construal Frames that specify values
	///     for some of OCC's appraisal variables: Desirability, DesirabilityForOther, Like and
	///     Praiseworthiness.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	public class AppraisalRule : BaseDomainObject
	{
        public Name EventName { get; set; }
		public ConditionSet Conditions { get; set; }

		public AppraisalRule(Name eventName, ConditionSet conditions = null)
		{
			EventName = eventName;
			Conditions = conditions ?? new ConditionSet();
			Desirability = Praiseworthiness = 0;
		}

	    public AppraisalRule(AppraisalRuleDTO appraisalRuleDTO)
	    {
	        EventName = Name.BuildName(appraisalRuleDTO.EventMatchingTemplate);
	        Desirability = appraisalRuleDTO.Desirability;
	        Praiseworthiness = appraisalRuleDTO.Praiseworthiness;
			Conditions = appraisalRuleDTO.Conditions==null ? new ConditionSet() : new ConditionSet(appraisalRuleDTO.Conditions);
	    }

		/// <summary>
		///     Clone Constructor
		/// </summary>
		/// <param name="other">the reaction to clone</param>
		public AppraisalRule(AppraisalRule other) : base(other.Id)
		{
			EventName = other.EventName;
			Conditions = new ConditionSet(other.Conditions);
			Desirability = other.Desirability;
			Praiseworthiness = other.Praiseworthiness;
			//DesirabilityForOther = other.DesirabilityForOther;
			//Like = other.Like;
			//ReferencedEventName = (Name)other.ReferencedEventName.Clone();
			//if (Other != null)
			//	Other = (Name) other.Other.Clone();
		}

		/// <summary>
		///     Desirability of the event
		/// </summary>
		public float Desirability { get; set; }

	    /// <summary>
		///     Praiseworthiness of the event
		/// </summary>
		public float Praiseworthiness { get; set; }

	}
}