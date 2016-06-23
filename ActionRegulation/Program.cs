using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using KnowledgeBase.WellFormedNames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ActionRegulation
{
    class Program
    {
        static void Main(string[] args)
        {
            PlanningGraph graphPlan = new PlanningGraph();
            graphPlan.birthdayDinnerExample();

            WorldState worldState = new WorldState();
            worldState.addState("chicken nuggets on sale");
            worldState.addState("rainy weather");
            worldState.addState("something something test");

            Drives drives = new Drives(0.8f, 0.8f, 1.0f);

            DriveSatisfaction driveSatisfaction = new DriveSatisfaction(drives);

            EmotionalAppraisalAsset asset;
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\example.json");
            //asset = EmotionalAppraisalAsset.LoadFromFile(path);

            //drive to appraisal rule test stuff
            /*
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
            */
            //var fileStream = File.Open("F:\\Documents\\Github Projects\\IntelligentAgents\\example.json", FileMode.Open);
            //asset.SaveToFile(fileStream);


            //drive and goal effects test stuff

            //Goal testGoal1 = new Goal("get nuggets", new List<string>(new string[] { "chicken nuggets on sale" }), 5, 0, 0);
            //Goal testGoal2 = new Goal("get all the nuggets", new List<string>(new string[] { "chicken nuggets on sale", "infinite nuggets available" }), 10, 0, 0);
            //Goal testGoal3 = new Goal("get a lotta nuggets", new List<string>(new string[] { "chicken nuggets on sale" }), 7, 0, 0);

            ////////////////////////////// goal/action planning manual work ///////////////////////////////////////////


            //goals
            //Goal eat = new Goal("eat", new List<string>(new string[] { "have food" }), 6, 0, 0);
            //Goal drink = new Goal("drink", new List<string>(), 2, 0, 0);
            //Goal rest = new Goal("rest", new List<string>(new string[] { "place to rest" }), 7, 0, -1);

            //Goal socialize = new Goal("socialize", new List<string>(new string[] { "plan with friends" }), -3, 0, 5);
            //Goal paintball = new Goal("paintball", new List<string>(new string[] { "plan with friends" }), -6, -1, 4);

            //actions
            //Action makeFood = new Action("make food", new List<string>(new string[] { "have groceries" }), new List<string>(new string[] { "have food" }), -0.2f, 0, 0);
            //Action getGroceries = new Action("get groceries", new List<string>(), new List<string>(new string[] { "have groceries" }), -1, 0, 0);
            
            //Action checkTransportation = new Action("check transportation", new List<string>(), new List<string>(new string[] { "have transportation" }), -0.1f, 0, 0);

            //Action makePlans = new Action("make plans", new List<string>(), new List<string>(new string[] { "plan with friends" }), -1, 0, 0);

            //Action findPlaceToRest = new Action("find place to rest", new List<string>(), new List<string>(new string[] { "place to rest" }), -1, 0, 0);

            //////////////////////////////////////////////////////////////////////////////////////////////////

            string input = "";

            while(true)
            {
                input = Console.ReadLine();

                var eventArg = new List<String>();
                ///////////////// drive to appraisal rule test stuff/////////////////////////
                if (input == "1")
                    eventArg.Add("event(action,stranger,socialconflict(lose),test)");
                else if (input == "2")
                    eventArg.Add("event(action,friend,socialconflict(gain),test)");
                else if (input == "3")
                    eventArg.Add("event(action,jim,affiliation(lose),test)");
                else if (input == "4")
                    eventArg.Add("event(action,bob,affiliation(gain),test)");
                else if (input == "5")
                    eventArg.Add("event(action,food,energy(gain),test)");
                ///////////////// drive and goal effects test stuff//////////////////////////
                else if (input == "6")
                {
                    //driveSatisfaction.addGoal(testGoal1);
                    //driveSatisfaction.addGoal(testGoal3);
                    //driveSatisfaction.ChooseGoal(worldState);
                    Console.WriteLine("energy: " + drives.Energy + " integrity: " + drives.Integrity + " affiliation: " + drives.Affiliation);
                }
                else if (input == "7")
                {
                    //driveSatisfaction.addGoal(testGoal2);
                    //driveSatisfaction.ChooseGoal(worldState);
                    Console.WriteLine("energy: " + drives.Energy + " integrity: " + drives.Integrity + " affiliation: " + drives.Affiliation);
                }
                else if (input == "8")
                {
                    List<string> initialState = new List<string>(new string[] { "have(cake)" });
                    List<Action> actionList = new List<Action>();

                    //actionList.Add(new Action("eat(cake)", new List<string>(new string[] { "have(cake)" }), new List<string>(new string[] { "!have(cake)", "eaten(cake)" })));
                    //actionList.Add(new Action("bake(cake)", new List<string>(new string[] { "!have(cake)" }), new List<string>(new string[] { "have(cake)" })));

                    //Goal cake = new Goal("cake", new List<string>(new string[] { "have(cake)", "eaten(cake)" }), 7, 0, 0);

                    //driveSatisfaction.addGoal(cake);

                    driveSatisfaction.printGoalList();

                    Console.WriteLine(driveSatisfaction.chooseGoal().Name);

                    driveSatisfaction.actionPlanner(initialState, actionList, driveSatisfaction.chooseGoal());
                }
                else if (input == "9")
                {
                    //BasicGraph graph = new BasicGraph();
                    //graph.CakeExample();

                    //graphPlan = new PlanningGraph();
                    graphPlan.birthdayDinnerExample();
                }
                else if ( input == "10")
                {
                    Literal nugget = new Literal("nugget", true);
                    List<Literal> test1 = new List<Literal>(new Literal[] { nugget, new Literal("bread", true), new Literal("chicken", false) });
                    List<Literal> test2 = new List<Literal>(new Literal[] { nugget });
                    List<Literal> list = test1.Except(test2).ToList();
                    foreach (Literal lit in list)
                        Console.WriteLine("name: " + lit.Name + " value: " + lit.Value);
                }

                //asset.AppraiseEvents(eventArg);

                //Console.WriteLine("Mood: " + asset.Mood);

                //foreach (var emotion in asset.ActiveEmotions)
                //{
                //    Console.WriteLine(emotion.Type + ": " + emotion.Intensity);
                //}

                ////asset.Update();

                //Console.WriteLine("Update");
                //Console.WriteLine("Mood: " + asset.Mood);

                //foreach (var emotion in asset.ActiveEmotions)
                //{
                //    Console.WriteLine(emotion.Type + ": " + emotion.Intensity);
                //}
            }
        }
    }
}
