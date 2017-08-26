using System;
using System.Text.RegularExpressions;

namespace ConferenceTrackManagement.Processor
{
	public class SessionTalk
	{
		public string Topic;
		public TalkDuration Duration;
		public SessionTalk(string topicTitle)
		{
			try
			{
				string tempTopic = "";
				int tempDuration = -1;
				FetchTalkDetails(topicTitle, out tempTopic, out tempDuration);
				TalkDuration _duration = new TalkDuration(tempDuration);
				Duration = _duration;				
				Topic = tempTopic;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		private void FetchTalkDetails(string topicTitle, out string topic, out int duration)
		{
			topic = "";
			duration = 0;
			//fecth the duration and talk title-->
			string tempDuration = Regex.Match(topicTitle, @"\d+").Value;
			if (tempDuration != "")
			{
			
				topic = topicTitle.Replace(tempDuration, "").Replace("min", "");
				duration = int.Parse(tempDuration);
				return;
			}
			else
			{
				if ((topicTitle.ToLower().Contains("lightning")) || (topicTitle.ToUpper().Contains("LIGHTNING")))
					topic = topicTitle;
				duration = 5;
				return;
			}
		}	

	}
}
