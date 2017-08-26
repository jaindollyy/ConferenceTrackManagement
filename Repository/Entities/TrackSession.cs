using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement.Processor
{
	public class TrackSession
	{
		public string Name;
		public TimeSpan Duration;
		public List<SessionTalk> SessionTalks;

		public TrackSession(string name, int Hours)
		{
			Name = name;
			Duration = new TimeSpan(Hours, 0, 0);
			SessionTalks = new List<SessionTalk>();
		}
	}
}
