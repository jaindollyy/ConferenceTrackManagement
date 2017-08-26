using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement.Processor
{
	public class Track
	{
		public int TrackNumber;
		public TrackSession Morning;
		public NonTrackSession Lunch;
		public TrackSession Evening;
		public NonTrackSession NetworkingEvent;
		public int TimeSaved;
		public Track(int trackNumber)
		{
			TrackNumber = trackNumber;
			Morning = new TrackSession("Morning", 3);
			Lunch = new NonTrackSession("Lunch", 1);
			Evening = new TrackSession("Evening", 4);
			NetworkingEvent = new NonTrackSession("NetworkingEvent", 1);
			TimeSaved = 0;
		}
	}

}
