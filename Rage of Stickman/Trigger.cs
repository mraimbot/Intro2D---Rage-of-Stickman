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
		private AnimatedTexture2D background;
		private GameEvent gameEvent;

		private bool onlyOnce;

		public Trigger(AnimatedTexture2D background, GameEvent gameEvent, Vector2 position, Vector2 size, bool onlyOnce = false, bool isVisible = true, bool isActive = true)
			: base(position, size, 0, Color.Purple, isActive, isVisible)
		{
			this.background = background;
			this.gameEvent = gameEvent;
			this.onlyOnce = onlyOnce;
		}

		public void Activate()
		{
			if (isActive)
			{
				Game.Content.gameEvents.Add(gameEvent);

				if (onlyOnce)
				{
					isActive = false;
					isVisible = false;
				}
			}
		}

		public override void Draw()
		{
			if (isVisible)
			{
				if (background != null)
				{
					background.Draw(position);
				}
			}
		}
	}
}
