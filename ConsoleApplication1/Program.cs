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

            asset = EmotionalAppraisalAsset.LoadFromFile("F:\\Documents\\Universiteit Utrecht\\Master Thesis\\example.json");

            var eventArg = new List<Name>();
            eventArg.Add(Name.BuildName("event(action,mary,hug,john)"));
            eventArg.Add(Name.BuildName("event(action,mary,hug,john)"));

            asset.AppraiseEvents(eventArg);

            Console.WriteLine(asset.Mood);

            foreach (var emotion in asset.ActiveEmotions)
            {
                Console.WriteLine(emotion.Type);
                Console.WriteLine(emotion.Intensity);
            }

            asset.Update();

            Console.WriteLine("Update");
            Console.WriteLine(asset.Mood);

            foreach (var emotion in asset.ActiveEmotions)
            {
                Console.WriteLine(emotion.Type);
                Console.WriteLine(emotion.Intensity);
            }

            Console.ReadLine();
        }
    }
}
