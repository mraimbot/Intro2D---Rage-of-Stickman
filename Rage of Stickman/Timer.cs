using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class Timer : GameObject
	{
		private float timeInSeconds;
		private float time;

		public Timer(float timeInSeconds)
			: base(Vector2.Zero, Vector2.One, 0, Color.Purple, true, false)
		{
			Reset(timeInSeconds);
		}
		public void Reset(float timeInSeconds = 0)
		{
			if (timeInSeconds > 0)
			{
				this.timeInSeconds = timeInSeconds * 1000;
			}

			time = this.timeInSeconds;
		}

		public bool IsTimeUp()
		{
			return (time == 0) ? (true) : (false);
		}

		public override void Update(bool isPaused)
		{
			if (isActive)
			{
				if (!isPaused)
				{
					time -= Game.Content.gameTime.ElapsedGameTime.Milliseconds;
					if (time < 0)
					{
						time = 0;
					}
				}
			}
		}
	}
}
