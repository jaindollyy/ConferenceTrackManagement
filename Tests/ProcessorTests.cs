
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConferenceTrackManagement.Application;
using System.Linq;
using ConferenceTrackManagement.Processor;
using System.Collections.Generic;

namespace Tests
{
    
    [TestClass]
    public class ProcessorTests
    {          
        public static string[] Inputs  = new   string[]
             { " Writing Fast Tests Against Enterprise Rails 60min"
                ,"Overdoing it in Python 45min"
                ,"Lua for the Masses 30min"
                ,"Ruby Errors from Mismatched Gem Versions 45min"
                ,"Common Ruby Errors 45min"
                ,"Rails for Python Developers lightning"
                ,"Communicating Over Distance 60min"
                ,"Accounting - Driven Development 45min"
                ,"Woah 30min"
                ,"Sit Down and Write 30min"
                ,"Pair Programming vs Noise 45min"
                ,"Rails Magic 60min"
                ,"Ruby on Rails: Why We Should Move On 60min"
                ,"Clojure Ate Scala(on my project) 45min"
                ,"Programming in the Boondocks of Seattle 30min"
                ," for Back - End Development 30min"
                ,"Ruby on Rails Legacy App Maintenance 60min"
                ,"A World Without HackerNews 30min"
                ,"User Interface CSS in Rails Apps 30min"
          };
       
        [TestMethod]
        public void CreateOutputTest()
        {
            List<string> Outputs = new List<string> { };
            List<Track> Tracks = new List<Track> { };
            int NumberOfTracks = 2;

            InputProcessor inputProcessor = new InputProcessor(Inputs, NumberOfTracks);
            Tracks = inputProcessor.Process();
            OutputProcessor outputProcessor = new OutputProcessor(Tracks);
            Outputs = outputProcessor.Process();

            bool expected = true;
            bool actual;

            actual = Outputs.Any(x => x.Contains("Track 1:"));
            Assert.AreEqual(expected, actual);

            actual = Outputs.Count(x => x.Contains("12:00PM Lunch")) == 2;
            Assert.AreEqual(expected, actual);

            actual = Outputs.Any(x => x.Contains("Track 2:"));
            Assert.AreEqual(expected, actual);

            actual = Outputs.Count(x => x.Contains("Networking Event")) == 2;
            Assert.AreEqual(expected, actual);
        }
    }
}

