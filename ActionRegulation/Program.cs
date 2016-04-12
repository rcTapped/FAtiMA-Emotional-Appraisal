using EmotionalAppraisal;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.DTOs;
using KnowledgeBase.DTOs.Conditions;
using KnowledgeBase.WellFormedNames;
using System;
using System.Collections.Generic;
using System.IO;

namespace ActionRegulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Drives drives = new Drives();
            EmotionalAppraisalAsset asset;

            asset = EmotionalAppraisalAsset.LoadFromFile("F:\\Documents\\Github Projects\\IntelligentAgents\\example.json");
            
            var eventArg = new List<Name>();
            eventArg.Add((Name)"event(action,food,energy(lose),self)");

            //test stuff

            AppraisalRule rule = drives.EnergyToEmotion(5);
            
            var newRule = new AppraisalRuleDTO()
            {
                EventMatchingTemplate = rule.EventName.ToString(),
                Desirability = rule.Desirability,
                Praiseworthiness = rule.Praiseworthiness,
                Conditions = new List<ConditionDTO>(),
            };

            asset.AddOrUpdateAppraisalRule(newRule);

            AppraisalRule rule2 = drives.EnergyToEmotion(-5);

            var newRule2 = new AppraisalRuleDTO()
            {
                EventMatchingTemplate = rule2.EventName.ToString(),
                Desirability = rule2.Desirability,
                Praiseworthiness = rule2.Praiseworthiness,
                Conditions = new List<ConditionDTO>(),
            };

            asset.AddOrUpdateAppraisalRule(newRule2);

            //var fileStream = File.Open("F:\\Documents\\Github Projects\\IntelligentAgents\\example.json", FileMode.Open);
            //asset.SaveToFile(fileStream);

            //

            asset.AppraiseEvents(eventArg);

            Console.WriteLine("Mood: " + asset.Mood);

            foreach (var emotion in asset.ActiveEmotions)
            {
                Console.WriteLine(emotion.Type + ": " + emotion.Intensity);
            }

            asset.Update();

            Console.WriteLine("Update");
            Console.WriteLine("Mood: " + asset.Mood);

            foreach (var emotion in asset.ActiveEmotions)
            {
                Console.WriteLine(emotion.Type + ": " + emotion.Intensity);
            }

            Console.ReadLine();
        }
    }
}
