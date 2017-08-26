using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceTrackManagement.Processor;

namespace ConferenceTrackManagement.Processor
{
    public class InputProcessor
    {

        private string[] Inputs;
        private int NumberOfTracks;
        private List<SessionTalk> SessionTalks;
        private List<Track> Tracks=new List<Track> { };
        
        public InputProcessor(string[] inputs, int numberOfTracks)
        {
            this.Inputs = inputs;
            this.NumberOfTracks = numberOfTracks;                 
        }

        public List<Track> Process()
        {
            CreateSessionTalks(Inputs);
            CreateTracks(NumberOfTracks);
            ScheduleSessionTalks();
            return Tracks;
        }


            /// <summary>
            /// Method to create a sorted session talks
            /// </summary>
            /// <param name="inputs"> List created from input.txt file</param>

            public void CreateSessionTalks(string[] inputs)
        {
            List<SessionTalk> selectedTalks = new List<SessionTalk>();
            foreach (string input in inputs)
            {
                SessionTalk newTalk = new SessionTalk(input);
                selectedTalks.Add(newTalk);
            }
            List<SessionTalk> SortedTalkList = selectedTalks.OrderBy(o => o.Duration.value).ToList();
            SessionTalks = SortedTalkList;
        }

        /// <summary>
        /// Method to create tracks
        /// </summary>
        /// <param name="numberOfTracks">This is hardcoded to 2</param>
        public  void CreateTracks(int numberOfTracks)
        {
            List<Track> tracks = new List<Track>();
            Track TrackProgram;
            for (int i = 0; i < numberOfTracks; i++)
            {
                TrackProgram = new Track(i + 1);
                tracks.Add(TrackProgram);
            }
            Tracks = tracks;
        }

        /// <summary>
        /// Method to Schedule session talks for both tracks
        /// </summary>
        public void ScheduleSessionTalks()
        {
            foreach (Track t in Tracks)
            {
                bool MorningSessionFull = false;
                bool EveningSessionFull = false;

                //morning session -->
                TimeSpan MorningTS = t.Morning.Duration;
                double TempTime = MorningTS.TotalMinutes;
                for (int i = SessionTalks.Count - 1; i >= 0; i--)
                {
                    //for morning session -->
                    if ((TempTime >= double.Parse(SessionTalks[i].Duration.value.ToString())) && (!MorningSessionFull))
                    {
                        t.Morning.SessionTalks.Add(SessionTalks[i]);
                        TempTime = TempTime - double.Parse(SessionTalks[i].Duration.value.ToString());
                        SessionTalks.RemoveAt(i);
                        if (TempTime == 0)
                        {
                            MorningSessionFull = true;
                        }
                    }
                }

                t.TimeSaved += int.Parse(TempTime.ToString());

                //evening session -->
                TimeSpan eveningTS = t.Evening.Duration;
                TempTime = eveningTS.TotalMinutes;
                for (int i = SessionTalks.Count - 1; i >= 0; i--)
                {
                    //for evening session -->
                    if (MorningSessionFull)
                    {
                        if ((TempTime >= double.Parse(SessionTalks[i].Duration.value.ToString())) && (!EveningSessionFull))
                        {
                            t.Evening.SessionTalks.Add(SessionTalks[i]);
                            TempTime = TempTime - double.Parse(SessionTalks[i].Duration.value.ToString());
                            SessionTalks.RemoveAt(i);
                            if (TempTime == 0)
                            {
                                EveningSessionFull = true;
                            }
                        }
                    }
                }

                t.TimeSaved += int.Parse(TempTime.ToString());//total time saved in the whole track

            }
        }
    }
}
