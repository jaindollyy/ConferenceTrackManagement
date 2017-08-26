using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConferenceTrackManagement.Processor;

namespace ConferenceTrackManagement.Processor
{
    public class OutputProcessor
    {

        private List<Track> Tracks=new List<Track> { };
        private List<string> Outputs = new List<string> { "Output:" };

        public OutputProcessor(List<Track> tracks)
        {
            this.Tracks = tracks;                     
        }

        /// <summary>
        /// Method to add both tracks conference program in output list
        /// </summary>
        public List<string> Process()
        {
          
            foreach (Track t in Tracks)
            {
                var currentTime = new TimeSpan(9, 0, 0);
                //trackNumber
                Outputs.Add("Track " + t.TrackNumber.ToString() + ":");


                //calculate the time 
                //morning
                TimeSpan resultTimeMorning = TimeSpan.FromHours(9);
                resultTimeMorning = TimeSpan.FromMinutes(resultTimeMorning.TotalMinutes);
                string fromTimeStringM = resultTimeMorning.ToString("hh':'mm");
                //evening
                TimeSpan resultTimeEvening = TimeSpan.FromHours(1);
                resultTimeEvening = TimeSpan.FromMinutes(resultTimeEvening.TotalMinutes);
                string fromTimeStringE = resultTimeEvening.ToString("hh':'mm");
                //<- time calculation ends here

                //Morning Session
                for (int i = 0; i < t.Morning.SessionTalks.Count; i++)
                {
                    fromTimeStringM = resultTimeMorning.ToString("hh':'mm");
                    Outputs.Add(fromTimeStringM + "AM " + t.Morning.SessionTalks[i].Topic);

                    int time = t.Morning.SessionTalks[i].Duration.value;
                    resultTimeMorning = TimeSpan.FromMinutes(resultTimeMorning.TotalMinutes + time);

                }

                Outputs.Add("12:00PM Lunch");


                //Evening Session                
                for (int i = 0; i < t.Evening.SessionTalks.Count; i++)
                {
                    fromTimeStringE = resultTimeEvening.ToString("hh':'mm");
                    Outputs.Add(fromTimeStringE + "PM " + t.Evening.SessionTalks[i].Topic);

                    int time = t.Evening.SessionTalks[i].Duration.value;
                    resultTimeEvening = TimeSpan.FromMinutes(resultTimeEvening.TotalMinutes + time);
                }

                //Networking event
                if (resultTimeEvening < TimeSpan.FromHours(4))
                    resultTimeEvening = TimeSpan.FromHours(4);
                if (resultTimeEvening > TimeSpan.FromHours(5))
                    resultTimeEvening = TimeSpan.FromHours(5);
                fromTimeStringE = resultTimeEvening.ToString("hh':'mm");
                Outputs.Add(fromTimeStringE + "PM Networking Event");
            }
            return Outputs;
        }

       
    }
}
