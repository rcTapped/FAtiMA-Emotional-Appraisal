using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalAppraisal;
using KnowledgeBase.WellFormedNames;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            EmotionalAppraisalAsset asset;

            asset = EmotionalAppraisalAsset.LoadFromFile("E:\\Documents\\Github Projects\\IntelligentAgents\\example.json");

            var eventArg = new List<Name>();
            eventArg.Add((Name) "event(action, mary, hug, john)");
            eventArg.Add(Name.BuildName("event(action,mary,hug,john)"));

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
