using System;

namespace ConferenceTrackManagement.Processor
{
	public class TalkDuration
	{
		public int value;
		public TalkDuration(int duration)
		{
			try
			{
				if (IsDurationInvalid(duration))
					throw new Exception("Invalid Talk Duration");
				value = duration;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		private bool IsDurationInvalid(int duration)
		{
			if ((duration < 0) || (duration > 60))
				return true;
			return false;
		}
	}

}
