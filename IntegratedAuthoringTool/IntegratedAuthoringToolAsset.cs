﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using GAIPS.Rage;
using GAIPS.Serialization;
using IntegratedAuthoringTool.DTOs;
using KnowledgeBase.WellFormedNames;
using RolePlayCharacter;

namespace IntegratedAuthoringTool
{
    [Serializable]
    public class IntegratedAuthoringToolAsset : LoadableAsset<IntegratedAuthoringToolAsset>, ICustomSerialization
    {
	    private class CharacterHolder
	    {
		    public string Source;
		    public RolePlayCharacterAsset RPCAsset;
        }

        public static readonly string VALID_DIALOGUE_PROPERTY = "ValidDialogue";
        public static readonly string INITIAL_DIALOGUE_STATE = "Start";
        public static readonly string TERMINAL_DIALOGUE_STATE = "End";
        public static readonly string ANY_DIALOGUE_STATE = "*";
        public static readonly string PLAYER = "Player";
        public static readonly string AGENT = "Agent";

        private IList<DialogStateAction> m_playerDialogues;
        private IList<DialogStateAction> m_agentDialogues;

        private Dictionary<string, CharacterHolder> m_characterSources;
        private Dictionary<string, string> m_dialogueStates; 

        public string ScenarioName { get; set; }
        
	    protected override string OnAssetLoaded()
	    {
		    KeyValuePair<string, CharacterHolder> current = new KeyValuePair<string, CharacterHolder>();
            
            try
		    {
				foreach (var pair in m_characterSources)
				{
					current = pair;

					if (pair.Value.RPCAsset == null)
					{
						string errorsOnLoad;
						pair.Value.RPCAsset = RolePlayCharacterAsset.LoadFromFile(ToAbsolutePath(pair.Value.Source),out errorsOnLoad);
					    if (errorsOnLoad != null)
					        return errorsOnLoad;
					    else
					    {
					        foreach (var d in m_agentDialogues)
					        {
						        var validDialoguePropertyEvent =
							        $"Event({Constants.PROPERTY_CHANGE_EVENT},World,{VALID_DIALOGUE_PROPERTY}({d.CurrentState},{d.NextState},{d.Meaning},{d.Style}),True)";
                                pair.Value.RPCAsset.PerceptionActionLoop(new[] {validDialoguePropertyEvent});
                            }
					    }
					}

					if (!string.Equals(pair.Key, pair.Value.RPCAsset.CharacterName))
						return $"Name mismatch. IAT name \"{pair.Key}\" != RPC File Name \"{pair.Value.RPCAsset.CharacterName}\" for file \"{ToAbsolutePath(pair.Value.Source)}\"";

				}
			}
		    catch (Exception ex)
		    {
			    return $"An error occured when trying to load the RPC \"{current.Key}\" at \"{ToAbsolutePath(current.Value.Source)}\". Please check if the path is correct.";

			}
		    return null;
		}

	    protected override void OnAssetPathChanged(string oldpath)
	    {
		    foreach (var holder in m_characterSources.Values)
		    {
				holder.Source = ToRelativePath(AssetFilePath, ToAbsolutePath(oldpath, holder.Source));
		    }
	    }

	    public IntegratedAuthoringToolAsset()
        {
            m_playerDialogues = new List<DialogStateAction>();
            m_agentDialogues = new List<DialogStateAction>();
	        m_characterSources = new Dictionary<string, CharacterHolder>();
            m_dialogueStates = new Dictionary<string, string>();
        }
        
        public void AddAgentDialogAction(DialogueStateActionDTO dialogueStateActionDTO)
        {
            this.m_agentDialogues.Add(new DialogStateAction(dialogueStateActionDTO));
        }

        public string GetCurrentDialogueState(string character)
        {
            if (m_dialogueStates.ContainsKey(character))
            {
                return m_dialogueStates[character];
            }
            else
            {
                m_dialogueStates[character] = INITIAL_DIALOGUE_STATE;
                return INITIAL_DIALOGUE_STATE;
            }
        }

        public void SetDialogueState(string character, string state)
        {
            m_dialogueStates[character] = state;
        }

        public void AddPlayerDialogAction(DialogueStateActionDTO dialogueStateActionDTO)
        {
            this.m_playerDialogues.Add(new DialogStateAction(dialogueStateActionDTO));
        }

