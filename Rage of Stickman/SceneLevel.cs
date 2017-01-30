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

		private bool onBackToMenu;

		public SceneLevel(AnimatedTexture2D background, AnimatedTexture2D foreground, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(background, position, size, active, visible)
		{
			this.foreground = foreground;
			Initialize();
		}

		public void Initialize()
		{
			onBackToMenu = false;
		}

		public override void EventHandler()
		{
			if (Game.Content.gameEvents.Count > 0)
			{
				for (int ID = Game.Content.gameEvents.Count - 1; ID >= 0; ID--)
				{
					if (Game.Content.gameEvents[ID].Target() == ETarget.Scene)
					{
						//switch (Game.Content.gameEvents[ID].Event())
						//{
							
						//}
						Game.Content.gameEvents.RemoveAt(ID);
					}
				}
			}
		}

		public override void Update(bool isPaused)
		{
			if (active)
			{
				Input();
				Logic();

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

		private void Input()
		{
			onBackToMenu = false;

			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				if (!Game.Content.previousKeyState.IsKeyDown(key))
				{
					switch (key)
					{
						case Keys.Escape:
							onBackToMenu = true;
							break;
					}
				}
			}
		}

		private void Logic()
		{
			if (onBackToMenu)
			{
				Game.Content.gameEvents.Add(new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu));
			}
		}

		public override void Draw()
		{
			base.Draw();

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

			if (foreground != null)
			{
				foreground.Draw(position);
			}
		}
	}
}
