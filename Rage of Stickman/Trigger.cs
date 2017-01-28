using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class Trigger : GameObject
	{
		private GameEvent gameEvent;

		public Trigger(GameEvent gameEvent, Vector2 position, Vector2 size, bool active = true)
			: base(position, size, active, false)
		{
			this.gameEvent = gameEvent;
		}

		public void Activate()
		{
			Game.Content.gameEvents.Add(gameEvent);
		}
	}
}
