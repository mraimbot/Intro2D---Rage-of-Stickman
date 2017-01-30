using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class GameEvent
	{
		private ETarget target;
		private EGameEvent gameEvent;
		private float value;
		private string text;

		public GameEvent(ETarget target, EGameEvent gameEvent, float value = 0.0f, string text = "")
		{
			this.target = target;
			this.gameEvent = gameEvent;
			this.value = value;
			this.text = text;
		}

		public ETarget Target()
		{
			return target;
		}

		public EGameEvent Event()
		{
			return gameEvent;
		}

		public float Value()
		{
			return value;
		}

		public string Text()
		{
			return text;
		}
	}
}
