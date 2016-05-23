﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace EmotionalAppraisal
{
	public partial class EmotionalAppraisalAsset
	{
		/// <summary>
		/// Actual implementation of the agent's emotional state.
		/// This ensures that only one instace of the emotional state exists per agent,
		/// because only the agent's correspondent EmotionalModule may create it.
		/// </summary>
		/// @author: Pedro Gonçalves
		/// @author: João Dias
		[Serializable]
		private class ConcreteEmotionalState : IEmotionalState, ICustomSerialization
		{
			private EmotionalAppraisalAsset m_parent = null;
			private Dictionary<string, ActiveEmotion> emotionPool;
            private EmotionDisposition m_defaultEmotionalDisposition;
			private Dictionary<string, EmotionDisposition> emotionDispositions;
			private Mood mood;

			private ConcreteEmotionalState()
			{
                this.m_defaultEmotionalDisposition = new EmotionDisposition("*", 1, 1);
				this.emotionPool = new Dictionary<string, ActiveEmotion>();
				this.emotionDispositions = new Dictionary<string, EmotionDisposition>();
                this.mood = new Mood();
            }

			public ConcreteEmotionalState(EmotionalAppraisalAsset parent) : this()
			{
				m_parent = parent;
            }

			private float DeterminePotential(IEmotion emotion)
			{
				float potetial = emotion.Potential;
				float scale = (float)emotion.Valence;

				potetial += scale*(this.mood.MoodValue* m_parent.MoodInfluenceOnEmotionFactor);
				return potetial < 0 ? 0 : potetial;
			}

            /// <summary>
            /// unique hash string calculation function
            /// </summary>
            /// <param name="emotion"></param>
            private static string calculateHashString(IEmotion emotion, AM am)
            {
                StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
                try
                {
                    builder.Append(emotion.GetCause(am).EventName.ToString().ToUpper());
                    using (var it = emotion.AppraisalVariables.GetEnumerator())
                    {
                        while (it.MoveNext())
                        {
                            builder.Append("-");
                            builder.Append(it.Current);
                        }
                    }

                    return builder.ToString();
                }
                finally
                {
                    builder.Length = 0;
                    ObjectPool<StringBuilder>.Recycle(builder);
                }
            }

            
            public EmotionDTO AddActiveEmotion(EmotionDTO emotion)
		    {
                EmotionDisposition disposition = GetEmotionDisposition(emotion.Type);
                var activeEmotion = new ActiveEmotion(emotion, disposition.Threshold, disposition.Decay);
                string hash = calculateHashString(activeEmotion, m_parent.m_am);

                if (emotionPool.ContainsKey(hash))
                {
                    throw new ArgumentException("This given emotion is already related to given cause",nameof(emotion));
                }

                emotionPool.Add(hash, activeEmotion);
                activeEmotion.GetCause(m_parent.m_am).LinkEmotion(activeEmotion.EmotionType);

                return activeEmotion.ToDto(m_parent.m_am);
		    }


		    public void RemoveEmotion(EmotionDTO emotion)
		    {
                var activeEmotion = new ActiveEmotion(emotion, this.DefaultEmotionDisposition.Decay,this.DefaultEmotionDisposition.Threshold);
                string hash = calculateHashString(activeEmotion, m_parent.m_am);
                emotionPool.Remove(hash);
		    }

			/// <summary>
			/// Creates and Adds to the emotional state a new ActiveEmotion based on 
			/// a received BaseEmotion. However, the ActiveEmotion will be created 
			/// and added to the emotional state only if the final intensity for 
			/// the emotion surpasses the threshold for the emotion type. 
			/// </summary>
			/// <param name="emotion">the BaseEmotion that creates the ActiveEmotion</param>
			/// <returns>the ActiveEmotion created if it was added to the EmotionalState.
			/// Otherwise, if the intensity of the emotion was not enough to be added to the EmotionalState, the method returns null</returns>
			public IActiveEmotion AddEmotion(IEmotion emotion)
			{
				if (emotion == null)
					return null;

				int decay;
				ActiveEmotion auxEmotion = null;
				bool reappraisal = false;

				EmotionDisposition disposition = GetEmotionDisposition(emotion.EmotionType);
				decay = disposition.Decay;

				ActiveEmotion previousEmotion;
				string hash = calculateHashString(emotion, m_parent.m_am);
				if (emotionPool.TryGetValue(hash,out previousEmotion))
				{
					//if this test is true, it means that this is 100% a reappraisal of the same event
					//if not true, it is not a reappraisal, but the appraisal of a new event of the same
					//type
					if (previousEmotion.CauseId == emotion.CauseId)
						reappraisal = true;
					
					//in both cases we need to remove the old emotion. In the case of reappraisal it is obvious.
					//In the case of the appraisal of a similar event, we want to aggregate all the similar resulting 
					//emotions into only one emotion
					emotionPool.Remove(hash);
				}

				float potential = DeterminePotential(emotion);
				if (potential > disposition.Threshold)
				{
					auxEmotion = new ActiveEmotion(emotion, potential, disposition.Threshold, decay, m_parent.Tick);
					emotionPool.Add(hash, auxEmotion);
					if (!reappraisal)
						this.mood.UpdateMood(auxEmotion,m_parent);

					auxEmotion.GetCause(m_parent.m_am).LinkEmotion(auxEmotion.EmotionType);
				}

				return auxEmotion;
			}

			public EmotionDisposition GetEmotionDisposition(String emotionName)
			{
				EmotionDisposition disp;
				if (this.emotionDispositions.TryGetValue(emotionName, out disp))
					return disp;

				return m_defaultEmotionalDisposition;
			}

		    public void RemoveEmotionDisposition(string emotionName)
		    {
		        this.emotionDispositions.Remove(emotionName);
		    }

		    public EmotionDisposition DefaultEmotionDisposition
		    {
		        get { return m_defaultEmotionalDisposition;}
		        set { m_defaultEmotionalDisposition = value;} 
		    }

           public IActiveEmotion DetermineActiveEmotion(IEmotion potEm)
			{
				EmotionDisposition emotionDisposition = GetEmotionDisposition(potEm.EmotionType);
				float potential = DeterminePotential(potEm);

				if (potential > emotionDisposition.Threshold)
					return new ActiveEmotion(potEm, potential, emotionDisposition.Threshold, emotionDisposition.Decay,m_parent.Tick);

				return null;
			}

			/// <summary>
			/// Clears all the emotions in the EmotionalState
			/// </summary>
			public void Clear()
			{
				this.emotionPool.Clear();
			}

			/// <summary>
			/// Decays all emotions, mood and arousal
			/// </summary>
			public void Decay()
			{
				this.mood.DecayMood(m_parent);
				HashSet<string> toRemove = ObjectPool<HashSet<string>>.GetObject();
				using (var it = this.emotionPool.GetEnumerator())
				{
					while (it.MoveNext())
					{
						it.Current.Value.DecayEmotion(m_parent);
						if (!it.Current.Value.IsRelevant)
							toRemove.Add(it.Current.Key);
					}
				}
				foreach (var key in toRemove)
					this.emotionPool.Remove(key);

				toRemove.Clear();
				ObjectPool<HashSet<string>>.Recycle(toRemove);
			}

			public IEnumerable<IActiveEmotion> GetEmotionsByType(string emotionType)
			{
				return emotionPool.Values.Where(emotion => string.Equals(emotion.EmotionType, emotionType, StringComparison.CurrentCultureIgnoreCase)).Cast<IActiveEmotion>();
			}

			/// <summary>
			/// Searches for a given emotion in the EmotionalState
			/// </summary>
			/// <param name="emotionKey">a string that corresponds to a hashkey that represents the emotion in the EmotionalState</param>
			/// <returns>the found ActiveEmotion if it exists in the EmotionalState, null if the emotion doesn't exist anymore</returns>
			public IActiveEmotion GetEmotion(string emotionKey)
			{
				ActiveEmotion emo;
				if (this.emotionPool.TryGetValue(emotionKey, out emo))
					return emo;
				return null;
			}

			/// <summary>
			/// Searches for a given emotion in the EmotionalState
			/// </summary>
			/// <param name="emotionKey">a BaseEmotion that serves as a template to find the active emotion in the EmotionalState</param>
			/// <returns>the found ActiveEmotion if it exists in the EmotionalState, null if the emotion doesn't exist anymore</returns>
			public IActiveEmotion GetEmotion(IEmotion emotion)
			{
				return GetEmotion(calculateHashString(emotion,m_parent.m_am));
			}

			public void RemoveEmotion(IActiveEmotion em)
			{
				if (em != null)
					this.emotionPool.Remove(calculateHashString(em,m_parent.m_am));
			}

			public IEnumerable<string> GetEmotionsKeys()
			{
				return this.emotionPool.Keys;
			}

			public IEnumerable<IActiveEmotion> GetAllEmotions()
			{
				return this.emotionPool.Values.Cast<IActiveEmotion>();
			}

			public float Mood
			{
				get
				{
					return this.mood.MoodValue;
				}
			    set
			    {
                    this.mood.SetMoodValue(value,m_parent);
                }
			}
            
			public IActiveEmotion GetStrongestEmotion()
			{
				return this.emotionPool.Values.MaxValue(emo => emo.Intensity);
			}

			public IActiveEmotion GetStrongestEmotion(Name cause)
			{
				var set = this.emotionPool.Values.Where(emo => emo.GetCause(m_parent.m_am).EventName.Match(cause));
				return set.MaxValue(emo => emo.Intensity);
			}

			public void AddEmotionDisposition(EmotionDisposition emotionDisposition)
			{
                this.emotionDispositions.Add(emotionDisposition.Emotion, emotionDisposition);
			}

			public IEnumerable<EmotionDisposition> GetEmotionDispositions()
			{
				return this.emotionDispositions.Values;
			}

			public override string ToString()
			{
				return $"Mood: {this.mood} Emotions: {this.emotionPool}";
			}

			public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				dataHolder.SetValue("Parent",m_parent);
				dataHolder.SetValue("EmotionalPool", emotionPool.Values.ToArray());
				dataHolder.SetValue("EmotionDispositions", emotionDispositions.Values.Prepend(m_defaultEmotionalDisposition).ToArray());
				dataHolder.SetValue("Mood",mood.MoodValue);
			}

			public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				m_parent = dataHolder.GetValue<EmotionalAppraisalAsset>("Parent");
				mood.SetMoodValue(dataHolder.GetValue<float>("Mood"), m_parent);
				var dispositions = dataHolder.GetValue<EmotionDisposition[]>("EmotionDispositions");
				EmotionDisposition defaultDisposition = null;
				foreach (var disposition in dispositions)
				{
					if (string.Equals(disposition.Emotion, "*", StringComparison.InvariantCultureIgnoreCase))
					{
						if (defaultDisposition == null)
							defaultDisposition = disposition;
					}
					else
						emotionDispositions.Add(disposition.Emotion, disposition);
				}
				if (defaultDisposition == null)
					defaultDisposition = new EmotionDisposition("*",1,1);
				m_defaultEmotionalDisposition = defaultDisposition;

				context.PushContext();
				{
					context.Context = m_parent.Tick;

					var emotions = dataHolder.GetValue<ActiveEmotion[]>("EmotionalPool");
					foreach (var emotion in emotions)
					{
						var hash = calculateHashString(emotion, m_parent.m_am);
						emotionPool.Add(hash, emotion);
					}
				}
				context.PopContext();
			}
		}
	}
}
