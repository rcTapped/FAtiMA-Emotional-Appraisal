﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GAIPS.Serialization;
using IntegratedAuthoringTool.DTOs;
using RolePlayCharacter;

namespace IntegratedAuthoringTool
{
    [Serializable]
    public class IntegratedAuthoringToolAsset
    {
        private IList<CharacterSourceDTO> CharacterSources { get; set; }
        private IList<RolePlayCharacterAsset> Characters { get; set; }

        public string ScenarioName { get; set; }
        
        public static IntegratedAuthoringToolAsset LoadFromFile(string filename)
        {
            IntegratedAuthoringToolAsset iat;

            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                iat = serializer.Deserialize<IntegratedAuthoringToolAsset>(f);
            }

            iat.Characters = new List<RolePlayCharacterAsset>();
            foreach (var source in iat.CharacterSources)
            {
                var character = RolePlayCharacterAsset.LoadFromFile(source.Source);
                iat.Characters.Add(character);
            }

            return iat;
        }

        public IEnumerable<CharacterSourceDTO> GetAllCharacterSources()
        {
            return this.CharacterSources;
        }

        public IEnumerable<RolePlayCharacterAsset> GetAllCharacters()
        {
            return this.Characters;
        } 

        public void AddCharacter(string filename)
        {
            var character = RolePlayCharacterAsset.LoadFromFile(filename);
            this.Characters.Add(character);
            this.CharacterSources.Add(new CharacterSourceDTO {Name = character.CharacterName, Source = filename});
        }

        public void RemoveCharacter(string characterName)
        {
            var characterSource = CharacterSources.FirstOrDefault(c => c.Name == characterName);
            var character = Characters.FirstOrDefault(c => c.CharacterName == characterName);
            if (characterSource != null && character != null)
            {
                this.CharacterSources.Remove(characterSource);
                this.Characters.Remove(character);
            }
            else
            {
                throw new Exception("Invalid Operation");
            }
        }

        #region Serialization
        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void GetObjectData(ISerializationData dataHolder)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
            dataHolder.SetValue("Characters", CharacterSources);
        }

        public void SetObjectData(ISerializationData dataHolder)
        {
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");
            CharacterSources = dataHolder.GetValue<List<CharacterSourceDTO>>("Characters");
        }
        #endregion
    }
}
