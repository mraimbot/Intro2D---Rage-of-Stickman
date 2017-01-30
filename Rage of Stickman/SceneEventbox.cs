using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class SceneEventbox : SceneComponent
	{
		List<GameEvent> events;

		public SceneEventbox(List<GameEvent> events)
			: base(null, Vector2.Zero, Vector2.One, true, false)
		{
			this.events = events;
		}
		public override void Update(bool isPaused)
		{
			base.Update(isPaused);

			if (active)
			{
				if (!isPaused)
				{
					if (events != null && events.Count > 0)
					{
						Game.Content.gameEvents.Add(events.ElementAt(0));
						events.RemoveAt(0);
					}
				}
			}
		}
	}
}
