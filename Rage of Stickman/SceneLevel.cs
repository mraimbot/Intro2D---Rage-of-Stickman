using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
    class SceneLevel : SceneComponent
    {
		private AnimatedTexture2D foreground;

		public SceneLevel(AnimatedTexture2D background, AnimatedTexture2D foreground, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(background, position, size, active, visible)
		{
			this.foreground = foreground;
		}

		public override void EventHandler()
		{
			if (Game.Content.gameEvents.Count > 0)
			{
				for (int ID = Game.Content.gameEvents.Count - 1; ID >= 0; ID--)
				{
					if (Game.Content.gameEvents[ID].Target() == ETarget.Level)
					{
						switch (Game.Content.gameEvents[ID].Event())
						{
							case EGameEvent.Player_Heal:
								Game.Content.player.Damage(-(int)Game.Content.gameEvents[ID].Value());
								break;

							case EGameEvent.Player_Rage:
								Game.Content.player.Rage((int)Game.Content.gameEvents[ID].Value());
								break;
						}
						Game.Content.gameEvents.RemoveAt(ID);
					}
				}
			}
		}

		public override void Update(bool isPaused)
		{
			if (isActive)
			{
				Game.Content.tileMap.Update(isPaused);
				Game.Content.player.Update(isPaused);

				for (int i = Game.Content.enemies.Count - 1; i >= 0; i--)
				{
					Game.Content.enemies[i].Update(isPaused);
					if (Game.Content.enemies[i].isDead())
					{
						Game.Content.enemies.RemoveAt(i);
					}
				}
			}

			base.Update(isPaused);
		}

		public override void Draw()
		{
			if (background != null)
			{
				background.Draw(position);
			}

			Game.Content.tileMap.Draw();

			foreach (Enemy enemy in Game.Content.enemies)
			{
				enemy.Draw();
			}

			Game.Content.player.Draw();

			foreach (Trigger trigger in Game.Content.triggers)
			{
				trigger.Draw();
			}

			if (foreground != null)
			{
				foreground.Draw(position);
			}
		}
	}
}