        public void EditPlayerDialogAction(DialogueStateActionDTO dialogueStateActionToEdit, DialogueStateActionDTO newDialogueAction)
        {
            this.AddPlayerDialogAction(newDialogueAction);
            this.RemoveDialogueActions(PLAYER, new[] { dialogueStateActionToEdit });
        }

        public void EditAgentDialogAction(DialogueStateActionDTO dialogueStateActionToEdit, DialogueStateActionDTO newDialogueAction)
        {
            this.AddAgentDialogAction(newDialogueAction);
            this.RemoveDialogueActions(AGENT, new[] {dialogueStateActionToEdit});
        }

        public IEnumerable<DialogueStateActionDTO> GetDialogueActions(string speaker, string currentState)
        {
            var dialogList = SelectDialogActionList(speaker);
            if (currentState == ANY_DIALOGUE_STATE)
                return dialogList.Select(d => d.ToDTO());
            else
                return dialogList.Where(d => d.CurrentState == currentState).Select(d => d.ToDTO());
        }

        public void  RemoveDialogueActions(string speaker, IEnumerable<DialogueStateActionDTO> actionsToRemove)
        {
            var dialogList = SelectDialogActionList(speaker);
            foreach (var dialogueStateActionDto in actionsToRemove)
            {
                var action = dialogList.FirstOrDefault(d => d.Id == dialogueStateActionDto.Id);
                dialogList.Remove(action);
            }
        }
        
        public IEnumerable<CharacterSourceDTO> GetAllCharacterSources()
        {
	        return m_characterSources.Select(p => new CharacterSourceDTO() {Name = p.Key, Source = ToAbsolutePath(p.Value.Source)});
        }

	    public RolePlayCharacterAsset GetCharacterAsset(string characterName)
	    {
		    CharacterHolder holder;
		    if (!m_characterSources.TryGetValue(characterName, out holder))
				throw new Exception($"Character \"{characterName}\" not found");

		    return holder.RPCAsset;
	    }

	    public IEnumerable<RolePlayCharacterAsset> GetAllCharacters()
	    {
		    return m_characterSources.Values.Select(h => h.RPCAsset);
	    } 

        public void AddCharacter(RolePlayCharacterAsset character)
        {
	        if(m_characterSources.ContainsKey(character.CharacterName))
				throw new Exception("A character with the same name already exists.");

			m_characterSources.Add(character.CharacterName,new CharacterHolder() {Source = ToRelativePath(character.AssetFilePath),RPCAsset = character});
        }

        public void RemoveCharacters(IList<string> charactersToRemove)
        {
            foreach (var characterName in charactersToRemove)
            {
	            m_characterSources.Remove(characterName);
            }   
        }


        private IList<DialogStateAction> SelectDialogActionList(string speaker)
        {
            if (speaker != AGENT && speaker != PLAYER)
            {
                throw new Exception("Invalid Speaker");
            }

            if (speaker == AGENT)
                return m_agentDialogues;
            else
                return m_playerDialogues;
        }

        #region Serialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
            if (m_characterSources.Count > 0)
            {
				dataHolder.SetValue("Characters", m_characterSources.Select(p => new CharacterSourceDTO() { Name = p.Key, Source = ToRelativePath(p.Value.Source) }).ToArray());
            }
            if (m_playerDialogues.Any())
            {
                dataHolder.SetValue("PlayerDialogues", m_playerDialogues.Select(a => a.ToDTO()).ToArray());
            }
            if (m_agentDialogues.Any())
            {
                dataHolder.SetValue("AgentDialogues", m_agentDialogues.Select(a => a.ToDTO()).ToArray());
            }
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");
            var charArray = dataHolder.GetValue<CharacterSourceDTO[]>("Characters");
            if (charArray != null)
				m_characterSources = charArray.ToDictionary(c => c.Name, c => new CharacterHolder() {Source = c.Source });

            var playerDialogueArray = dataHolder.GetValue<DialogueStateActionDTO[]>("PlayerDialogues");
            if (playerDialogueArray != null)
            {
                m_playerDialogues = new List<DialogStateAction>(playerDialogueArray.Select(dto => new DialogStateAction(dto)));
            }

            var agentDialogueArray = dataHolder.GetValue<DialogueStateActionDTO[]>("AgentDialogues");
            if (agentDialogueArray != null)
            {
                m_agentDialogues = new List<DialogStateAction>(agentDialogueArray.Select(dto => new DialogStateAction(dto)));
            }
        }

        #endregion

      
    }
}
