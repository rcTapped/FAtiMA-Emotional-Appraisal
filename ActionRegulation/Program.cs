using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
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
            drives.EnergyWeight = 0.8f;
            drives.IntegrityWeight = 0.8f;
            drives.AffiliationWeight = 1.0f;

            EmotionalAppraisalAsset asset;

            asset = EmotionalAppraisalAsset.LoadFromFile("F:\\Documents\\Github Projects\\IntelligentAgents\\example.json");
            
            //test stuff

            AppraisalRuleDTO rule = drives.EnergyToAppraisalRule(5);
            AppraisalRuleDTO rule2 = drives.EnergyToAppraisalRule(-8);

            AppraisalRuleDTO rule3 = drives.IntegrityToAppraisalRule(3);
            AppraisalRuleDTO rule4 = drives.IntegrityToAppraisalRule(-4);

            AppraisalRuleDTO rule5 = drives.AffiliationToAppraisalRule(8);
            AppraisalRuleDTO rule6 = drives.AffiliationToAppraisalRule(-4);
            
            AppraisalRuleDTO rule7 = drives.GoalToAppraisalRule(new Goal("socialconflict", -4.0f, -4.0f, -8.0f));
            AppraisalRuleDTO rule8 = drives.GoalToAppraisalRule(new Goal("socialconflict", 3.0f, 3.0f, 6.0f));

            AppraisalRuleDTO rule9 = drives.DrivesToAppraisalRule(3.0f, 3.0f, 6.0f);
            AppraisalRuleDTO rule10 = drives.DrivesToAppraisalRule(-5.0f, -5.0f, 5.0f);

            asset.AddOrUpdateAppraisalRule(rule);
            asset.AddOrUpdateAppraisalRule(rule2);

            asset.AddOrUpdateAppraisalRule(rule3);
            asset.AddOrUpdateAppraisalRule(rule4);

            asset.AddOrUpdateAppraisalRule(rule5);
            asset.AddOrUpdateAppraisalRule(rule6);

            asset.AddOrUpdateAppraisalRule(rule7);
            asset.AddOrUpdateAppraisalRule(rule8);

            asset.AddOrUpdateAppraisalRule(rule9);
            asset.AddOrUpdateAppraisalRule(rule10);

            //var fileStream = File.Open("F:\\Documents\\Github Projects\\IntelligentAgents\\example.json", FileMode.Open);
            //asset.SaveToFile(fileStream);

            //

            string input = "";

            while(true)
            {
                input = Console.ReadLine();

                var eventArg = new List<Name>();
                if(input == "1")
                    eventArg.Add((Name)"event(action,stranger,socialconflict(lose),test)");
                else if(input == "2")
                    eventArg.Add((Name)"event(action,friend,socialconflict(gain),test)");
                else if (input == "3")
                    eventArg.Add((Name)"event(action,jim,affiliation(lose),test)");
                else if (input == "4")
                    eventArg.Add((Name)"event(action,bob,affiliation(gain),test)");
                else if (input == "5")
                    eventArg.Add((Name)"event(action,food,energy(gain),test)");

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
            }
        }
    }
}
