﻿using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Interfaces;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		private abstract class BaseEvent: IBaseEvent, IPerspective<BaseEvent>, ICustomSerialization
		{
			private HashSet<string> m_linkedEmotions = new HashSet<string>();
			
			public uint Id { get; protected set; }

			public IEnumerable<string> LinkedEmotions => m_linkedEmotions;

			public Name Type { get; private set; }

		    public Name Subject { get; private set; }

			public ulong Timestamp { get; private set; }

            public Name EventName { get; private set; }

            protected BaseEvent(uint id, Name eventName, ulong timestamp)
			{
				Id = id;
	            Type = eventName.GetNTerm(1);
	            Subject = eventName.GetNTerm(2);
				Timestamp = timestamp;
				EventName = eventName;
			}

			public void LinkEmotion(string emotionType)
			{
				m_linkedEmotions.Add(emotionType);
			}

			public abstract EventDTO ToDTO();

			protected abstract Name BuildEventName();

			public BaseEvent ApplyPerspective(Name name)
			{
				return SwapPerspective(name, Name.SELF_SYMBOL);
			}

			public BaseEvent RemovePerspective(Name name)
			{
				return SwapPerspective(Name.SELF_SYMBOL,name);
			}

			public virtual BaseEvent SwapPerspective(Name oldPerspective, Name newPerspective)
			{
				EventName = EventName.SwapPerspective(oldPerspective, newPerspective);
				Subject = Subject.SwapPerspective(oldPerspective, newPerspective);
				return this;
			}

			public virtual void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				dataHolder.SetValue("Id", Id);
				dataHolder.SetValue("Type", Type);
				dataHolder.SetValue("Subject", Subject);
				dataHolder.SetValue("Timestamp", Timestamp);
				if (m_linkedEmotions.Count > 0)
				{
					dataHolder.SetValue("LinkedEmotions", m_linkedEmotions.ToArray());
				}
			}

			public virtual void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				Id = dataHolder.GetValue<uint>("Id");
				Type = dataHolder.GetValue<Name>("Type");
				Subject = dataHolder.GetValue<Name>("Subject");
				Timestamp = dataHolder.GetValue<ulong>("Timestamp");

				if (m_linkedEmotions == null)
					m_linkedEmotions = new HashSet<string>();
				else
					m_linkedEmotions.Clear();
				var le = dataHolder.GetValue<string[]>("LinkedEmotions");
				if (le != null && le.Length > 0)
					m_linkedEmotions.UnionWith(le);

				EventName = BuildEventName();
			}
		}
	}
}